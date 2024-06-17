using Application.Features.Baskets.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Baskets.Commands.Update;

public class UpdateBasketCommand : IRequest<UpdatedBasketResponse>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public double TotalPrice { get; set; }

    public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, UpdatedBasketResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository;
        private readonly BasketBusinessRules _basketBusinessRules;

        public UpdateBasketCommandHandler(IMapper mapper, IBasketRepository basketRepository,
                                         BasketBusinessRules basketBusinessRules)
        {
            _mapper = mapper;
            _basketRepository = basketRepository;
            _basketBusinessRules = basketBusinessRules;
        }

        public async Task<UpdatedBasketResponse> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            Basket? basket = await _basketRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            await _basketBusinessRules.BasketShouldExistWhenSelected(basket);
            basket = _mapper.Map(request, basket);

            await _basketRepository.UpdateAsync(basket!);

            UpdatedBasketResponse response = _mapper.Map<UpdatedBasketResponse>(basket);
            return response;
        }
    }
}