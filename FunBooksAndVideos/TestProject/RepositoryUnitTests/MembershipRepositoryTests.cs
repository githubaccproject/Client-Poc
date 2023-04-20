using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.CustomerMembershipRepository;
using Infrastructure.Repositories.MembershipRepository;
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
    public class MembershipRepositoryTests
    {
        private IMembershipRepository _membershipRepository;
        private DbContextOptions<ProjectDBContext> _dbContextOptions;

        [TestInitialize]
        public void TestInitialize()
        {
            // Set up the in-memory database for testing
            _dbContextOptions = new DbContextOptionsBuilder<ProjectDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            // Initialize the MembershipRepository with the in-memory database
            using (var dbContext = new ProjectDBContext(_dbContextOptions))
            {
                dbContext.Database.EnsureCreated();
            }

            _membershipRepository = new MembershipRepository(new ProjectDBContext(_dbContextOptions));
        }

        [TestMethod]
        public async Task Add_ValidMembership_AddsMembershipToDatabase()
        {
            // Arrange
            var membership = new Membership { Id = 1, Name = "Test Membership", Price = "14.99" };

            // Act
            _membershipRepository.AddAsync(membership);
            _membershipRepository.SaveChangesAsync();

            // Assert
            using (var dbContext = new ProjectDBContext(_dbContextOptions))
            {
                var result = await dbContext.Membership.FirstOrDefaultAsync(m => m.Id == membership.Id);
                Assert.IsNotNull(result);
                Assert.AreEqual("Test Membership", result.Name);
                Assert.AreEqual("14.99" , result.Price);
            }
        }

        [TestMethod]
        public async Task Update_ValidMembership_UpdatesMembershipInDatabase()
        {
            // Arrange
            var membership = new Membership { Id = 1, Name = "Test Membership", Price = "14.99" };
            using (var dbContext = new ProjectDBContext(_dbContextOptions))
            {
                dbContext.Membership.Add(membership);
                dbContext.SaveChanges();
            }

            // Act
            membership.Price = "14.99";
            _membershipRepository.Update(membership);
            _membershipRepository.SaveChangesAsync();

            // Assert
            using (var dbContext = new ProjectDBContext(_dbContextOptions))
            {
                var result = await dbContext.Membership.FirstOrDefaultAsync(m => m.Id == membership.Id);
                Assert.IsNotNull(result);
                Assert.AreEqual("14.99", result.Price);
            }
        }

        [TestMethod]
        public async Task Delete_ValidMembership_DeletesMembershipFromDatabase()
        {
            // Arrange
            var membership = new Membership { Id = 1, Name = "Test Membership", Price = "14.99" };
            using (var dbContext = new ProjectDBContext(_dbContextOptions))
            {
                dbContext.Membership.Add(membership);
                dbContext.SaveChanges();
            }

            // Act
            _membershipRepository.Delete(membership);
            _membershipRepository.SaveChangesAsync();

            // Assert
            using (var dbContext = new ProjectDBContext(_dbContextOptions))
            {
                var result = await dbContext.Membership.FirstOrDefaultAsync(m => m.Id == membership.Id);
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
    

