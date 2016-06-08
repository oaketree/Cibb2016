using Cibb.Core.DAL;
using Cibb.Web.Contract;
using Cibb.Web.Contract.Model;
using System.Linq;

namespace Cibb.Web.BLL.FriendLinks
{
    public class WebService:IWebService
    {
        private IWebRepository<FriendLink> FriendLinks;
        public WebService(IWebRepository<FriendLink> FriendLinks) 
        {
            this.FriendLinks = FriendLinks;
        }

        public IQueryable<FriendLink> GetPeople(int type,int num) 
        {
            IQueryable<FriendLink> peopleList = null;
            if (type == 1) {
               peopleList=this.FriendLinks.FindList(n => n.LinkType == "9" || n.LinkType == "11", o => o.SortID, num);
            }
            if (type == 2) {
                peopleList = this.FriendLinks.FindList(n => n.LinkType == "12", o => o.SortID, num);
            }
            return peopleList;
        }
        public IQueryable<FriendLink> GetCompany(int num) {
            var c = this.FriendLinks.FindList(n => n.LinkType == "7", o => o.SortID,num);
            return c;
        }
    }
}
