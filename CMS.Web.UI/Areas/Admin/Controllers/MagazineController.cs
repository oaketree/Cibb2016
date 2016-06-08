using Cibb.Core.Utility;
using CMS.BLL.Attributes;
using CMS.BLL.Magazine;
using CMS.BLL.Magazine.Models;
using System.Web.Mvc;

namespace CMS.Web.UI.Areas.Admin.Controllers
{
    public class MagazineController : Controller
    {
        private IWebService webservice;
        public MagazineController(IWebService webservice)
        {
            this.webservice = webservice;
        }

        [Permission]
        public ActionResult MagazineList()
        {
            return View();
        }

        public ActionResult JsonMagazineList(int page = 1, int rows = 1)
        {
            return Json(webservice.GetMagazineList(page,rows));
        }

        [Permission]
        public ActionResult AddMagazine()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMagazine(MagazineModel mm)
        {
            webservice.addMagazine(mm);
            ModelState.AddModelError("", "期刊添加成功！");
            return View();
        }

        [Permission]
        public ActionResult MagazineEdit(int id)
        {
            return View(webservice.getMagezine(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MagazineEdit(MagazineModel mm)
        {
            webservice.updateMagazine(mm);
            ModelState.AddModelError("", "期刊修改成功！");
            return View(webservice.getMagezine(mm.ID));
        }

        [Permission]
        public void JsonMagazineDel(int id)
        {
            webservice.delMagazine(id);
        }

        public JsonResult JsonYear()
        {
            return Json(this.webservice.GetYear());
        }

        public JsonResult JsonPeriod()
        {
            return Json(this.webservice.GetPeriod());
        }

        public JsonResult JsonCategory()
        {
            return Json(this.webservice.GetCategory());
        }

        public JsonResult JsonMagazine()
        {
            return Json(this.webservice.GetMagazine());
        }

        public JsonResult JsonMagazineCategory(int mid)
        {
            return Json(this.webservice.GetMagazineCategory(mid));
        }
        public JsonResult JsonMagazineCategory2(int y,int p)
        {
            var r = webservice.GetMagazineCategory2(y, p);
            if (r == null)
                return Json(0);
            else
                return Json(r);
        }

        [Permission]
        public ActionResult CategoryList()
        {
            return View();
        }

        [Permission]
        public JsonResult JsonCategoryList(int page = 1, int rows = 1)
        {
            return Json(webservice.GetCategory(page, rows));
        }

        [Permission]
        public void ChangeSort(string sort)
        {
            webservice.setSort(sort);
        }

        [Permission]
        public void ChangeImgSort(string sort)
        {
            webservice.setImgSort(sort);
        }


        [Permission]
        public ActionResult AddCategory()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(CategoryModel cm)
        {
            webservice.addCategory(cm);
            ModelState.AddModelError("", "分类添加成功！");
            return View();
        }

        [Permission]
        public ActionResult CategoryEdit(int id)
        {
            return View(webservice.getCategory(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryEdit(CategoryModel cm)
        {
            webservice.CategoryEdit(cm);
            ModelState.AddModelError("", "分类修改成功！");
            return View();
        }

        public void JsonCategoryDel(int id)
        {
            webservice.CategoryDel(id);
        }

        public ActionResult ArticleList()
        {
            return View();
        }

        public void JsonArticleDel(int id)
        {
            webservice.ArticleDel(id);
        }

        public ActionResult AddArticle()
        {
            return View(webservice.getArticleGuid());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddArticle(ArticleModel am)
        {
            webservice.addArticle(am);
            return RedirectToAction("AddArticle");
        }

        public ActionResult ArticleEdit(int id)
        {
            return View(webservice.getArticle(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ArticleEdit(ArticleModel am)
        {
            if (!webservice.ArticleEdit(am))
                ModelState.AddModelError("", "修改失败，期刊不存在！");
            else {
                ModelState.AddModelError("", "修改成功！");
            }
            return View(webservice.getArticle(am.ID));
        }

        public JsonResult JsonArticleList(string title,int? yearid,int? periodid,int page = 1, int rows = 1)
        {
            return Json(webservice.GetArticleList(title,yearid,periodid,page, rows));
        }


        public ActionResult CommentList()
        {
            return View();
        }

        public JsonResult JsonCommentList(string title,int page = 1, int rows = 1)
        {
            return Json(webservice.getCommentList(title,page, rows));
        }


        public ActionResult CommentArticleList(int aid)
        {
            return View();
        }

        public PartialViewResult Menu()
        {
            return PartialView();
        }

 
        public JsonResult JsonFileUpload(string id, string name, string type, string lastModifiedDate, int size,int sortid,string guid)
        {
            return Json(webservice.GetFileUpload(sortid,guid));
        }
        //编辑时上传
        public JsonResult JsonFileUpload2(string id, string name, string type, string lastModifiedDate, int size, int sortid, int aid)
        {
            return Json(webservice.GetFileUpload2(sortid, aid));
        }

        public JsonResult JsonGetUploadPic(string g)
        {
            return Json(webservice.GetUploadPic(g));
        }

        public JsonResult JsonGetUploadPic2(int a)
        {
            return Json(webservice.GetUploadPic2(a));
        }

        public void JsonDelImage(int id)
        {
            webservice.DelImage(id);
        }


        public ActionResult ArticleUpload()
        {
            return View();
        }
        public string getsha(string a)
        {
            return Security.Sha256(a);
        }
    }
}