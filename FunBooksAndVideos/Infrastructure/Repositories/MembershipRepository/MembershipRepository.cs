using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.MembershipRepository
{
    public class MembershipRepository : Repository<Membership>, IMembershipRepository
    {
        public MembershipRepository(ProjectDBContext dbContext) : base(dbContext)
        {
        }
    }

}
