using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OrganizerMVC.Models.Database;

namespace OrganizerMVC.Models
{
    public interface IUserStore : IUserStore<User, int>
    {

    }

    public class UserStore : 
        UserStore<User, Role, int, UserLogin, UserRole, UserClaim>, 
        IUserStore
    {
        public UserStore() : base(new DataContext())
        {

        }

        public UserStore(DataContext context) : base(context)
        {

        }
    }
}