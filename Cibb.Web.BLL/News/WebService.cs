using Cibb.Core.DAL;
using Cibb.Web.BLL.Model;
using Cibb.Web.Contract.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cibb.Web.BLL.News
{

    public class WebService:IWebService
    {
        private IWebRepository<New> news;
        private IWebRepository<NewsColumn> newsColumns;

        private int[] columnsPid = { 102, 103, 104, 105, 106, 107 };//协会网站总目录
        private int[] columns = { 108, 109, 112, 113, 114, 116, 117, 119, 120, 124, 126, 155, 177, 178 };//协会网站目录

        public WebService(IWebRepository<New> news, IWebRepository<NewsColumn> newsColumns)
        {
            this.news = news;
            this.newsColumns = newsColumns;
        }

        /// <summary>
        /// 获取新闻图片
        /// </summary>
        /// <returns></returns>
        public IQueryable<Link> GetHomeImg(int num)
        {
            var oj = this.news.FindList(w => w.pic != string.Empty && w.picarea == 2, o => o.RegDate, num).Select(s => new Link { title = s.Title, pic = s.pic, linkid = s.newsid });
            return oj;
        }
        /// <summary>
        /// 首页顶部新闻
        /// </summary>
        /// <returns></returns>
        public IQueryable<Link> GetNewsAll(int num)
        {
            var newsList = news.FindList(n => columns.Contains(n.ColumnID), o => o.RegDate,num)
                .Select(s => new Link
               {
                   title = s.Title,
                   linkid = s.newsid,
                   columnId = s.ColumnID,
                   columnName = s.NewsColumn.ColumnName
               });
            return newsList;
        }

        /// <summary>
        /// 首页新闻列表
        /// </summary>
        /// <param name="count">新闻条数</param>
        /// <param name="cid">新闻分类号</param>
        /// <returns>新闻视图</returns>
        public IQueryable<Link> GetTitleList(int count, int cid)
        {
            var newsList = news.FindList(n => n.ColumnID == cid, o => o.RegDate, count)
                 .Select(s => new Link
                 {
                     title = s.Title,
                     linkid = s.newsid,
                     columnName = s.NewsColumn.ColumnName,
                     date = s.RegDate
                 });
            return newsList;
        }

        /// <summary>
        /// 导航名称
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public string GetMenu(int category)
        {
            return this.newsColumns.GetById(category).ColumnName;
        }

        public async Task<string> GetMenuAsync(int category)
        {
            return await Task.Run<string>(() =>
            {
                return this.newsColumns.GetById(category).ColumnName;
            });
        }


        /// <summary>
        /// 新闻详细页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public New GetNews(int id) 
        {
            var news = this.news.GetById(id);
            if (news != null)
            {
                news.Hits = news.Hits + 1;
                this.news.Update(news);
                return news;
            }
            else
                return null;
        }

        public IQueryable<Link> GetHot(int num)
        {
            var newsList = news.FindList(n => columns.Contains(n.ColumnID) && n.RegDate.Value.Year == DateTime.Now.Year, o => o.Hits, num)
                 .Select(s => new Link
                 {
                     title = s.Title,
                     linkid = s.newsid,
                     columnName = s.NewsColumn.ColumnName,
                     date = s.RegDate
                 });
            return newsList;
        }
        public IQueryable<NewsColumn> GetRight(int category)
        {
            var pid = this.newsColumns.GetById(category).ColumnParentID;
            var r = this.newsColumns.FindAll(n => n.ColumnParentID == pid && n.ColumnID != category);
            return r;
        }

        public IQueryable<Link> GetRightList(int category,int num)
        {
            var rl = this.news.FindList(n => n.ColumnID == category, o => o.RegDate,num)
                .Select(s => new Link
                {
                    title = s.Title,
                    linkid = s.newsid,
                    columnId = s.ColumnID,
                    date = s.RegDate
                });
            return rl;
        }
        public IQueryable<NewsColumn> GetRightForIndex(int num)
        {
            var ri = this.newsColumns.FindList(n => columnsPid.Contains(n.ColumnParentID.Value), o => Guid.NewGuid(), num);
            return ri;
        }
        public ViewModels<New> GetNewsIndex(int pageSize, int page)
        {
            var viewData= this.news.FindAllByPage(n => columns.Contains(n.ColumnID), o => o.RegDate, pageSize, page);
            var count=this.news.FindAll(n => columns.Contains(n.ColumnID)).Count();

            ViewModels<New> viewModel = new ViewModels<New>
            {
                data = viewData,
                pagingInfo = new PagingInfo { 
                    CurrentPage=page,
                    ItemPerPage=pageSize,
                    TotalItems=count
                }
            };
            return viewModel;
        }
        public ViewModels<New> GetNewsList(int category,int pageSize, int page) 
        {
            var p = this.newsColumns.GetById(category);
            if (p != null)
            {
                if (columns.Contains(p.ColumnID))
                {
                    var viewData = this.news.FindAllByPage(n => n.ColumnID == category, o => o.RegDate, pageSize, page);
                    var count = this.news.FindAll(n => n.ColumnID == category).Count();
                    ViewModels<New> viewModel = new ViewModels<New>
                    {
                        data = viewData,
                        pagingInfo = new PagingInfo
                        {
                            CurrentPage = page,
                            ItemPerPage = pageSize,
                            TotalItems = count
                        },
                        CurrentCategory = category
                    };
                    return viewModel;
                }
                else
                    return null;
            }
            else
                return null;
        }





    }
}
