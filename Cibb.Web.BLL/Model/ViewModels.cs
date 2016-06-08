using System.Linq;

namespace Cibb.Web.BLL.Model
{
    public class ViewModels<T>
    {
        public IQueryable<T> data { get; set; }
        public PagingInfo pagingInfo { get; set; }
        public int CurrentCategory { get; set; }
    }
}
