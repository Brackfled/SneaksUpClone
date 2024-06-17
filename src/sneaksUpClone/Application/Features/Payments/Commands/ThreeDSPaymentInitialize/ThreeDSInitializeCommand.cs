using Application.Services.PaymentService;
using Iyzipay.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Payments.Commands.ThreeDSPaymentInitialize;
public class ThreeDSInitializeCommand: IRequest<ThreeDSInitializeResponse>
{
    public CreateThreedsPaymentRequest CreateThreedsPaymentRequest {  get; set; }

    public class ThreeDSInitializeCommandHandler : IRequestHandler<ThreeDSInitializeCommand, ThreeDSInitializeResponse>
    {
        private readonly IPaymentService _paymentService;

        public ThreeDSInitializeCommandHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<ThreeDSInitializeResponse> Handle(ThreeDSInitializeCommand request, CancellationToken cancellationToken)
        {
            ThreeDSInitializeResponse response = await _paymentService.ThreeDSPaymentInitialize(request.CreateThreedsPaymentRequest);
            return response;
        }
    }
}
