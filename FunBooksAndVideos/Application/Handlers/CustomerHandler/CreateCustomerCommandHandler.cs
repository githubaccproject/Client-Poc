using Application.Commands.CustomerCommand;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories.CustomerRepository;
using MediatR;

namespace Application.Handlers.CustomerHandler
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(IMapper mapper, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request.Customer);
            _customerRepository.AddAsync(customer);
            await _customerRepository.SaveChangesAsync();

            return customer.Id;
        }
    }
}
