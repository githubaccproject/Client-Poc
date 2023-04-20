using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.CustomerMembershipRepository
{
    public class CustomerMembershipRepository : Repository<CustomerMembership>, ICustomerMembershipRepository
    {
        public CustomerMembershipRepository(ProjectDBContext dbContext) : base(dbContext)
        {
        }
    }

}
