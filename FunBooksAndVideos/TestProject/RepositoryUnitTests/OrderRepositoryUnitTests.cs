using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.CustomerRepository;
using Infrastructure.Repositories.OrderRepository;
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
    public class OrderRepositoryTests
    {
        private OrderRepository _orderRepository;
        private ProjectDBContext _dbContextOptions;

        [TestInitialize]
        public void TestInitialize()
        {
            // Set up the test data and initialize the repository
            var options = new DbContextOptionsBuilder<ProjectDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _dbContextOptions = new ProjectDBContext(options);
            _orderRepository = new OrderRepository(_dbContextOptions);
        }

        [TestMethod]
        public async Task GetAll_OrderExist_ReturnsAllOrder()
        {
            // Arrange
            var order1 = new Order { Id = 1, Customer_ID = 1, Date = DateTime.Now, price = 100 };
            var order2 = new Order { Id = 2, Customer_ID = 2, Date = DateTime.Now, price = 200 };
            var order3 = new Order { Id = 3, Customer_ID = 3, Date = DateTime.Now, price = 300 };
            _dbContextOptions.Order.AddRange(order1, order2, order3);
            _dbContextOptions.SaveChanges();

            // Act
            var result = await _orderRepository.GetAll().ToListAsync();

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.Any(o => o.Id == order1.Id));
            Assert.IsTrue(result.Any(o => o.Id == order2.Id));
            Assert.IsTrue(result.Any(o => o.Id == order3.Id));
        }

        [TestMethod]
        public async Task GetById_OrderExists_ReturnsOrder()
        {
            // Arrange
            var order = new Order { Id = 1, Customer_ID = 1, Date = DateTime.Now, price = 100 };
            _dbContextOptions.Order.Add(order);
            _dbContextOptions.SaveChanges();

            // Act
            var result = await _orderRepository.GetByIdAsync(order.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(order.Id, result.Id);
            Assert.AreEqual(order.Customer_ID, result.Customer_ID);
            Assert.AreEqual(order.Date, result.Date);
            Assert.AreEqual(order.price, result.price);
        }

        [TestMethod]
        public async Task GetById_OrderNotExists_ReturnsNull()
        {
            // Act
            var result = await _orderRepository.GetByIdAsync(999);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Add_ValidOrder_AddsOrderToDatabase()
        {
            // Arrange
            var order = new Order { Id = 1, Customer_ID = 1, Date = DateTime.Now, price = 100 };

            // Act
            await _dbContextOptions.AddAsync(order);
            await _dbContextOptions.SaveChangesAsync();


            var result = await _dbContextOptions.Order.FirstOrDefaultAsync(o => o.Id == order.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(order.Id, result.Id);
            Assert.AreEqual(order.Customer_ID, result.Customer_ID);
            Assert.AreEqual(order.Date, result.Date);
            Assert.AreEqual(order.price, result.price);
        }

        [TestMethod]
        public async Task Update_ValidOrder_UpdatesOrderInDatabase()
        {
            // Arrange
            var order = new Order { Id = 1, Customer_ID = 1, Date = DateTime.Now, price = 100 };

            await _dbContextOptions.AddAsync(order);
            await _dbContextOptions.SaveChangesAsync();


            order.Customer_ID = 2;
            order.price = 200;
            // Act

            _dbContextOptions.Update(order);
            await _dbContextOptions.SaveChangesAsync();

            // Assert
            var result = await _dbContextOptions.Order.FirstOrDefaultAsync(o => o.Id == order.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(order.Id, result.Id);
            Assert.AreEqual(order.Customer_ID, result.Customer_ID);
            Assert.AreEqual(order.Date, result.Date);
            Assert.AreEqual(order.price, result.price);
        }

        [TestMethod]
        public async Task Delete_ValidOrder_DeletesOrderFromDatabase()
        {
            // Arrange
            var order = new Order { Id = 1, Customer_ID = 1, Date = DateTime.Now, price = 100 };
            await _dbContextOptions.AddAsync(order);
            await _dbContextOptions.SaveChangesAsync();

            // Act
            _orderRepository.Delete(order);
            _orderRepository.SaveChangesAsync();

            // Assert
            var result = await _dbContextOptions.Order.FirstOrDefaultAsync(o => o.Id == order.Id);
            Assert.IsNull(result);
        }

    }


}
