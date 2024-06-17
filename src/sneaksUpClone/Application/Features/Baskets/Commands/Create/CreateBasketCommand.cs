using Application.Features.Baskets.Rules;
using Application.Services.PaymentService;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Baskets.Commands.Create;

public class CreateBasketCommand : IRequest<CreatedBasketResponse>
{
    public Guid UserId { get; set; }
    public double TotalPrice { get; set; }

    public class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommand, CreatedBasketResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository;
        private readonly BasketBusinessRules _basketBusinessRules;

        public CreateBasketCommandHandler(IMapper mapper, IBasketRepository basketRepository,
                                         BasketBusinessRules basketBusinessRules)
        {
            _mapper = mapper;
            _basketRepository = basketRepository;
            _basketBusinessRules = basketBusinessRules;
        }

        public async Task<CreatedBasketResponse> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            Basket basket = _mapper.Map<Basket>(request);

            await _basketRepository.AddAsync(basket);

            CreatedBasketResponse response = _mapper.Map<CreatedBasketResponse>(basket);
            return response;
        }
    }
}