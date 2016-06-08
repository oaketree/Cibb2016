using Cibb.Core.DAL;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CMS.Web
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
            _unityContainer.RegisterType<BLL.Admin.IWebService, BLL.Admin.WebService>();
            _unityContainer.RegisterType<BLL.User.IWebService, BLL.User.WebService>();
            _unityContainer.RegisterType<BLL.Forum.IWebService, BLL.Forum.WebService>();
            _unityContainer.RegisterType<BLL.Magazine.IWebService, BLL.Magazine.WebService>();
            _unityContainer.RegisterType<BLL.Data.IWebService, BLL.Data.WebService>();



            //container.RegisterInstance<IFilterProvider>("FilterProvider", new UnityFilterAttributeFilterProvider(container));
            _unityContainer.RegisterType<IWebRepository<Contract.Models.Admin.Role>,WebRepository<Contract.Models.Admin.Role, Contract.Models.Admin.WebDBContext>>();
            _unityContainer.RegisterType<IWebRepository<Contract.Models.Admin.Authorise>, WebRepository<Contract.Models.Admin.Authorise, Contract.Models.Admin.WebDBContext>>();
            _unityContainer.RegisterType<IWebRepository<Contract.Models.Admin.Admins>, WebRepository<Contract.Models.Admin.Admins, Contract.Models.Admin.WebDBContext>>();
            _unityContainer.RegisterType<IWebRepository<Contract.Models.Admin.AdminRole>, WebRepository<Contract.Models.Admin.AdminRole, Contract.Models.Admin.WebDBContext>>();
            _unityContainer.RegisterType<IWebRepository<Contract.Models.Admin.RoleAuthorise>, WebRepository<Contract.Models.Admin.RoleAuthorise, Contract.Models.Admin.WebDBContext>>();

            _unityContainer.RegisterType<IWebRepository<Contract.Models.User.URole>, WebRepository<Contract.Models.User.URole, Contract.Models.User.WebDBContext>>();
            _unityContainer.RegisterType<IWebRepository<Contract.Models.User.UAuthorise>, WebRepository<Contract.Models.User.UAuthorise, Contract.Models.User.WebDBContext>>();
            _unityContainer.RegisterType<IWebRepository<Contract.Models.User.Users>, WebRepository<Contract.Models.User.Users, Contract.Models.User.WebDBContext>>();
            _unityContainer.RegisterType<IWebRepository<Contract.Models.User.UserRole>, WebRepository<Contract.Models.User.UserRole, Contract.Models.User.WebDBContext>>();
            _unityContainer.RegisterType<IWebRepository<Contract.Models.User.URoleAuthorise>, WebRepository<Contract.Models.User.URoleAuthorise, Contract.Models.User.WebDBContext>>();
            _unityContainer.RegisterType<IWebRepository<Contract.Models.User.Company>, WebRepository<Contract.Models.User.Company, Contract.Models.User.WebDBContext>>();
            _unityContainer.RegisterType<IWebRepository<Contract.Models.User.UserDetail>, WebRepository<Contract.Models.User.UserDetail, Contract.Models.User.WebDBContext>>();
            _unityContainer.RegisterType<IWebRepository<Contract.Models.User.Person>, WebRepository<Contract.Models.User.Person, Contract.Models.User.WebDBContext>>();



            _unityContainer.RegisterType<IWebRepository<Contract.Models.Forum.Forums>, WebRepository<Contract.Models.Forum.Forums, Contract.Models.Forum.WebDBContext>>();
            _unityContainer.RegisterType<IWebRepository<Contract.Models.Forum.ForumCategory>, WebRepository<Contract.Models.Forum.ForumCategory, Contract.Models.Forum.WebDBContext>>();
            _unityContainer.RegisterType<IWebRepository<Contract.Models.Forum.Report>, WebRepository<Contract.Models.Forum.Report, Contract.Models.Forum.WebDBContext>>();

            _unityContainer.RegisterType<IWebRepository<Contract.Models.Magazine.GyglArticle>, WebRepository<Contract.Models.Magazine.GyglArticle, Contract.Models.Magazine.WebDBContext>>();
            _unityContainer.RegisterType<IWebRepository<Contract.Models.Magazine.GyglCategory>, WebRepository<Contract.Models.Magazine.GyglCategory, Contract.Models.Magazine.WebDBContext>>();
            _unityContainer.RegisterType<IWebRepository<Contract.Models.Magazine.Gygl>, WebRepository<Contract.Models.Magazine.Gygl, Contract.Models.Magazine.WebDBContext>>();
            _unityContainer.RegisterType<IWebRepository<Contract.Models.Magazine.GyglAssist>, WebRepository<Contract.Models.Magazine.GyglAssist, Contract.Models.Magazine.WebDBContext>>();
            _unityContainer.RegisterType<IWebRepository<Contract.Models.Magazine.GyglCategoryRelation>, WebRepository<Contract.Models.Magazine.GyglCategoryRelation, Contract.Models.Magazine.WebDBContext>>();
            _unityContainer.RegisterType<IWebRepository<Contract.Models.Magazine.GyglComment>, WebRepository<Contract.Models.Magazine.GyglComment, Contract.Models.Magazine.WebDBContext>>();
            _unityContainer.RegisterType<IWebRepository<Contract.Models.Magazine.GyglImage>, WebRepository<Contract.Models.Magazine.GyglImage, Contract.Models.Magazine.WebDBContext>>();

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
