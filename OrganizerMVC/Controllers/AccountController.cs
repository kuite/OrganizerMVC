using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using OrganizerMVC.DataAccess;
using OrganizerMVC.Models;

namespace OrganizerMVC.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        //private readonly UserManager<User> _userManager;

        private readonly AppUserManager _userManager;

        public AccountController()
            : this(new AppUserManager(new AppUserStore(new DataContext())))
        {
        }

        public AccountController(AppUserManager userManager)
        {
            this._userManager = userManager;
        }

//        public AccountController() : this (Startup.UserManagerFactory.Invoke())
//        {
//
//        }
//
//        public AccountController(UserManager<User> userManager)
//        {
//            this._userManager = userManager;
//        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindAsync(model.Email, model.Password);

            if (user != null)
            {
                //await SignIn(user);
                await SignInAsync(user, model.RememberMe);
                return Redirect(GetRedirectUrl("calendar/index"));
            }

            // user authN failed
            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new AppUser
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
               // await SignIn(user);
                await SignInAsync(user, false);
                return RedirectToAction("index", "home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return View();
        }


        public ActionResult LogOut()
        {
            GetAuthenticationManager().SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("index", "home");
        }

        #region Helpers

//        private async Task SignIn(User user)
//        {
//            var identity = await _userManager.CreateIdentityAsync(
//                user, DefaultAuthenticationTypes.ApplicationCookie);
//
//            GetAuthenticationManager().SignIn(identity);
//        }

        private async Task SignInAsync(AppUser user, bool isPersistent)
        {
            GetAuthenticationManager().SignOut(DefaultAuthenticationTypes.ExternalCookie);
            ClaimsIdentity identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            // Extend identity claims
            identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));

            GetAuthenticationManager().SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private IAuthenticationManager GetAuthenticationManager()
        {
            var ctx = Request.GetOwinContext();
            return ctx.Authentication;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "home");
            }

            return returnUrl;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}