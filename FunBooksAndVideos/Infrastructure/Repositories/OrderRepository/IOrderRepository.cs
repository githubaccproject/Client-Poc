using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.OrderRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        // Add any additional methods specific to Order entity
    }
}
