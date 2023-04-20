using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.OrderProductRepository
{
    public class OrderProductRepository : Repository<OrderProduct>, IOrderProductRepository
    {
        public OrderProductRepository(ProjectDBContext dbContext) : base(dbContext)
        {
        }
    }

}
