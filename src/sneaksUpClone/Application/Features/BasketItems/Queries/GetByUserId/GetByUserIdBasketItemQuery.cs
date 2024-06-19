using Application.Features.Baskets.Rules;
using Application.Services.BasketItems;
using Application.Services.Baskets;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BasketItems.Queries.GetByUserId;
public class GetByUserIdBasketItemQuery: IRequest<GetListResponse<GetByUserIdBasketItemsItemDto>>
{
    public Guid UserId { get; set; }

    public class GetByUserIdBasketItemQueryHandler : IRequestHandler<GetByUserIdBasketItemQuery, GetListResponse<GetByUserIdBasketItemsItemDto>>
    {
        private readonly IBasketItemService _basketItemService;
        private readonly IBasketService _basketService;
        private readonly BasketBusinessRules _basketBusinessRules;
        private IMapper _mapper;

        public GetByUserIdBasketItemQueryHandler(IBasketItemService basketItemService, IMapper mapper, IBasketService basketService,
                                                 BasketBusinessRules basketBusinessRules)
        {
            _basketItemService = basketItemService;
            _mapper = mapper;
            _basketService = basketService;
            _basketBusinessRules = basketBusinessRules;
        }

        public async Task<GetListResponse<GetByUserIdBasketItemsItemDto>> Handle(GetByUserIdBasketItemQuery request, CancellationToken cancellationToken)
        {
            Basket? basket = await _basketService.GetAsync(predicate:b => b.UserId == request.UserId);
            await _basketBusinessRules.BasketShouldExistWhenSelected(basket);

            IPaginate<Domain.Entities.BasketItem>? basektItems = await _basketItemService.GetListAsync(predicate: bi => bi.Basket.Id == basket.Id, include: bi => bi.Include(bi => bi.Basket).Include(bi => bi.Product),
                                                                                                   cancellationToken: cancellationToken,
                                                                                                   size:100,
                                                                                                   index:0);

            GetListResponse<GetByUserIdBasketItemsItemDto> response = _mapper.Map<GetListResponse<GetByUserIdBasketItemsItemDto>>(basektItems);
            return response;
            
        }
    }
}
