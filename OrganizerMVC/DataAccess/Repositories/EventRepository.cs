using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using System.Linq;
using System.Web;
using OrganizerMVC.Models;
using OrganizerMVC.Models.Database;

namespace OrganizerMVC.DataAccess.Repositories
{
    public class EventRepository : IRepository<Event, int>
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

        public IEnumerable<Event> Get()
        {
            return Context.Activities.ToList();
        }

        public Event Get(int id)
        {
            return Context.Activities.Find(id);
        }

        public void Add(Event entity)
        {
            Context.Activities.Add(entity);
            Context.SaveChanges();
        }

        public void Remove(Event entity)
        {
            var obj = Context.Activities.Find(entity.EventId);
            Context.Activities.Remove(obj);
            Context.SaveChanges();
        }
    }
}