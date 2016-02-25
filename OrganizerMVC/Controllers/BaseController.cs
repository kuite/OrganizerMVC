using System.Security.Claims;
using System.Web.Mvc;
using OrganizerMVC.Models;
using ClaimsPrincipal = OrganizerMVC.Models.ClaimsPrincipal;

namespace OrganizerMVC.Controllers
{
    public abstract class BaseController : Controller
    {
        public ClaimsPrincipal CurrentUser
        {
            get { return new ClaimsPrincipal((System.Security.Claims.ClaimsPrincipal)this.User); }
            set { CurrentUser = value; }
        }

        public BaseController()
        {

        }
    }
}