using System.Security.Claims;

namespace OrganizerMVC.Models
{
    public class ClaimsPrincipal : System.Security.Claims.ClaimsPrincipal
    {
        public ClaimsPrincipal(System.Security.Claims.ClaimsPrincipal principal) : base(principal)
        {
            
        }

        public int UserId
        {
            get { return int.Parse(FindFirst( ClaimTypes.Sid ).Value); }
        }
    }
}