using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.CustomerMembershipRepository
{
    public interface ICustomerMembershipRepository : IRepository<CustomerMembership>
    {
        // Add any additional methods specific to CustomerMembership entity
    }
}
