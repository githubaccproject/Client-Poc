using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ProductRepository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ProjectDBContext dbContext) : base(dbContext)
        {
        }
    }

}
