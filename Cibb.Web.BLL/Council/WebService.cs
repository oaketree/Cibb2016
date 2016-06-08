using Cibb.Core.DAL;
using Cibb.Web.BLL.Model;
using Cibb.Web.Contract.Model;
using System.Linq;

namespace Cibb.Web.BLL.Council
{
    public class WebService:IWebService
    {
        private IWebRepository<BranchDirector> BranchDirectors;
        private IWebRepository<BranchMember> BranchMembers;
        public WebService(IWebRepository<BranchDirector> BranchDirectors, IWebRepository<BranchMember> BranchMembers) 
        {
            this.BranchDirectors = BranchDirectors;
            this.BranchMembers = BranchMembers;
        }


        public ViewModels<BranchDirector> GetBranchDirector(int pageSize, int page)
        {
            var viewData = this.BranchDirectors.FindAllByPage(null, o => o.sortid, pageSize, page);
            var count = this.BranchDirectors.FindAll(null).Count();
            ViewModels<BranchDirector> viewModel = new ViewModels<BranchDirector>
            {
                data = viewData,
                pagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemPerPage = pageSize,
                    TotalItems = count
                }
            };
            return viewModel;
        }
        public IQueryable<Link> GetBranchCompany() 
        { 
            var bc = this.BranchMembers.FindList(n=>n.type==1,o => o.sortid,0)
                .Select(s => new Link
                {
                    linkid = s.ID,
                    title = s.CompanyName
                });
            return bc;
        }
        public ViewModels<BranchMember> GetBranchMember(int pageSize, int page)
        {
            var viewData = this.BranchMembers.FindAllByPage2(null, o => o.province, o => o.sortid, pageSize, page);
            var count = this.BranchMembers.FindAll(null).Count();
            ViewModels<BranchMember> viewModel = new ViewModels<BranchMember>
            {
                data = viewData,
                pagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemPerPage = pageSize,
                    TotalItems = count
                }
            };
            return viewModel;
        }
        public BranchMember GetCompanyDetail(int id) 
        {
            var bm = this.BranchMembers.GetById(id);
            if (bm != null)
                return this.BranchMembers.GetById(id);
            else
                return null;
        }


    }
}
