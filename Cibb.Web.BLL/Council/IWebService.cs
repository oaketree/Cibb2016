using Cibb.Web.BLL.Model;
using Cibb.Web.Contract.Model;
using System.Linq;

namespace Cibb.Web.BLL.Council
{
   public interface IWebService
    {
       ViewModels<BranchDirector> GetBranchDirector(int pageSize, int page);
       IQueryable<Link> GetBranchCompany();
       ViewModels<BranchMember> GetBranchMember(int pageSize, int page);
       BranchMember GetCompanyDetail(int id); 
    }
}
