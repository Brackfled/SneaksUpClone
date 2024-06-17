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
    public RetrieveBinNumberRequest RetrieveBinNumberRequest { get; set; }

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
            BinNumber binNumber = await _paymentService.GetBinCheck(request.RetrieveBinNumberRequest);

            BinCheckResponse response = _mapper.Map<BinCheckResponse>(binNumber);
            return response;
        }
    }
}
