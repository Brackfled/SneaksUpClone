using Application.Features.Baskets.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Baskets.Queries.GetById;

public class GetByIdBasketQuery : IRequest<GetByIdBasketResponse>
{
    public Guid Id { get; set; }

    public class GetByIdBasketQueryHandler : IRequestHandler<GetByIdBasketQuery, GetByIdBasketResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository;
        private readonly BasketBusinessRules _basketBusinessRules;

        public GetByIdBasketQueryHandler(IMapper mapper, IBasketRepository basketRepository, BasketBusinessRules basketBusinessRules)
        {
            _mapper = mapper;
            _basketRepository = basketRepository;
            _basketBusinessRules = basketBusinessRules;
        }

        public async Task<GetByIdBasketResponse> Handle(GetByIdBasketQuery request, CancellationToken cancellationToken)
        {
            Basket? basket = await _basketRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            await _basketBusinessRules.BasketShouldExistWhenSelected(basket);

            GetByIdBasketResponse response = _mapper.Map<GetByIdBasketResponse>(basket);
            return response;
        }
    }
}