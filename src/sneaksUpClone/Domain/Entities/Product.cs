using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Product: Entity<Guid>
{
    public string Name { get; set; }
    public int StockAmount { get; set; }
    public double Price { get; set; }



    public Product()
    {
        Name = string.Empty;
    }

    public Product(string name, int stockAmount, double price)
    {
        Name = name;
        StockAmount = stockAmount;
        Price = price;
    }
}
