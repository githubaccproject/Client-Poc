using Application.Commands.CustomerCommand;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories.CustomerRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.CustomerHandler
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, int>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(IMapper mapper, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            // Perform validation using FluentValidation

            // If validation fails, throw an exception or return appropriate response

            // Retrieve the customer from the repository
            var customer = await _customerRepository.GetByIdAsync(request.Id);

            if (customer == null)
            {
                // Customer not found, handle appropriately
                throw new Exception("Customer not found.");
            }

            // Update the customer entity with the data from UpdateCustomerDto
            var UpdateCustomer = _mapper.Map<Customer>(request.Customer);

            // Update the repository
            _customerRepository.Update(UpdateCustomer);
            await _customerRepository.SaveChangesAsync();

            return customer.Id;
        }
    }
}
