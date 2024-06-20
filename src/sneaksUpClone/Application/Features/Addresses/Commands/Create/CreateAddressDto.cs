using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Addresses.Commands.Create;
public class CreateAddressDto
{
    public string City { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
    public string ContactName { get; set; }
    public string AddressName { get; set; }

    public CreateAddressDto()
    {
        City = string.Empty;
        Country = string.Empty;
        ZipCode = string.Empty;
        ContactName = string.Empty;
        AddressName = string.Empty;
    }

    public CreateAddressDto(string city, string country, string zipCode, string contactName, string addressName)
    {
        City = city;
        Country = country;
        ZipCode = zipCode;
        ContactName = contactName;
        AddressName = addressName;
    }
}
