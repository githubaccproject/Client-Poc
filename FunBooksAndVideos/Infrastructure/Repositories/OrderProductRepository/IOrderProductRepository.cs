using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.OrderProductRepository
{
    public interface IOrderProductRepository : IRepository<OrderProduct>
    {
        // Add any additional methods specific to OrderProduct entity
    }
}
