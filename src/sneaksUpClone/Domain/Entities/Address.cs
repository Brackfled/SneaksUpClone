using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Address : Entity<Guid>
{
    public Guid UserId { get; set; }
    public string AddressName { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
    public string ContactName { get; set; }

    public User? User { get; set; } = default!;

    public Address()
    {
        AddressName = string.Empty;
        City = string.Empty;
        Country = string.Empty;
        ZipCode = string.Empty;
        ContactName = string.Empty;
    }

    public Address(string addressName, string city, string country, string zipCode, string contactName)
    {
        AddressName = addressName;
        City = city;
        Country = country;
        ZipCode = zipCode;
        ContactName = contactName;
    }
}
