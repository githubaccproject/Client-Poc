using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.CustomerRepository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        // Add any additional methods specific to Customer entity
    }
}
