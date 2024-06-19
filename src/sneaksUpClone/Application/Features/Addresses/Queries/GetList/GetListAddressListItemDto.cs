using Domain.Entities;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Addresses.Queries.GetList;

public class GetListAddressListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string AddressName { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
    public string ContactName { get; set; }
    public User? User { get; set; }
}