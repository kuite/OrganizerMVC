using System.Security.Claims;

namespace OrganizerMVC.Models
{
    public interface IClaimsPrincipal
    {
        int UserId { get; }
    }

    public class ClaimsPrincipal : System.Security.Claims.ClaimsPrincipal, IClaimsPrincipal
    {
        private int _userId;

        public ClaimsPrincipal(System.Security.Claims.ClaimsPrincipal principal) : base(principal)
        {
            
        }

        public int UserId
        {
            get
            {
                if (_userId == 0)
                {
                    _userId = int.Parse(FindFirst(ClaimTypes.Sid).Value);
                }
                return _userId;
            }
//            set { _userId = value; }
        }
    }
}