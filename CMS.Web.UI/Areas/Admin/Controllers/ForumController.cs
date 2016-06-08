using CMS.BLL.Attributes;
using CMS.BLL.Forum;
using CMS.BLL.Forum.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CMS.Web.UI.Areas.Admin.Controllers
{
    public class ForumController : Controller
    {
        private IWebService webservice;
        public ForumController(IWebService webservice)
        {
            this.webservice = webservice;
        }

        public PartialViewResult Menu()
        {
            return PartialView();
        }

        [Permission]
        public ActionResult ForumList()
        {
            return View();
        }

        [Permission]
        public ActionResult AddForum()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddForum(ForumModel fm)
        {
            this.webservice.addForum(fm);
            ModelState.AddModelError("", "修改成功！");
            return View();
        }


        [Permission]
        public ActionResult ForumEdit(int id)
        {
            return View(this.webservice.GetSelectForum(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForumEdit(ForumModel fm)
        {
            this.webservice.updateForum(fm);
            ModelState.AddModelError("", "修改成功！");
            return View();
        }


        [Permission]
        public ActionResult JsonForumList(int page = 1, int rows = 1)
        {
            var a = this.webservice.GetForumList(page, rows);
            return Json(a, JsonRequestBehavior.AllowGet);
        }

        [Permission]
        public ActionResult CategoryList()
        {
            return View();
        }

        public ActionResult JsonCategoryList(int page = 1, int rows = 1)
        {
            var a = this.webservice.GetCategoryList(page, rows);
            return Json(a, JsonRequestBehavior.AllowGet);
        }

        [Permission]
        public ActionResult AddCategory()
        {
            return View(new CategoryModel
            {
                ForumList = this.webservice.GetForum(),
                CategoryList = this.webservice.GetCategory(),
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(CategoryModel cm)
        {
            if (ModelState.IsValid) {
                this.webservice.AddCategory(cm);
                ModelState.AddModelError("", "添加成功！");
            }
            cm.ForumList = this.webservice.GetForum();
            cm.CategoryList = this.webservice.GetCategory();
            cm.Name = string.Empty;
            return View(cm);
        }

        [Permission]
        public ActionResult CategoryEdit(int id)
        {
            return View(this.webservice.GetSelectCategory(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryEdit(CategoryModel cm)
        {
            this.webservice.updateCategory(cm);
            ModelState.AddModelError("", "修改成功！");
            cm.ForumList = this.webservice.GetForum();
            cm.CategoryList = this.webservice.GetCategory();
            return View(cm);
        }

        public void JsonCategoryDel(int id)
        {
            this.webservice.delCategory(id);
        }

        public ActionResult ArticleList()
        {
            return View();
        }

        [Permission]
        public ActionResult JsonArticleList(string title, int? forumid, int? categoryid,int page = 1, int rows = 1)
        {
            var a = this.webservice.GetArticleList(title, forumid, categoryid,page, rows);
            return Json(a);
        }

        public void JsonArticleDel(int id)
        {
            this.webservice.delArticle(id);
        }


        [Permission]
        public ActionResult JsonForumTree()
        {
            return Json(this.webservice.getForumTree());
        }

        [Permission]
        public ActionResult JsonCategoryTree()
        {
            return Json(this.webservice.getCategoryTree());
        }

        public ActionResult JsonForum()
        {
            return Json(this.webservice.GetForum(),JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonCategory()
        {
            return Json(this.webservice.GetCategory(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonSubCategory(int? f, int? c)
        {
            var sc = new List<SelectListItem>{
            new SelectListItem
            {
                Text="无子分类",
                Value="0"
            }};
            if (f != null && c != null)
            {
                var gsc = this.webservice.GetSubCategory(f.Value, c.Value);
                if (gsc != null)
                    sc = gsc;
                return Json(sc, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(sc, JsonRequestBehavior.AllowGet);
        }

        [Permission]
        public ActionResult AddArticle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AddArticle(ArticleModel am)
        {
            if (ModelState.IsValid)
            {
                this.webservice.AddArticle(am);
                //ModelState.AddModelError("", "文章添加成功！");
                return RedirectToAction("AddArticle");
            }
            return View();
        }

        [Permission]
        public ActionResult ArticleEdit(int id)
        {
            return View(this.webservice.getArticle(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ArticleEdit(ArticleModel am)
        {
            if (ModelState.IsValid)
            {
                this.webservice.updateArticle(am);
                ModelState.AddModelError("", "文章修改成功！");
            }
            return View(this.webservice.getArticle(am.ID));
        }

        [Permission]
        public void JsonImgDel(int id)
        {
            this.webservice.delImg(id);
        }

        [Permission]
        public void JsonFileDel(int id)
        {
            this.webservice.delFile(id);
        }

        public ActionResult Upload()
        {
            return Json(this.webservice.GetUpload());
        }
       
    }
}