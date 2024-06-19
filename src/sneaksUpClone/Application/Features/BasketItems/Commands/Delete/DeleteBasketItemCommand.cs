using Application.Features.BasketItems.Constants;
using Application.Features.BasketItems.Rules;
using Application.Services.Baskets;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.BasketItems.Commands.Delete;

public class DeleteBasketItemCommand : IRequest<DeletedBasketItemResponse>
{
    public Guid Id { get; set; }

    public class DeleteBasketItemCommandHandler : IRequestHandler<DeleteBasketItemCommand, DeletedBasketItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly IBasketService _basketService;
        private readonly BasketItemBusinessRules _basketItemBusinessRules;

        public DeleteBasketItemCommandHandler(IMapper mapper, IBasketItemRepository basketItemRepository,
                                         BasketItemBusinessRules basketItemBusinessRules, IBasketService basketService)
        {
            _mapper = mapper;
            _basketItemRepository = basketItemRepository;
            _basketItemBusinessRules = basketItemBusinessRules;
            _basketService = basketService;
        }

        public async Task<DeletedBasketItemResponse> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            BasketItem? basketItem = await _basketItemRepository.GetAsync(predicate: bi => bi.Id == request.Id, cancellationToken: cancellationToken);
            await _basketItemBusinessRules.BasketItemShouldExistWhenSelected(basketItem);

            Basket? basket = await _basketService.GetAsync(predicate: b => b.Id == basketItem.BasketId, cancellationToken:cancellationToken);
            basket.TotalPrice = basket.TotalPrice - basketItem.Product.Price;
            await _basketService.UpdateAsync(basket);

            await _basketItemRepository.DeleteAsync(basketItem!);

            DeletedBasketItemResponse response = _mapper.Map<DeletedBasketItemResponse>(basketItem);
            return response;
        }
    }
}