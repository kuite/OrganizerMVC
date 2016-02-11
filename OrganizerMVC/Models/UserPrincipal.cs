using System.Security.Claims;

namespace OrganizerMVC.Models
{
    public class UserPrincipal : System.Security.Claims.ClaimsPrincipal
    {
        public UserPrincipal(System.Security.Claims.ClaimsPrincipal principal) : base(principal)
        {

        }

        public string Name
        {
            get
            {
                return FindFirst(ClaimTypes.Name).Value;
            }
        }

        public string Role
        {
            get
            {
                return FindFirst(ClaimTypes.Role).Value;
            }
        }
    }
}