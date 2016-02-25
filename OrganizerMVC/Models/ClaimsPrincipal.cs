using System.Security.Claims;

namespace OrganizerMVC.Models
{
//    public interface IClaimsPrincipal
//    {
//    }

    public class ClaimsPrincipal : System.Security.Claims.ClaimsPrincipal//, IClaimsPrincipal
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