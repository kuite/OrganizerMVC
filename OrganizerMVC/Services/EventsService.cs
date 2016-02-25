﻿using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using OrganizerMVC.Models;
using OrganizerMVC.Models.Database;

namespace OrganizerMVC.Services
{
    public class EventsService : IEventsService
    {
        private readonly DataContext _context;

        public EventsService(DataContext context)
        {
            _context = context;
        }

        public void Add(Event evt)
        {
            _context.Events.Add(evt);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = _context.Events.Find(id);
            _context.Events.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Event evt)
        {
            _context.Set<Event>().AddOrUpdate(evt);
            _context.SaveChanges();
        }

        public Event Get(int id)
        {
            return _context.Events.Find(id);
        }

        public IEnumerable<Event> GetUserEvents(int userId)
        {
            return _context.Events.Where(e => e.User.Id == userId);
        }
    }
}