using Application.Commands.CustomerCommand;
using Infrastructure.Repositories.CustomerRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.CustomerHandler
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the customer from the repository
            var customer = await _customerRepository.GetByIdAsync(request.Id);

            if (customer == null)
            {
                // Customer not found, handle appropriately
                throw new Exception("Customer not found.");
            }

            // Delete the customer from the repository
            _customerRepository.Delete(customer);
            await _customerRepository.SaveChangesAsync();

            return true;
        }
    }
}
