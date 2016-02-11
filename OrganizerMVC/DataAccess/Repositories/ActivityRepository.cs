using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using System.Linq;
using System.Web;
using OrganizerMVC.Models;
using OrganizerMVC.Models.Database;

namespace OrganizerMVC.DataAccess.Repositories
{
    public class ActivityRepository : IRepository<Activity, int>
    {
        [Dependency]
        public DataContext Context { get; set; }

        public DataContext CurrentContext
        {
            get
            {
                return Context;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                CurrentContext = value;
            }
        }

        public IEnumerable<Activity> Get()
        {
            return Context.Activities.ToList();
        }

        public Activity Get(int id)
        {
            return Context.Activities.Find(id);
        }

        public void Add(Activity entity)
        {
            Context.Activities.Add(entity);
            Context.SaveChanges();
        }

        public void Remove(Activity entity)
        {
            var obj = Context.Activities.Find(entity.ActivityId);
            Context.Activities.Remove(obj);
            Context.SaveChanges();
        }
    }
}