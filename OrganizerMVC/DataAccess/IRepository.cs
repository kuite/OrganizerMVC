using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrganizerMVC.Models.Database;

namespace OrganizerMVC.DataAccess
{
    public interface IRepository<TEnt, in TPk> where TEnt : class
    {
        DataContext CurrentContext { get; set; }
        IEnumerable<TEnt> Get();
        TEnt Get(TPk id);
        void Add(TEnt entity);
        void Remove(TEnt entity);
    }
}