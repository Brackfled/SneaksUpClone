using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Basket: Entity<Guid>
{
    public Guid UserId { get; set; }
    public double TotalPrice { get; set; }

    public virtual ICollection<Product> Products { get; set; } = default!;

 
}
