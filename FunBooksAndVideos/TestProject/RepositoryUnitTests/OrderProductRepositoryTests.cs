using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.OrderProductRepository;
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
    public class OrderProductRepositoryTests
    {
        private OrderProductRepository _orderProductRepository;
        private DbContextOptions<ProjectDBContext> _dbContextOptions;

        [TestInitialize]
        public void TestInitialize()
        {
            // Set up DbContext options with in-memory database
            _dbContextOptions = new DbContextOptionsBuilder<ProjectDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            // Create a new instance of OrderProductRepository with the in-memory database
            using (var dbContext = new ProjectDBContext(_dbContextOptions))
            {
                dbContext.Database.EnsureCreated();
            }

            _orderProductRepository = new OrderProductRepository(new ProjectDBContext(_dbContextOptions));
        }

        [TestMethod]
        public async Task GetById_ValidId_ReturnsOrderProduct()
        {
            // Arrange
            var orderProduct = new OrderProduct { Id = 1, Order_ID = 1, Product_ID = 1, price = 10.99m, MembershipName = "Test Membership", Quantity = 2 };
            using (var dbContext = new ProjectDBContext(_dbContextOptions))
            {
                dbContext.OrderProduct.Add(orderProduct);
                dbContext.SaveChanges();
            }

            // Act
            var result = await _orderProductRepository.GetByIdAsync(orderProduct.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(orderProduct.Id, result.Id);
        }

        [TestMethod]
        public async Task GetAll_ReturnsAllOrderProduct()
        {
            // Arrange
            var orderProduct1 = new OrderProduct { Id = 1, Order_ID = 1, Product_ID = 1, price = 10.99m, MembershipName = "Test Membership 1", Quantity = 2 };
            var orderProduct2 = new OrderProduct { Id = 2, Order_ID = 2, Product_ID = 2, price = 20.99m, MembershipName = "Test Membership 2", Quantity = 3 };
            using (var dbContext = new ProjectDBContext(_dbContextOptions))
            {
                dbContext.OrderProduct.AddRange(orderProduct1, orderProduct2);
                dbContext.SaveChanges();
            }

            // Act
            var result = await _orderProductRepository.GetAll().ToListAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public async Task Add_ValidOrderProduct_AddsOrderProductToDatabase()
        {
            // Arrange
            var orderProduct = new OrderProduct { Id = 1, Order_ID = 1, Product_ID = 1, price = 10.99m, MembershipName = "Test Membership", Quantity = 2 };

            // Act
            _orderProductRepository.AddAsync(orderProduct);
            _orderProductRepository.SaveChangesAsync();

            // Assert
            using (var dbContext = new ProjectDBContext(_dbContextOptions))
            {
                var result = await dbContext.OrderProduct.FirstOrDefaultAsync(op => op.Id == orderProduct.Id);
                Assert.IsNotNull(result);
                Assert.AreEqual(orderProduct.Id, result.Id);
            }
        }

        [TestMethod]
        public async Task Update_ValidOrderProduct_UpdatesOrderProductInDatabase()
        {
            // Arrange
            var orderProduct = new OrderProduct { Id = 1, Order_ID = 1, Product_ID = 1, price = 10.99m, MembershipName = "Test Membership", Quantity = 2 };
            using (var dbContext = new ProjectDBContext(_dbContextOptions))
            {
                dbContext.OrderProduct.Add(orderProduct);
                dbContext.SaveChanges();
            }

            // Modify orderProduct
            orderProduct.Quantity = 3;
                        // Act
        _orderProductRepository.Update(orderProduct);
            _orderProductRepository.SaveChangesAsync();

            // Assert
            using (var dbContext = new ProjectDBContext(_dbContextOptions))
            {
                var result = await dbContext.OrderProduct.FirstOrDefaultAsync(op => op.Id == orderProduct.Id);
                Assert.IsNotNull(result);
                Assert.AreEqual(3, result.Quantity);
            }
        }

        [TestMethod]
        public async Task Delete_ValidOrderProduct_DeletesOrderProductFromDatabase()
        {
            // Arrange
            var orderProduct = new OrderProduct { Id = 1, Order_ID = 1, Product_ID = 1, price = 10.99m, MembershipName = "Test Membership", Quantity = 2 };
            using (var dbContext = new ProjectDBContext(_dbContextOptions))
            {
                dbContext.OrderProduct.Add(orderProduct);
                dbContext.SaveChanges();
            }

            // Act
            _orderProductRepository.Delete(orderProduct);
            _orderProductRepository.SaveChangesAsync();

            // Assert
            using (var dbContext = new ProjectDBContext(_dbContextOptions))
            {
                var result = await dbContext.OrderProduct.FirstOrDefaultAsync(op => op.Id == orderProduct.Id);
                Assert.IsNull(result);
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Clean up the in-memory database after each test
            using (var dbContext = new ProjectDBContext(_dbContextOptions))
            {
                dbContext.Database.EnsureDeleted();
            }
        }
    
    }


}
