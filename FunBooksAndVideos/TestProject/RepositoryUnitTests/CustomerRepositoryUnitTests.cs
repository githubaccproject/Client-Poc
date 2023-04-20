using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.CustomerRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.RepositoryUnitTests
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        private ProjectDBContext _dbContext;
        private ICustomerRepository _customerRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            // Set up the test data and initialize the repository
            var options = new DbContextOptionsBuilder<ProjectDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _dbContext = new ProjectDBContext(options);
            _customerRepository = new CustomerRepository(_dbContext);
        }

        [TestMethod]
        public async Task GetAll_Customer_ReturnsAllCustomer()
        {
            // Arrange
            var Customer = new List<Customer>
        {
            new Customer { Id = 1, Name = "Sanny" },
            new Customer { Id = 2, Name = "Raj" },
            new Customer { Id = 3, Name = "Balaji" }
        };
            _dbContext.Customer.AddRange(Customer);
            _dbContext.SaveChanges();

            // Act
            var result = await _customerRepository.GetAll().ToListAsync();

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("Sanny", result[0].Name);
            Assert.AreEqual("Raj", result[1].Name);
            Assert.AreEqual("Balaji", result[2].Name);
        }

        [TestMethod]
        public async Task GetById_ExistingCustomerId_ReturnsCustomer()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "Sanny" };
            _dbContext.Customer.Add(customer);
            _dbContext.SaveChanges();

            // Act
            var result = await _customerRepository.GetByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Sanny", result.Name);
        }

        [TestMethod]
        public async Task GetById_NonExistingCustomerId_ReturnsNull()
        {
            // Act
            var result = await _customerRepository.GetByIdAsync(999);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Update_ExistingCustomer_SavesChanges()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "Sanny" };
            _dbContext.Customer.Add(customer);
            _dbContext.SaveChanges();

            // Act
            customer.Name = "Raj";
            _customerRepository.Update(customer);
            _customerRepository.SaveChangesAsync();

            // Assert
            var result = await _customerRepository.GetByIdAsync(1);
            Assert.AreEqual("Raj", result.Name);
        }

        [TestMethod]
        public async Task Delete_ExistingCustomer_RemovesCustomer()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "Sanny" };
            _dbContext.Customer.Add(customer);
            _dbContext.SaveChanges();

            // Act
            _customerRepository.Delete(customer);
            _customerRepository.SaveChangesAsync();

            // Assert
            var result = await _customerRepository.GetByIdAsync(1);
            Assert.IsNull(result);
        }
    }

}
