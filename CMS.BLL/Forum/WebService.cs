using Cibb.Core.DAL;
using Cibb.Core.Utility;
using CMS.BLL.Common;
using CMS.BLL.Forum.Models;
using CMS.Contract.Models.Forum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace CMS.BLL.Forum
{
    public class WebService:IWebService
    {
        private IWebRepository<Forums> Forum;
        private IWebRepository<ForumCategory> Category;
        private IWebRepository<Report> Report;
        public WebService(IWebRepository<Forums> Forum, IWebRepository<ForumCategory> Category, IWebRepository<Report> Report)
        {
            this.Forum = Forum;
            this.Category = Category;
            this.Report = Report;
        }

        public Dictionary<string, object> GetForumList(int pageIndex, int pageSize)
        {
            var total = this.Forum.FindAll().Count();
            var rows = this.Forum.FindAllByPage(null, o => o.RegDate, pageSize, pageIndex).Select(cm => new { 
                ID=cm.ID,
                Name=cm.Name,
                SimpleName=cm.SimpleName,
                YearFrom = cm.YearFrom.ToString(),
                YearTo=cm.YearTo.ToString()
            });
            Dictionary<string, object> json = new Dictionary<string, object>();
            json.Add("total", total);
            json.Add("rows", rows);
            return json;
        }

        public Dictionary<string, object> GetCategoryList(int pageIndex, int pageSize)
        {
            Func<int?, string> cv1 = v =>
            {
                string r = string.Empty;
                switch (v)
                {
                    case 1:
                        r = "主题报告";
                        break;
                    case 2:
                        r = "专题报告";
                        break;
                    default:
                        r = "其他报告";
                        break;
                }
                return r;
            };

            Func<int?, string> cv2 = v =>
            {
                //var f = this.Forum.Find(n => n.ID == v).SimpleName;
                var f = this.Forum.GetById(v.Value).SimpleName;
                return f;
            };

            var total = this.Category.FindAll().Count();
            var rows = this.Category.FindAllByPage(null, o => o.RegDate, pageSize, pageIndex).AsEnumerable().Select(cm => new
            {
                ID = cm.ID,
                Name = cm.Name,
                Category = cv1(cm.Category),
                Forum = cv2(cm.ForumID)
            });
            Dictionary<string, object> json = new Dictionary<string, object>();
            json.Add("total", total);
            json.Add("rows", rows);
            return json;
        }

        public Dictionary<string, object> GetArticleList(string title, int? forumid, int? categoryid,int pageIndex, int pageSize)
        {
            Func<int?, string> cv1 = v =>
            {
                string r = string.Empty;
                switch (v)
                {
                    case 1:
                        r = "主题报告";
                        break;
                    case 2:
                        r = "专题报告";
                        break;
                    default:
                        r = "其他报告";
                        break;
                }
                return r;
            };

            Func<int?, string> cv2 = v =>
            {
                var f = this.Forum.GetById(v.Value).SimpleName;
                return f;
            };

            Expression<Func<Report, bool>> express = PredicateExtensions.True<Report>();
            if (forumid > 0)
                express = express.And(n => n.ForumID==forumid);
            if(categoryid>0)
                express = express.And(n => n.Category== categoryid);
            if (!string.IsNullOrEmpty(title))
                express = express.And(n => n.Title.Contains(title));
            var total = this.Report.FindAll(express).Count();
            var rows = this.Report.FindAllByPage(express, o => o.RegDate, pageSize, pageIndex).AsEnumerable().Select(cm => new 
            {
                ID=cm.ID,
                Title=cm.Title,
                Speaker=cm.Speaker,
                Hits=cm.Hits.Value,
                Forum=cv2(cm.ForumID),
                Category=cv1(cm.Category)
            });

            Dictionary<string, object> json = new Dictionary<string, object>();
            json.Add("total", total);
            json.Add("rows", rows);
            return json;
        }

        public void delArticle(int id)
        {
            this.Report.Delete(id);
        }

        public List<object> getForumTree()
        {
            List<object> ddl = new List<object>();
            ddl.Add(new { id = 0, text = "所有论坛" });
            ddl.AddRange(this.Forum.FindAll(false,o=>o.YearFrom).Select(s => new 
            {
                id = s.ID,
                text = s.SimpleName,
            }).AsEnumerable());
            return ddl;
        }

        public List<object> getCategoryTree()
        {
            List<object> ddl = new List<object>();
            ddl.Add(new { id = 0, text = "所有报告" });
            ddl.Add(new { id = 1, text = "主题报告" });
            ddl.Add(new { id = 2, text = "专题报告" });
            return ddl;
        }

        public void addForum(ForumModel fm)
        {
            this.Forum.Insert(new Forums
            {
                Name = fm.Name,
                SimpleName = fm.SimpleName,
                Contacts = fm.Contacts,
                Phone = fm.Phone,
                Email = fm.Email,
                YearFrom =Convert.ToDateTime(fm.YearFrom),
                YearTo = Convert.ToDateTime(fm.YearTo)
            });
        }

        public ForumModel GetSelectForum(int id)
        {
            //var f = this.Forum.Find(n => n.ID == id);
            var f = this.Forum.GetById(id);
            var fm = new ForumModel
            {
                ID=f.ID,
                Name = f.Name,
                SimpleName = f.SimpleName,
                Contacts = f.Contacts,
                Email = f.Email,
                Phone = f.Phone,
                YearFrom = f.YearFrom==null?string.Empty:f.YearFrom.Value.ToString("MM/dd/yyyy"),
                YearTo = f.YearTo==null?string.Empty:f.YearTo.Value.ToString("MM/dd/yyyy")
            };
            return fm;
        }

        public void updateForum(ForumModel fm)
        {
            //var f = this.Forum.Find(n => n.ID == fm.ID);
            var f = this.Forum.GetById(fm.ID);
            f.Name = fm.Name;
            f.SimpleName = fm.SimpleName;
            f.Contacts = fm.Contacts;
            f.Email = fm.Email;
            f.Phone = fm.Phone;
            f.YearFrom = Convert.ToDateTime(fm.YearFrom);
            f.YearTo = Convert.ToDateTime(fm.YearTo);
            this.Forum.Update(f);
        }

        public List<SelectListItem> GetForum()
        {
            List<SelectListItem> forums = this.Forum.FindAll(false,o=>o.YearFrom).Select(s => new SelectListItem
            {
                Text = s.SimpleName,
                Value = s.ID.ToString(),
                Selected = false
            }).ToList();
            return forums;
        }

        public List<SelectListItem> GetCategory()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text="主题报告",Value="1"},
                new SelectListItem{Text="专题报告",Value="2"},
            };
        }

        public void AddCategory(CategoryModel cm)
        {
            this.Category.Insert(new ForumCategory { 
            ForumID=int.Parse(cm.Forum),
            Category=int.Parse(cm.Category),
            Name=cm.Name
            });
        }
        public CategoryModel GetSelectCategory(int id)
        {
            Func<int?, List<SelectListItem>> cv1 = v => {
                List<SelectListItem> ddl = this.Forum.FindAll().Select(f => new SelectListItem
                {
                    Text=f.SimpleName,
                    Value=f.ID.ToString()
                }).ToList();
                if (v != null)
                {
                    foreach (var item in ddl)
                    {
                        if (int.Parse(item.Value) == v)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
                return ddl;
            };

            Func<int?, List<SelectListItem>> cv2 = v =>
            {
                List<SelectListItem> ddl = new List<SelectListItem>
                {
                    new SelectListItem{Text="主题报告",Value="1"},
                new SelectListItem{Text="专题报告",Value="2"},
                };

                if (v != null)
                {
                    foreach (var item in ddl)
                    {
                        if (int.Parse(item.Value) == v)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
                return ddl;
            };

            //var c = this.Category.Find(n => n.ID == id);
            var c = this.Category.GetById(id);
            var cm = new CategoryModel { 
                ID=c.ID,
                Name=c.Name,
                ForumList=cv1(c.ForumID),
                CategoryList=cv2(c.Category)
            };
            return cm;
        }

        public void updateCategory(CategoryModel cm)
        {
            //var c = this.Category.Find(n => n.ID == cm.ID);
            var c = this.Category.GetById(cm.ID);
            c.Category = int.Parse(cm.Category);
            c.ForumID = int.Parse(cm.Forum);
            c.Name = cm.Name;
            this.Category.Update(c);
        }

        public void delCategory(int id)
        {
            this.Category.Delete(id);
        }

        public List<SelectListItem> GetSubCategory(int f,int c)
        {
            var cfind = this.Category.FindAll(n => n.ForumID == f && n.Category == c);
            if (cfind.Count() > 0)
            {
                List<SelectListItem> sc = cfind.Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.ID.ToString()
                }).ToList();
                return sc;
            }
            else
                return null;
        }

        public void AddArticle(ArticleModel am)
        {
            string imgfile = string.Empty;
            var img = new Uploader("Image", new string[] { ".jpg", ".png", ".gif", ".jpeg" });
            if (img.checkUpload())
            {
                string savepath1 = "/Content/upload/forum/thumbpic/";
                string savepath2= @"E:/boiler/cibb/cibb2015/Content/upload/forum/thumbpiclist/";
                string savepath3 = @"E:/boiler/cibb/cibb2015/Content/upload/forum/thumbpic/";
                img.save(savepath1, new Cut { width = 100, height = 100, mode = "Cut" });
                img.save(savepath2, new Cut { width = 140, height = 167, mode = "Cut" }, false);
                img.save(savepath3, new Cut { width = 244, height = 292, mode = "Cut" }, false);
                img.end();
                imgfile = img.FileName;
            }
            string fufile = string.Empty;
            var fu = new Uploader("FileUpload", new string[] { ".pdf", ".docx", ".doc", ".xls", ".xlsx" });
            if (fu.checkUpload())
            {
                string savepath= @"E:/boiler/cibb/cibb2015/Content/upload/forum/files/";
                fu.save(savepath, false);
                fu.end();
                fufile = fu.FileName;
            }
            Report.Insert(new Report
            {
                ForumID=int.Parse(am.Forum),
                Category=int.Parse(am.Category),
                SubCategory=int.Parse(am.SubCategory),
                Speaker=am.Speaker,
                Profile=am.Profile,
                Hits=0,
                Summary=am.Summary,
                Title=am.Title,
                Image=imgfile,
                FileUpload=fufile
            });
        }

        public ArticleModel getArticle(int id)
        {
            var r = this.Report.GetById(id);
            return new ArticleModel
            {
                ID = r.ID,
                Forum = r.ForumID.ToString(),
                Category = r.Category.ToString(),
                SubCategory = r.SubCategory.ToString(),
                Title = r.Title,
                Speaker = r.Speaker,
                Profile = r.Profile,
                Summary = r.Summary,
                Image = r.Image,
                FileUpload = r.FileUpload
            };
        }

        public void updateArticle(ArticleModel am)
        {
            var a = this.Report.GetById(am.ID);
            a.ForumID = int.Parse(am.Forum);
            a.Category = int.Parse(am.Category);
            a.SubCategory = int.Parse(am.SubCategory);
            a.Title = am.Title;
            a.Speaker = am.Speaker;
            a.Profile = am.Profile;
            a.Summary = am.Summary;

            var img = new Uploader("Image", new string[] { ".jpg", ".png", ".gif", ".jpeg" });
            if (img.checkUpload())
            {
                if (!string.IsNullOrEmpty(a.Image))
                {
                    string delpath1 = "/Content/upload/forum/thumbpic/";
                    string delpath2 = @"E:/boiler/cibb/cibb2015/Content/upload/forum/thumbpiclist/";
                    string delpath3 = @"E:/boiler/cibb/cibb2015/Content/upload/forum/thumbpic/";
                    new FileDel(Path.Combine(delpath1, a.Image));
                    new FileDel(Path.Combine(delpath2, a.Image), false);
                    new FileDel(Path.Combine(delpath3, a.Image), false);
                }
                string savepath1 = "/Content/upload/forum/thumbpic/";
                string savepath2 = @"E:/boiler/cibb/cibb2015/Content/upload/forum/thumbpiclist/";
                string savepath3 = @"E:/boiler/cibb/cibb2015/Content/upload/forum/thumbpic/";
                img.save(savepath1, new Cut { width = 100, height = 100, mode = "Cut" });
                img.save(savepath2, new Cut { width = 140, height = 167, mode = "Cut" }, false);
                img.save(savepath3, new Cut { width = 244, height = 292, mode = "Cut" }, false);
                img.end();
                a.Image = img.FileName;
            }
            var fu = new Uploader("FileUpload", new string[] { ".pdf", ".docx", ".doc", ".xls", ".xlsx" });
            if (fu.checkUpload())
            {
                if (!string.IsNullOrEmpty(a.FileUpload))
                {
                    string delpath = @"E:/boiler/cibb/cibb2015/Content/upload/forum/files/";
                    new FileDel(Path.Combine(delpath, a.FileUpload), false);
                }
                string savepath = @"E:/boiler/cibb/cibb2015/Content/upload/forum/files/";
                fu.save(savepath, false);
                fu.end();
                a.FileUpload = fu.FileName;
            }
            this.Report.Update(a);
        }


        public void delImg(int id)
        {
            var a = this.Report.GetById(id);
            if (a != null)
            {
                if (a.Image != string.Empty)
                {
                    string delpath1 = "/Content/upload/forum/thumbpic/";
                    string delpath2 = @"E:/boiler/cibb/cibb2015/Content/upload/forum/thumbpiclist/";
                    string delpath3 = @"E:/boiler/cibb/cibb2015/Content/upload/forum/thumbpic/";
                    new FileDel(Path.Combine(delpath1, a.Image));
                    new FileDel(Path.Combine(delpath2, a.Image), false);
                    new FileDel(Path.Combine(delpath3, a.Image), false);
                    a.Image = string.Empty;
                    this.Report.Update(a);
                }
            }
        }

        public void delFile(int id)
        {
            var a = this.Report.GetById(id);
            if (a != null)
            {
                if (a.FileUpload != string.Empty)
                {
                    //文件删除
                    string delpath = @"E:/boiler/cibb/cibb2015/Content/upload/forum/files/";
                    new FileDel(Path.Combine(delpath, a.FileUpload), false);
                    a.FileUpload = string.Empty;
                    this.Report.Update(a);
                }
            }
        }
        public Dictionary<string,object> GetUpload()
        {
            var a = new Attachment("/Content/upload/forum/");
            return a.save();
        }
    }
}
