using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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


            _manager = new UserManager(new UserStore(new DataContext()));
            _controller = new AccountController(_manager);

            var user = await _manager.FindAsync(_userViewModel.Email, _userViewModel.Password);
            var identity = await _manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));

            var cp = Mock.Of<System.Security.Claims.ClaimsPrincipal>();
//            c.aClaim(It.IsAny<string>(), It.IsAny<string>()).Returns(true);
            cp.AddIdentity(identity);

            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(x => x.User).Returns(cp);
            var reqContext = new RequestContext(httpContext.Object, new RouteData());

            _controller.ControllerContext = new ControllerContext(reqContext, _controller);

            var mockAuthenticationManager = new Mock<IAuthenticationManager>();
            mockAuthenticationManager.Setup(am => am.SignOut());
            mockAuthenticationManager.Setup(am => am.SignIn());
            _controller.AuthenticationManager = mockAuthenticationManager.Object;

            if (_manager.FindByEmail("test@wp.pl") == null)
            {
                await _manager.CreateAsync(new User { Email = _userViewModel.Email, UserName = _userViewModel.Email }, _userViewModel.Password);
            }
        }

        [Test]
        public async void LoginTest()
        {
            var result = await _controller.Login(_userViewModel, "home/index");
            Assert.IsNotNull(result);

            var loggedUser = _manager.FindByEmail("test@wp.pl");
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
