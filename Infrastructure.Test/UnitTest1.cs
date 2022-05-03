using ApplicationCore.Definitions;
using ApplicationCore.Entities;
using Infrastructure.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Create_User()
        {
            var rbacService = new RbacService();
            var userService = new UserService();
            var roles = await rbacService.GetRolesAsync();
            var user = new UserData()
            {
                Account = "mmendez2",
                LastName = "M",
                Name = "M",
                MothersLastName = "R",
                Password = "1234",
                IdRole = 1
            };

            await userService.CreateAsync(user);
            Assert.Pass();
        }
    }
}