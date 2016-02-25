using System.Threading.Tasks;
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
        private AccountController _controller;

        private UserManager _manager;

        private LoginViewModel _userViewModel;

        [SetUp]
        public void Setup()
        {
            SetupAsync().Wait();
        }

        public async Task SetupAsync()
        {
            _userViewModel = new LoginViewModel
            {
                Email = "user@wp.pl",
                Password = "useruser",
                RememberMe = false
            };
            var context = new DataContext();
            _manager = new UserManager(new UserStore(context));
            _controller = new AccountController(_manager);

            var user = await _manager.FindAsync(_userViewModel.Email, _userViewModel.Password);
            if (user == null)
            {
                await _manager.CreateAsync(new User { Email = _userViewModel.Email, UserName = _userViewModel.Email }, _userViewModel.Password);
            }
            var mockCp = new Mock<IClaimsPrincipal>();
            if (user != null) mockCp.SetupGet(cp => cp.UserId).Returns(user.Id);
            _controller.CurrentUser = mockCp.Object;

            var mockAuthenticationManager = new Mock<IAuthenticationManager>();
            mockAuthenticationManager.Setup(am => am.SignOut());
            mockAuthenticationManager.Setup(am => am.SignIn());
            _controller.AuthenticationManager = mockAuthenticationManager.Object;
        }

        [Test]
        public async void LoginTest()
        {
            var result = await _controller.Login(_userViewModel, "home/index");
            Assert.IsNotNull(result);

            var loggedUser = _manager.FindByEmail(_userViewModel.Email);
            Assert.IsNotNull(loggedUser);
        }

        [Test]
        public async void RegisterTest()
        {
            var form = new RegisterViewModel
            {
                Email = "registertest@wp.pl",
                Password = "testuser",
                ConfirmPassword = "testuser"
            };
            await _controller.Register(form);

            var loggedUser = _manager.FindByEmail("registertest@wp.pl");
            Assert.IsNotNull(loggedUser);
        }

        [Test]
        public void LogOutTest()
        {
            _controller.LogOut();
            Assert.IsNull(_controller.AuthenticationManager.User);
        }
    }
}
