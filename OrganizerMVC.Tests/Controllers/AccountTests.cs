using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Moq;
using NUnit.Framework;
using OrganizerMVC.Controllers;
using OrganizerMVC.Models;
using OrganizerMVC.Models.Database;
using OrganizerMVC.ViewModels;

namespace OrganizerMVC.Tests.Controllers
{
    public class AccountTests
    {
        [Test]
        public async void LoginTest()
        {
            var manager = new UserManager(new UserStore(new DataContext()));
            var accountController = new AccountController(manager);
            var mockAuthenticationManager = new Mock<IAuthenticationManager>();
            mockAuthenticationManager.Setup(am => am.SignOut());
            mockAuthenticationManager.Setup(am => am.SignIn());
            accountController.AuthenticationManager = mockAuthenticationManager.Object;

            var form = new LoginViewModel
            {
                Email = "tester@wp.pl",
                Password = "tester",
                RememberMe = false
            };

            var result = await accountController.Login(form, "home/index");
            Assert.IsNotNull(result);

            var loggedUser = manager.FindByEmail("tester@wp.pl");
            Assert.IsNotNull(loggedUser);

            Assert.AreEqual("tester@wp.pl", loggedUser.Email);
        }

        [Test]
        public async void RegisterTest()
        {
            var manager = new UserManager(new UserStore(new DataContext()));
            var accountController = new AccountController(manager);
            var mockAuthenticationManager = new Mock<IAuthenticationManager>();
            accountController.AuthenticationManager = mockAuthenticationManager.Object;

            var form = new RegisterViewModel
            {
                Email = "testuser@wp.pl",
                Password = "testuser"
            };

            var result = await accountController.Register(form);
            Assert.IsNotNull(result);

            var loggedUser = manager.FindByEmail("testuser@wp.pl");
            Assert.IsNotNull(loggedUser);
        }

        [Test]
        public void LogOutTest()
        {
            
        }
    }
}
