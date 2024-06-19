using Application.Features.BasketItems.Commands.Create;
using Application.Features.BasketItems.Commands.Delete;
using Application.Features.BasketItems.Commands.Update;
using Application.Features.BasketItems.Queries.GetById;
using Application.Features.BasketItems.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.BasketItems.Queries.GetByUserId;

namespace Application.Features.BasketItems.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<BasketItem, CreateBasketItemCommand>().ReverseMap();
        CreateMap<BasketItem, CreatedBasketItemResponse>().ReverseMap();
        CreateMap<BasketItem, UpdateBasketItemCommand>().ReverseMap();
        CreateMap<BasketItem, UpdatedBasketItemResponse>().ReverseMap();
        CreateMap<BasketItem, DeleteBasketItemCommand>().ReverseMap();
        CreateMap<BasketItem, DeletedBasketItemResponse>().ReverseMap();
        CreateMap<BasketItem, GetByIdBasketItemResponse>().ReverseMap();
        CreateMap<BasketItem, GetListBasketItemListItemDto>().ReverseMap();
        CreateMap<IPaginate<BasketItem>, GetListResponse<GetListBasketItemListItemDto>>().ReverseMap();

        CreateMap<BasketItem, GetByUserIdBasketItemsItemDto>()
            .ForMember(destinationMember:bi => bi.BasketUserId, memberOptions:opt => opt.MapFrom(bi => bi.Basket.UserId))
            .ForMember(destinationMember:bi => bi.BasketTotalPrice, memberOptions:opt => opt.MapFrom(bi => bi.Basket.TotalPrice))
            .ForMember(destinationMember:bi => bi.ProductName, memberOptions:opt => opt.MapFrom(bi => bi.Product.Name))
            .ForMember(destinationMember:bi => bi.ProductStockAmount, memberOptions:opt => opt.MapFrom(bi => bi.Product.StockAmount))
            .ForMember(destinationMember:bi => bi.ProductPrice, memberOptions:opt => opt.MapFrom(bi => bi.Product.Price))
            .ReverseMap();
        CreateMap<IPaginate<BasketItem>, GetListResponse<GetByUserIdBasketItemsItemDto>>().ReverseMap();
    }
}