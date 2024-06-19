using Application.Features.BasketItems.Rules;
using Application.Services.Baskets;
using Application.Services.Products;
using Application.Services.Repositories;
using Application.Services.UsersService;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Transaction;

namespace Application.Features.BasketItems.Commands.Create;

public class CreateBasketItemCommand : IRequest<CreatedBasketItemResponse>, ITransactionalRequest
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }

    public class CreateBasketItemCommandHandler : IRequestHandler<CreateBasketItemCommand, CreatedBasketItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;
        private readonly BasketItemBusinessRules _basketItemBusinessRules;

        public CreateBasketItemCommandHandler(IMapper mapper, IBasketItemRepository basketItemRepository,
                                         BasketItemBusinessRules basketItemBusinessRules,
                                         IBasketService basketService,
                                         IProductService productService)
        {
            _mapper = mapper;
            _basketItemRepository = basketItemRepository;
            _basketItemBusinessRules = basketItemBusinessRules;
            _basketService = basketService;
            _productService = productService;
        }

        public async Task<CreatedBasketItemResponse> Handle(CreateBasketItemCommand request, CancellationToken cancellationToken)
        {
            Basket? basket = await _basketService.GetAsync(u => u.UserId == request.UserId, cancellationToken: cancellationToken);
            Product? product = await _productService.GetAsync(p => p.Id == request.ProductId,cancellationToken: cancellationToken);

            BasketItem basketItem = new()
            {
                Id = Guid.NewGuid(),
                BasketId = basket.Id,
                ProductId = product.Id,
            };
            BasketItem createdBasketItem = await _basketItemRepository.AddAsync(basketItem);


            basket.TotalPrice = basket.TotalPrice + product.Price;
            Basket updatedBasket = await _basketService.UpdateAsync(basket);


            CreatedBasketItemResponse response = _mapper.Map<CreatedBasketItemResponse>(createdBasketItem);
            return response;
        }
    }
}