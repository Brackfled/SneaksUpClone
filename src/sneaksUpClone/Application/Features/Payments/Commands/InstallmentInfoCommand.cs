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
    public RetrieveInstallmentInfoRequest RetrieveInstallmentInfoRequest { get; set; }

    public class InstallmentInfoCommandHandler : IRequestHandler<InstallmentInfoCommand, InstallmentInfo>
    {
        private readonly IPaymentService _paymentService;

        public InstallmentInfoCommandHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<InstallmentInfo> Handle(InstallmentInfoCommand request, CancellationToken cancellationToken)
        {
            InstallmentInfo response = await _paymentService.GetInstallmentInfo(request.RetrieveInstallmentInfoRequest);

            return response;
        }
    }
}
