using System.Collections.Generic;
using OrganizerMVC.Models;

namespace OrganizerMVC.Services
{
    public interface IEventsService
    {
        void Add(Event evt);
        void Delete(int id);
        void Update(Event evt);
        Event Get(int id);
        IEnumerable<Event> GetUserEvents(int userId);
    }
}