using Kartverk.Mvc.Controllers;
using Kartverk.Mvc.DataAccess.Entities;
using Kartverk.Mvc.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kartverk.Mvc.Tests.Controllers
{
    public class HomeControllerRepositoryTests
    {
        [Fact]
        public async Task GetUser_ReturnsCorrectUser()
        {
            var userRepository = Substitute.For<IUserRepository>();
            userRepository.GetUser("derp")
                          .Returns(new User { Name = "TESTBRUKER" });

            var unitUnderTest = new HomeController(Substitute.For<ILogger<HomeController>>(), userRepository);
            var result = await unitUnderTest.GetUser("derp") as OkObjectResult;

            var model = result.Value as User;

            Assert.Equal("TESTBRUKER", model.Name);
        }
    }


    
}
