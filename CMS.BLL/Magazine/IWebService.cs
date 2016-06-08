using CMS.BLL.Magazine.Models;
using System.Collections.Generic;

namespace CMS.BLL.Magazine
{
    public interface IWebService
    {
        object GetFileUpload(int sortid, string guid);
        object GetCategory(int pageIndex, int pageSize);
        void addCategory(CategoryModel cm);
        CategoryModel getCategory(int id);
        void CategoryEdit(CategoryModel cm);
        void setSort(string sort);
        void CategoryDel(int id);
        object GetMagazineList(int pageIndex, int pageSize);
        List<object> GetYear();
        List<object> GetPeriod();
        List<object> GetCategory();
        List<object> GetMagazine();
        List<CategorySort> GetMagazineCategory(int mid);
        List<CategorySort> GetMagazineCategory2(int y, int p);
        void addMagazine(MagazineModel mm);
        MagazineModel getMagezine(int id);
        void updateMagazine(MagazineModel mm);
        void delMagazine(int id);
        object GetArticleList(string title, int? yearid, int? periodid, int pageIndex, int pageSize);
        void ArticleDel(int id);
        ArticleModel getArticleGuid();
        object GetUploadPic(string g);
        void DelImage(int id);
        void setImgSort(string sort);
        void addArticle(ArticleModel am);
        object GetFileUpload2(int sortid, int aid);
        object GetUploadPic2(int a);
        ArticleModel getArticle(int id);
        bool ArticleEdit(ArticleModel am);
        object getCommentList(string title, int pageIndex, int pageSize);
    }
}
