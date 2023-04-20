using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories.CustomerRepository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ProjectDBContext dbContext) : base(dbContext)
        {

        }

    }
}
