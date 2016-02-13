using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OrganizerMVC.Models
{
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        public virtual ICollection<Event> Activities { get; set; }
    }

    public class UserLogin : IdentityUserLogin<int>
    {

    }

    public class UserRole : IdentityUserRole<int>
    {

    }

    public class UserClaim : IdentityUserClaim<int>
    {
        
    }

    public class Role : IdentityRole<int, UserRole>
    {
        
    }
}