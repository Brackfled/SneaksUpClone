using Application.Services.PaymentService;
using Iyzipay.Model;
using Iyzipay.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Payments.Commands;
public class InstallmentInfoCommand : IRequest<InstallmentInfo>
{
    public string BinNo { get; set; }
    public double Price { get; set; }

    public class InstallmentInfoCommandHandler : IRequestHandler<InstallmentInfoCommand, InstallmentInfo>
    {
        private readonly IPaymentService _paymentService;

        public InstallmentInfoCommandHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<InstallmentInfo> Handle(InstallmentInfoCommand request, CancellationToken cancellationToken)
        {
            RetrieveInstallmentInfoRequest retrieveInstallmentInfoRequest = new() 
            {
                Locale = "tr",
                ConversationId = Guid.NewGuid().ToString(),
                Price = request.Price.ToString(),
                BinNumber = request.BinNo
            };

            InstallmentInfo response = await _paymentService.GetInstallmentInfo(retrieveInstallmentInfoRequest);

            return response;
        }
    }
}
