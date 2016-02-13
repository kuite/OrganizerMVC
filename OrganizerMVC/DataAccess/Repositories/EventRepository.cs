using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using System.Linq;
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
            return Context.Events.ToList();
        }

        public Event Get(int id)
        {
            return Context.Events.Find(id);
        }

        public void Add(Event entity)
        {
            Context.Events.Add(entity);
            Context.SaveChanges();
        }

        public void Remove(Event entity)
        {
            var obj = Context.Events.Find(entity.EventId);
            Context.Events.Remove(obj);
            Context.SaveChanges();
        }
    }
}