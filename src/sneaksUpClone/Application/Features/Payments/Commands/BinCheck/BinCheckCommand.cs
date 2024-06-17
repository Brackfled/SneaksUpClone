using Application.Services.PaymentService;
using AutoMapper;
using Iyzipay.Model;
using Iyzipay.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Payments.Commands.BinCheck;
public class BinCheckCommand: IRequest<BinCheckResponse>
{
    public string BinNo { get; set; }

    public class BinCheckCommandHandler : IRequestHandler<BinCheckCommand, BinCheckResponse>
    {
        private readonly IPaymentService _paymentService;
        private IMapper _mapper;

        public BinCheckCommandHandler(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        public async Task<BinCheckResponse> Handle(BinCheckCommand request, CancellationToken cancellationToken)
        {
            RetrieveBinNumberRequest retrieveBinNumberRequest = new()
            {
                Locale = "tr",
                BinNumber = request.BinNo,
                ConversationId = Guid.NewGuid().ToString(),
            };
            BinNumber binNumber = await _paymentService.GetBinCheck(retrieveBinNumberRequest);

            BinCheckResponse response = _mapper.Map<BinCheckResponse>(binNumber);
            return response;
        }
    }
}
