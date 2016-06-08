using CMS.BLL.Forum.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CMS.BLL.Forum
{
    public interface IWebService
    {
        Dictionary<string, object> GetForumList(int pageIndex, int pageSize);
        Dictionary<string, object> GetCategoryList(int pageIndex, int pageSize);
        Dictionary<string, object> GetArticleList(string title, int? forumid, int? categoryid,int pageIndex, int pageSize);
        void addForum(ForumModel fm);
        ForumModel GetSelectForum(int id);
        void updateForum(ForumModel fm);
        List<SelectListItem> GetForum();
        List<SelectListItem> GetCategory();
        void AddCategory(CategoryModel cm);
        CategoryModel GetSelectCategory(int id);
        void updateCategory(CategoryModel cm);
        void delCategory(int id);
        List<object> getForumTree();
        List<object> getCategoryTree();
        List<SelectListItem> GetSubCategory(int f, int c);
        void AddArticle(ArticleModel am);
        Dictionary<string, object> GetUpload();
        void delArticle(int id);
        ArticleModel getArticle(int id);
        void updateArticle(ArticleModel am);
        void delImg(int id);
        void delFile(int id);
    }
}
