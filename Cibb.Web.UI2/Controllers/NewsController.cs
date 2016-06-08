using Cibb.Web.BLL.News;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cibb.Web.UI2.Controllers
{
    public class NewsController : Controller
    {
        // GET: News

        private IWebService webservice;

        public NewsController(IWebService webservice)
        {
            this.webservice = webservice;
        }
        /// <summary>
        /// 首页新闻图
        /// </summary>
        /// <returns></returns>
        public PartialViewResult NewsImageForHome()
        {
            return PartialView(webservice.GetHomeImg(8));
        }
        /// <summary>
        /// 首页顶部新闻
        /// </summary>
        /// <returns></returns>
        public PartialViewResult NewsAllForHome()
        {
            return PartialView(webservice.GetNewsAll(9));
        }
        /// <summary>
        /// 首页新闻列表
        /// </summary>
        /// <param name="count">新闻条数</param>
        /// <param name="cid">新闻分类号</param>
        /// <returns>新闻视图</returns>
        public PartialViewResult TitleListForHome(int count, int cid) {
            return PartialView(webservice.GetTitleList(count,cid));
        }

        /// <summary>
        /// 导航名称
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<ActionResult> MenuAsync(int category)
        {
            //string tags = string.Empty;
            string tags = await webservice.GetMenuAsync(category);
            return Content(tags);
        }

        public ActionResult Menu(int category)
        {
            string tags = webservice.GetMenu(category);
            return Content(tags);
        }



        /// <summary>
        /// 新闻详细页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(int id)
        {
            var news=webservice.GetNews(id);
            if (news!= null)
            {
                return View(news);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
          /// <summary>
        /// 新闻热点
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Hot() {
            return PartialView(webservice.GetHot(8));
        }

        public PartialViewResult Right(int category)
        {
            return PartialView(webservice.GetRight(category));
        }
        public PartialViewResult RightList(int category) 
        {
            return PartialView(webservice.GetRightList(category,8));
        }

        public PartialViewResult RightForIndex()
        {
            return PartialView("Right", webservice.GetRightForIndex(2));
        }

        public ActionResult Index(int page = 1)
        {
            return View(webservice.GetNewsIndex(10,page));
        }

        public ActionResult List(int category, int page = 1)
        {
            var viewModel=webservice.GetNewsList(category,10,page);
            if(viewModel!=null)
                return View(viewModel);
            else
                return RedirectToAction("Index", "News");
        }

    }
}