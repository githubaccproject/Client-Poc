using Application.DTOs.CustomerDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CustomerCommand
{
    public class CreateCustomerCommand : IRequest<int>
    {
        public CreateCustomerDto Customer { get; set; }
    }

    public class UpdateCustomerCommand : IRequest<int>
    {
        public int Id { get; set; }
        public UpdateCustomerDto Customer { get; set; }
    }


    public class DeleteCustomerCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

}
