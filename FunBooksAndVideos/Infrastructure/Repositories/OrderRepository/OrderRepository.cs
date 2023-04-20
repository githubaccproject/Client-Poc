using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.OrderRepository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ProjectDBContext dbContext) : base(dbContext)
        {
        }

        // Add any additional methods specific to Order entity
    }

}
