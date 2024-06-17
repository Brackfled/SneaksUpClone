using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;

namespace Application.Features.Baskets.Queries.GetList;

public class GetListBasketQuery : IRequest<GetListResponse<GetListBasketListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListBasketQueryHandler : IRequestHandler<GetListBasketQuery, GetListResponse<GetListBasketListItemDto>>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public GetListBasketQueryHandler(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListBasketListItemDto>> Handle(GetListBasketQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Basket> baskets = await _basketRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListBasketListItemDto> response = _mapper.Map<GetListResponse<GetListBasketListItemDto>>(baskets);
            return response;
        }
    }
}