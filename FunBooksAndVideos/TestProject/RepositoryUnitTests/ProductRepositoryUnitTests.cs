using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.ProductRepository;
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
    public class ProductRepositoryTests
    {
        private IProductRepository _productRepository;
        private DbContextOptions<ProjectDBContext> _dbContextOptions;

        [TestInitialize]
        public void TestInitialize()
        {
            // Set up the in-memory database for testing
            _dbContextOptions = new DbContextOptionsBuilder<ProjectDBContext>()
                .UseInMemoryDatabase(databaseName: "ProductRepository_TestDB")
                .Options;

            // Initialize the ProductRepository with the in-memory database
            using (var dbContext = new ProjectDBContext(_dbContextOptions))
            {
                dbContext.Database.EnsureCreated();
            }

            _productRepository = new ProductRepository(new ProjectDBContext(_dbContextOptions));
        }

        [TestMethod]
        public async Task Add_ValidProduct_AddsProductToDatabase()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product1", Barcode = "123456789", IsActive = true, Description = "Test Product 1", price = 10.99m, Category = "Test Category" };

            // Act
            _productRepository.AddAsync(product);
            _productRepository.SaveChangesAsync();

            // Assert
            using (var dbContext = new ProjectDBContext(_dbContextOptions))
            {
                var result = await dbContext.Product.FirstOrDefaultAsync(p => p.Id == product.Id);
                Assert.IsNotNull(result);
                Assert.AreEqual(product.Id, result.Id);
                Assert.AreEqual(product.Name, result.Name);
                Assert.AreEqual(product.Barcode, result.Barcode);
                Assert.AreEqual(product.IsActive, result.IsActive);
                Assert.AreEqual(product.Description, result.Description);
                Assert.AreEqual(product.price, result.price);
                Assert.AreEqual(product.Category, result.Category);
            }
        }

        [TestMethod]
        public async Task Update_ValidProduct_UpdatesProductInDatabase()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product1", Barcode = "123456789", IsActive = true, Description = "Test Product 1", price = 10.99m, Category = "Test Category" };
            using (var dbContext = new ProjectDBContext(_dbContextOptions))
            {
                dbContext.Product.Add(product);
                dbContext.SaveChanges();
            }

            product.Name = "Product Updated";
            product.Barcode = "987654321";
            product.IsActive = false;
            product.Description = "Updated Product";
            product.price = 20.99m;
            product.Category = "Updated Category";

            // Act
            _productRepository.Update(product);
            _productRepository.SaveChangesAsync();

            // Assert
            using (var dbContext = new ProjectDBContext(_dbContextOptions))
            {
                var result = await dbContext.Product.FirstOrDefaultAsync(p => p.Id == product.Id);
                Assert.IsNotNull(result);
                Assert.AreEqual(product.Id, result.Id);
                Assert.AreEqual(product.Name, result.Name);
                Assert.AreEqual(product.Barcode, result.Barcode);
                Assert.AreEqual(product.IsActive, result.IsActive);
                Assert.AreEqual(product.Description, result.Description);
                Assert.AreEqual(product.price, result.price);
                Assert.AreEqual(product.Category, result.Category);
            }
        }

        [TestMethod]
        public async Task Delete_ValidProduct_DeletesProductFromDatabase()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product1", Barcode = "123456789", IsActive = true, Description = "Test Product 1", price = 10.99m, Category = "Test Category" };
            using (var dbContext = new ProjectDBContext(_dbContextOptions))
            {
                dbContext.Product.Add(product);
                dbContext.SaveChanges();
            }

            // Act
            _productRepository.Delete(product);
            _productRepository.SaveChangesAsync();

            // Assert
            using (var dbContext = new ProjectDBContext(_dbContextOptions))
            {
                var result = await dbContext.Product.FirstOrDefaultAsync(p => p.Id == product.Id);
                Assert.IsNull(result);
            }
        }

    }

 }
