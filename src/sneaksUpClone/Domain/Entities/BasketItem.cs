using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class BasketItem: Entity<Guid>
{
    public Guid BasketId { get; set; }
    public Guid ProductId { get; set; }

    public Basket? Basket { get; set; } = default!;
    public Product? Product { get; set; } = default!;
}
