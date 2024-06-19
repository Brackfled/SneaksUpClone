using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;

namespace Application.Features.BasketItems.Queries.GetList;

public class GetListBasketItemQuery : IRequest<GetListResponse<GetListBasketItemListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListBasketItemQueryHandler : IRequestHandler<GetListBasketItemQuery, GetListResponse<GetListBasketItemListItemDto>>
    {
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly IMapper _mapper;

        public GetListBasketItemQueryHandler(IBasketItemRepository basketItemRepository, IMapper mapper)
        {
            _basketItemRepository = basketItemRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListBasketItemListItemDto>> Handle(GetListBasketItemQuery request, CancellationToken cancellationToken)
        {
            IPaginate<BasketItem> basketItems = await _basketItemRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListBasketItemListItemDto> response = _mapper.Map<GetListResponse<GetListBasketItemListItemDto>>(basketItems);
            return response;
        }
    }
}