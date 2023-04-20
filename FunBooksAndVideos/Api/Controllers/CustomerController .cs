using Microsoft.AspNetCore.Mvc;
using MediatR;
using AutoMapper;
using Application.Queries.CustomerQuery;
using Application.DTOs.CustomerDto;
using Application.Commands.CustomerCommand;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CustomerController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                // Use Mediator to send a GetCustomersQuery
                var query = new GetCustomersQuery();
                var customers = await _mediator.Send(query);
                return Ok(customers);
            }
            catch (Exception ex)
            {
                // Handle exception and return appropriate response
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                // Use Mediator to send a GetCustomerByIdQuery
                var query = new GetCustomerByIdQuery (id);
                var customer = await _mediator.Send(query);

                // If customer is not found, return NotFound
                if (customer == null)
                {
                    return NotFound();
                }

                // Map Customer to CustomerDto
                var customerDto = _mapper.Map<CustomerDto>(customer);

                return Ok(customerDto);
            }
            catch (Exception ex)
            {
                // Handle exception and return appropriate response
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto createCustomerDto)
        {
            try
            {
                // Validate request data using FluentValidation
                var validator = new CustomerDtoValidator();
                var result = await validator.ValidateAsync(createCustomerDto);
                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }
                var command = new CreateCustomerCommand
                {
                    Customer = createCustomerDto
                };
                await _mediator.Send(command);

                return Ok();
            }
            catch (Exception ex)
            {
                // Handle exception and return appropriate response
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerDto updateCustomerDto)
        {
            try
            {
                // Validate request data using FluentValidation
                var validator = new UpdateCustomerDtoValidator();
                var result = await validator.ValidateAsync(updateCustomerDto);
                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }
                var command = new UpdateCustomerCommand
                {
                    Id = id ,
                    Customer = updateCustomerDto
                };

                // Use Mediator to send an UpdateCustomerCommand
                await _mediator.Send(command);

                return Ok();
            }
            catch (Exception ex)
            {
                // Handle exception and return appropriate response
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                // Use Mediator to send a DeleteCustomerCommand
                var command = new DeleteCustomerCommand { Id = id };
                await _mediator.Send(command);

                return Ok();
            }
            catch (Exception ex)
            {
                // Handle exception and return appropriate response
                return StatusCode(500, ex.Message);
            }
        }
    }

}
   