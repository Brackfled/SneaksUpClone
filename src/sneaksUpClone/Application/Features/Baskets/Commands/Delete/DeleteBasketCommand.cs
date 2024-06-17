using Application.Features.Baskets.Constants;
using Application.Features.Baskets.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Baskets.Commands.Delete;

public class DeleteBasketCommand : IRequest<DeletedBasketResponse>
{
    public Guid Id { get; set; }

    public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand, DeletedBasketResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository;
        private readonly BasketBusinessRules _basketBusinessRules;

        public DeleteBasketCommandHandler(IMapper mapper, IBasketRepository basketRepository,
                                         BasketBusinessRules basketBusinessRules)
        {
            _mapper = mapper;
            _basketRepository = basketRepository;
            _basketBusinessRules = basketBusinessRules;
        }

        public async Task<DeletedBasketResponse> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            Basket? basket = await _basketRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            await _basketBusinessRules.BasketShouldExistWhenSelected(basket);

            await _basketRepository.DeleteAsync(basket!);

            DeletedBasketResponse response = _mapper.Map<DeletedBasketResponse>(basket);
            return response;
        }
    }
}