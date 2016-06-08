using Cibb.Web.Contract.Model;
using System.Linq;

namespace Cibb.Web.BLL.FriendLinks
{
    public interface IWebService
    {
        IQueryable<FriendLink> GetPeople(int type, int num);
        IQueryable<FriendLink> GetCompany(int num);
    }
}
