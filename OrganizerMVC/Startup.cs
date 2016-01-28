using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using OrganizerMVC.DataAccess;
using OrganizerMVC.Models;

[assembly: OwinStartupAttribute(typeof(OrganizerMVC.Startup))]
namespace OrganizerMVC
{
    public partial class Startup
    {
        public static Func<UserManager<User>> UserManagerFactory { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/auth/login")
            });

            UserManagerFactory = () =>
            {
                var usermanager = new UserManager<User>(new UserStore<User>(new DataContext()));
                // allow alphanumeric characters in username
                usermanager.UserValidator = new UserValidator<User>(usermanager)
                {
                    AllowOnlyAlphanumericUserNames = true,
                };

                usermanager.ClaimsIdentityFactory = new UserClaimsIdentityFactory();

                return usermanager;
            };
        }
    }
}
