using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OrganizerMVC.Startup))]
namespace OrganizerMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
