using Application.DTOs.CustomerDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.CustomerQuery
{
    public class GetCustomerByIdQuery : IRequest<CustomerDto>
    {
        public int CustomerId { get; set; }

        public GetCustomerByIdQuery(int customerId)
        {
            CustomerId = customerId;
        }
    }

}
