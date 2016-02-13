using System.Web.Mvc;
using Microsoft.Practices.Unity;
using OrganizerMVC.DataAccess;
using OrganizerMVC.DataAccess.Repositories;
using OrganizerMVC.Models;
using Unity.Mvc5;

namespace OrganizerMVC
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            //Register the Repository in the Unity Container

            container.RegisterType<IRepository<Event, int>, EventRepository>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}