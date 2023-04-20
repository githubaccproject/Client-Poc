using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Controllers;
using Application.Commands.CustomerCommand;
using Application.DTOs.CustomerDto;
using Application.Queries.CustomerQuery;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TestProject.ControllerUnitTests
{
    [TestClass]
    public class CustomerControllerTests
    {
        private readonly Mock<IMediator> mediatorMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly CustomerController _controller;

        public CustomerControllerTests()
        {
            // Initialize Moq mocks
            mediatorMock = new Mock<IMediator>();
            mapperMock = new Mock<IMapper>();

            // Create CustomerController instance
            _controller = new CustomerController(mediatorMock.Object, mapperMock.Object);
        }

        [TestMethod]
        public async Task GetCustomers_Returns_OkObjectResult_With_Customers()
        {
            var customers = new List<CustomerDto>
            {
                new CustomerDto { Id = 1, Name = "Customer1", Email = "customer1@example.com", IsActive = true, Phone = "1234567890" },
                new CustomerDto { Id = 2, Name = "Customer2", Email = "customer2@example.com", IsActive = true, Phone = "9876543210" }
            };
            mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomersQuery>(), default)).ReturnsAsync(customers);

            // Act
            var result = await _controller.GetCustomers();

            // Assert
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task CreateCustomer_ValidData_ReturnsOkResult()
        {
            // Arrange
            var createCustomerDto = new CreateCustomerDto
            {
                // Set up the properties of createCustomerDto with valid data for testing
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<CreateCustomerCommand>(), default)).Returns(Task.FromResult(1));

            // Act
            var result = await _controller.CreateCustomer(createCustomerDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task CreateCustomer_InvalidData_ReturnsBadRequestResult()
        {
            // Arrange
            var createCustomerDto = new CreateCustomerDto
            {
                // Set up the properties of createCustomerDto with invalid data for testing
            };

            var validationFailures = new FluentValidation.Results.ValidationResult();
            validationFailures.Errors.Add(new FluentValidation.Results.ValidationFailure("PropertyName", "Error Message"));

            var validatorMock = new Mock<IValidator<CreateCustomerDto>>();
            validatorMock.Setup(v => v.ValidateAsync(createCustomerDto, default)).ReturnsAsync(validationFailures);

            mediatorMock.Setup(m => m.Send(It.IsAny<CreateCustomerCommand>(), default)).Returns(Task.FromResult(1));

            // Act
            var result = await _controller.CreateCustomer(createCustomerDto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }


        [TestMethod]
        public async Task CreateCustomer_ExceptionThrown_ReturnsStatusCode500()
        {
            // Arrange
            var createCustomerDto = new CreateCustomerDto
            {
                // Set up the properties of createCustomerDto for testing
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<CreateCustomerCommand>(), default)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.CreateCustomer(createCustomerDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            var statusCodeResult = (StatusCodeResult)result;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task GetCustomers_Returns_InternalServerError_When_Exception_Thrown()
        {
            mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomersQuery>(), default)).ThrowsAsync(new Exception("Some error occurred"));

            // Act
            var result = await _controller.GetCustomers();

            // Assert
            Assert.IsNotNull(result);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            var statusCodeResult = (StatusCodeResult)result;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);

        }

        [TestMethod]
        public async Task GetCustomerById_Returns_OkObjectResult_With_Customer()
        {
            // Arrange
            var customer = new CustomerDto { Id = 1, Name = "Customer1", Email = "customer1@example.com", IsActive = true, Phone = "1234567890" };
            mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomerByIdQuery>(), default)).ReturnsAsync(customer);

            // Act
            var result = await _controller.GetCustomerById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual(customer, okResult.Value);

        }

    }
}