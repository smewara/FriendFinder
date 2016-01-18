using System.Collections.Generic;
using Bluebeam.Data.Data;
using Bluebeam.Database.Repositories;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace Bluebeam
{
    /// <summary>
    /// Unity Container
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        /// Registering dependencies
        /// </summary>
        /// <param name="config"></param>
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            container.RegisterType<IDictionary<int, User>, Dictionary<int, User>>(new ContainerControlledLifetimeManager(), new InjectionConstructor());
            container.RegisterType<IUserRepo, UserRepo>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFriendRepo, FriendRepo>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFriendNetworkRepo, FriendNetworkRepo>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new ContainerControlledLifetimeManager(), new InjectionConstructor(container.Resolve<IDictionary<int, User>>()));
            container.RegisterType<Controllers.ServiceControllers.FriendsController, Controllers.ServiceControllers.FriendsController>();
            container.RegisterType<Controllers.ServiceControllers.UsersController, Controllers.ServiceControllers.UsersController>();
            
            config.DependencyResolver = new UnityDependencyResolver(container);
            
        }
    }
}