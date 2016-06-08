using Cibb.Core.DAL;
using Cibb.Web.Contract.Model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace Cibb.Web
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private Ninject.IKernel kernel;
        public NinjectDependencyResolver()
        {
            this.kernel = new Ninject.StandardKernel();
            this.AddBindings();
        }

        private void AddBindings()
        {
            this.kernel.Bind<Cibb.Web.BLL.News.IWebService>()
                 .To<Cibb.Web.BLL.News.WebService>();
            this.kernel.Bind<Cibb.Web.BLL.Council.IWebService>()
                 .To<Cibb.Web.BLL.Council.WebService>();
            this.kernel.Bind<Cibb.Web.BLL.FriendLinks.IWebService>()
                 .To<Cibb.Web.BLL.FriendLinks.WebService>();
            this.kernel.Bind<Cibb.Web.BLL.Reports.IWebService>()
                 .To<Cibb.Web.BLL.Reports.WebService>();
            this.kernel.Bind<Cibb.Web.BLL.Registration.IWebService>()
                 .To<Cibb.Web.BLL.Registration.WebService>();

            this.kernel.Bind<IWebRepository<New>>()
                .To<WebRepository<New, Cibb.Web.Contract.WebDBContext>>().WhenInjectedInto<Cibb.Web.BLL.News.WebService>();
             this.kernel.Bind<IWebRepository<NewsColumn>>()
                .To<WebRepository<NewsColumn, Cibb.Web.Contract.WebDBContext>>().WhenInjectedInto<Cibb.Web.BLL.News.WebService>();
             this.kernel.Bind<IWebRepository<BranchDirector>>()
                 .To<WebRepository<BranchDirector, Cibb.Web.Contract.WebDBContext>>().WhenInjectedInto<Cibb.Web.BLL.Council.WebService>();
             this.kernel.Bind<IWebRepository<BranchMember>>()
                  .To<WebRepository<BranchMember, Cibb.Web.Contract.WebDBContext>>().WhenInjectedInto<Cibb.Web.BLL.Council.WebService>();
             this.kernel.Bind<IWebRepository<FriendLink>>()
                   .To<WebRepository<FriendLink,Cibb.Web.Contract.WebDBContext>>().WhenInjectedInto<Cibb.Web.BLL.FriendLinks.WebService>();

             this.kernel.Bind<IWebRepository<Users>>()
                    .To<WebRepository<Users, Cibb.Web.Contract.WebDBContext>>();
            this.kernel.Bind<IWebRepository<UserDetail>>()
                    .To<WebRepository<UserDetail, Cibb.Web.Contract.WebDBContext>>();
            this.kernel.Bind<IWebRepository<UserRole>>()
                    .To<WebRepository<UserRole, Cibb.Web.Contract.WebDBContext>>();
             this.kernel.Bind<IWebRepository<Role>>()
                    .To<WebRepository<Role, Cibb.Web.Contract.WebDBContext>>();
             this.kernel.Bind<IWebRepository<Authorise>>()
                    .To<WebRepository<Authorise, Cibb.Web.Contract.WebDBContext>>();
             this.kernel.Bind<IWebRepository<RoleAuthorise>>()
                    .To<WebRepository<RoleAuthorise, Cibb.Web.Contract.WebDBContext>>();

             this.kernel.Bind<IWebRepository<Forums>>()
                    .To<WebRepository<Forums, Cibb.Web.Contract.WebDBContext>>();
             this.kernel.Bind<IWebRepository<ForumCategory>>()
                    .To<WebRepository<ForumCategory, Cibb.Web.Contract.WebDBContext>>();
             this.kernel.Bind<IWebRepository<Report>>()
                     .To<WebRepository<Report, Cibb.Web.Contract.WebDBContext>>();


        }

        public object GetService(Type serviceType)
        {
            return this.kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.kernel.GetAll(serviceType);
        }
    }
}
