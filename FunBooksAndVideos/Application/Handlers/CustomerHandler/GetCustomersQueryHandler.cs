using Application.DTOs.CustomerDto;
using Application.Queries.CustomerQuery;
using AutoMapper;
using Infrastructure.Repositories.CustomerRepository;
using MediatR;

namespace Application.Handlers.CustomerHandler
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<List<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = _customerRepository.GetAll();
            var customerDtos = _mapper.Map<List<CustomerDto>>(customers);
            return customerDtos;
        }
    }
}
