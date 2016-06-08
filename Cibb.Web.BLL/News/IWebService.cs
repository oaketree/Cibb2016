using Cibb.Web.BLL.Model;
using Cibb.Web.Contract.Model;
using System.Linq;
using System.Threading.Tasks;

namespace Cibb.Web.BLL.News
{
    public interface IWebService
    {
        IQueryable<Link> GetHomeImg(int num);

        IQueryable<Link> GetNewsAll(int num);

        IQueryable<Link> GetTitleList(int count, int cid);

        string GetMenu(int category);
        Task<string> GetMenuAsync(int category);

        New GetNews(int id);
        IQueryable<Link> GetHot(int num);
        IQueryable<NewsColumn> GetRight(int category);
        IQueryable<Link> GetRightList(int category, int num);
        IQueryable<NewsColumn> GetRightForIndex(int num);
        ViewModels<New> GetNewsIndex(int pageSize, int page = 1);
        ViewModels<New> GetNewsList(int category, int pageSize, int page);
    }
}
