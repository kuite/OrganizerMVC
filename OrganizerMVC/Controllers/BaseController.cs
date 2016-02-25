using System.Security.Claims;
using System.Web.Mvc;
using OrganizerMVC.Models;
using ClaimsPrincipal = OrganizerMVC.Models.ClaimsPrincipal;

namespace OrganizerMVC.Controllers
{
    public abstract class BaseController : Controller
    {
        private IClaimsPrincipal _currentUser;

        public IClaimsPrincipal CurrentUser
        {
            get {
                return _currentUser ??
                       (_currentUser = new ClaimsPrincipal((System.Security.Claims.ClaimsPrincipal) this.User));
            }
            set { _currentUser = value; }
        }

        public BaseController()
        {

        }
    }
}