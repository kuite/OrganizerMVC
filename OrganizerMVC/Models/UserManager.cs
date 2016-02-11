using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace OrganizerMVC.Models
{
    public class UserManager : UserManager<User, int>
    {
        public UserManager(IUserStore store) : base(store)
        {

        }
    }
}