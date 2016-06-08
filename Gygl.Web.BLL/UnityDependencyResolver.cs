using Cibb.Core.DAL;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Gygl.Web.BLL
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        IUnityContainer _unityContainer;
        public UnityDependencyResolver()
        {
            _unityContainer = new UnityContainer();
            AddBindings();
        }

        private void AddBindings()
        {
            _unityContainer.RegisterType<Register.IWebService, Register.WebService>(new ContainerControlledLifetimeManager())
            .RegisterType<Contribute.IWebService, Contribute.WebService>(new ContainerControlledLifetimeManager());
                
            _unityContainer.RegisterType<IWebRepository<Contract.Models.User.Users>, WebRepository<Contract.Models.User.Users, Contract.Models.User.WebDBContext>>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IWebRepository<Contract.Models.User.UserDetail>, WebRepository<Contract.Models.User.UserDetail, Contract.Models.User.WebDBContext>>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IWebRepository<Contract.Models.User.Authorise>, WebRepository<Contract.Models.User.Authorise, Contract.Models.User.WebDBContext>>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IWebRepository<Contract.Models.User.UserRole>, WebRepository<Contract.Models.User.UserRole, Contract.Models.User.WebDBContext>>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IWebRepository<Contract.Models.User.RoleAuthorise>, WebRepository<Contract.Models.User.RoleAuthorise, Contract.Models.User.WebDBContext>>(new ContainerControlledLifetimeManager());

            _unityContainer.RegisterType<IWebRepository<Contract.Models.Contribute.Contributes>, WebRepository<Contract.Models.Contribute.Contributes, Contract.Models.Contribute.WebDBContext>>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IWebRepository<Contract.Models.Contribute.Manuscript>, WebRepository<Contract.Models.Contribute.Manuscript, Contract.Models.Contribute.WebDBContext>>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IWebRepository<Contract.Models.Contribute.Message>, WebRepository<Contract.Models.Contribute.Message, Contract.Models.Contribute.WebDBContext>>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IWebRepository<Contract.Models.Contribute.Supplement>, WebRepository<Contract.Models.Contribute.Supplement, Contract.Models.Contribute.WebDBContext>>(new ContainerControlledLifetimeManager());
        }

        public object GetService(Type serviceType)
        {
            return (serviceType.IsClass && !serviceType.IsAbstract) ||
            _unityContainer.IsRegistered(serviceType) ?
            _unityContainer.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return (serviceType.IsClass && !serviceType.IsAbstract) ||
            _unityContainer.IsRegistered(serviceType) ?
            _unityContainer.ResolveAll(serviceType) : new object[] { };
        }
    }
}
