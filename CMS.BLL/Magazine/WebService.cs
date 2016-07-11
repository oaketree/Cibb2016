using Cibb.Core.DAL;
using Cibb.Core.Utility;
using CMS.BLL.Magazine.Models;
using CMS.Contract.Models.Magazine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace CMS.BLL.Magazine
{
    public class WebService : IWebService
    {
        private IWebRepository<GyglCategory> gyglCategory;
        private IWebRepository<GyglCategoryRelation> gyglCategoryRelation;
        private IWebRepository<Gygl> gygl;
        private IWebRepository<GyglArticle> gyglArticle;
        private IWebRepository<GyglAssist> gyglAssist;
        private IWebRepository<GyglImage> gyglImage;
        private IWebRepository<GyglComment> gyglComment;
        private IWebRepository<Contract.Models.User.Users> users;
        public WebService(IWebRepository<GyglCategory> gyglCategory, IWebRepository<GyglCategoryRelation> gyglCategoryRelation, IWebRepository<Gygl> gygl, IWebRepository<GyglArticle> gyglArticle, IWebRepository<GyglAssist> gyglAssist, IWebRepository<GyglImage> gyglImage, IWebRepository<GyglComment> gyglComment, IWebRepository<Contract.Models.User.Users> users)
        {
            this.gyglCategory = gyglCategory;
            this.gyglCategoryRelation = gyglCategoryRelation;
            this.gygl = gygl;
            this.gyglArticle = gyglArticle;
            this.gyglAssist = gyglAssist;
            this.gyglImage = gyglImage;
            this.gyglComment = gyglComment;
            this.users = users;
        }
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public object GetCategory(int pageIndex, int pageSize)
        {
            var _total = this.gyglCategory.FindAll().Count();
            var _rows = this.gyglCategory.FindAllByPage(null, true, o => o.SortID, pageSize, pageIndex).Select(s => new
            {
                ID = s.ID,
                Name = s.Name,
                SortID = s.SortID.Value
            });
            return new { total = _total, rows = _rows };
        }

        public void setSort(string sort)
        {
            var s = JsonConvert.DeserializeObject<List<Sort>>(sort);
            foreach (var a in s)
            {
                var gc = gyglCategory.GetById(a.id);
                gc.SortID = a.afterid;
                gyglCategory.Update(gc);
            }
        }


        public void setImgSort(string sort)
        {
            var s = JsonConvert.DeserializeObject<List<Sort>>(sort);
            foreach (var a in s)
            {
                var img = gyglImage.GetById(a.id);
                img.SortID = a.afterid;
                gyglImage.Update(img);
            }
        }


        public void addArticle(ArticleModel am)
        {
            var gl = gygl.GetById(int.Parse(am.Gygl));
            var a = new GyglArticle
            {
                Address = am.Address,
                Author = am.Author,
                CategoryID = int.Parse(am.Category),
                GyglID = int.Parse(am.Gygl),
                Title = am.Title,
                Keyword = am.Keyword,
                Summary = am.Summary,
                //Year=gl.Year,
                //Period=gl.Period,
                Hit = 0,
                Verify = Convert.ToBoolean(am.Verify)
            };
            gyglArticle.Insert(a);
            var img = gyglImage.FindAll(n => n.Guid == am.guid);
            foreach (var item in img)
            {
                item.ArticleID = a.ID;
                item.Guid = string.Empty;
                //gyglImage.Update(item);
            }
            gyglImage.Update();
        }


        public void addCategory(CategoryModel cm)
        {
            var a = new GyglCategory
            {
                Name = cm.Name,
            };
            gyglCategory.Insert(a);
            a.SortID = a.ID;
            gyglCategory.Update(a);
        }


        public CategoryModel getCategory(int id)
        {
            var a = gyglCategory.GetById(id);
            return new CategoryModel
            {
                ID = a.ID,
                Name = a.Name,
            };
        }

        public void CategoryEdit(CategoryModel cm)
        {
            var a = gyglCategory.GetById(cm.ID);
            a.Name = cm.Name;
            gyglCategory.Update(a);
        }

        public ArticleModel getArticle(int id)
        {
            var a = gyglArticle.GetById(id);
            var gl = gygl.GetById(a.GyglID.Value);
            return new ArticleModel
            {
                Year = gl.Year.ToString(),
                Period = gl.Period.ToString(),
                Author = a.Author,
                Address = a.Address,
                ID = a.ID,
                Category = a.CategoryID.ToString(),
                Summary = a.Summary,
                Keyword = a.Keyword,
                Title = a.Title,
                Verify = a.Verify.ToString()
            };
        }

        public bool ArticleEdit(ArticleModel am)
        {
            var year = int.Parse(am.Year);
            var period = int.Parse(am.Period);
            var gl = gygl.Find(n => n.Year == year && n.Period == period);
            if (gl == null)
                return false;
            else
            {
                var a = gyglArticle.GetById(am.ID);
                a.Address = am.Address;
                a.Author = am.Author;
                a.CategoryID = int.Parse(am.Category);
                a.GyglID = gl.ID;
                //a.Year = int.Parse(am.Year);
                //a.Period = int.Parse(am.Period);
                a.Keyword = am.Keyword;
                a.Summary = am.Summary;
                a.Title = am.Title;
                a.Verify = Convert.ToBoolean(am.Verify);
                gyglArticle.Update(a);
                return true;
            }
        }

        public void CategoryDel(int id)
        {
            var gr = gyglCategoryRelation.FindAll(n => n.CategoryID == id);
            foreach (var del in gr)
            {
                gyglCategoryRelation.Delete(del);
            }
            gyglCategory.Delete(id);
        }


        public object GetMagazineList(int pageIndex, int pageSize)
        {
            Func<int, string> cv = c =>
            {
                var gc = this.gyglCategoryRelation.FindAll(n => n.GyglID == c);
                if (gc != null)
                {
                    List<string> str = new List<string>();
                    foreach (var item in gc)
                    {
                        str.Add(item.Category.Name);
                    }
                    return string.Join(",", str);
                }
                else
                    return string.Empty;
            };
            var _total = gygl.FindAll().Count();
            var _rows = gygl.FindAllByPage(null, o => o.RegDate, pageSize, pageIndex).AsEnumerable().Select(s => new
            {
                ID = s.ID,
                Name = s.Name,
                Year = s.Year.Value,
                Period = s.Period.Value,
                Category = cv(s.ID),
                RegDate = s.RegDate.ToString()
            });
            return new { total = _total, rows = _rows };
        }


        public List<object> GetYear()
        {
            List<object> sc = new List<object>();
            var y = DateTime.Now.Year;
            for (int i = 0; i < 5; i++)
            {
                sc.Add(new
                {
                    Text = y.ToString() + "年",
                    text = y.ToString() + "年",
                    Value = y.ToString(),
                    id = y.ToString()
                });
                y--;
            }
            return sc;
        }

        public List<object> GetPeriod()
        {
            return new List<object> {
                new {text="第一期",id="1" },
                new {text="第二期",id="2" },
                new {text="第三期",id="3" },
                new {text="第四期",id="4" },
                new {text="第五期",id="5" },
                new {text="第六期",id="6" },
            };
        }

        public List<object> GetCategory()
        {
            List<object> sc = new List<object>();
            var li = this.gyglCategory.FindAll(true, o => o.SortID);
            foreach (var item in li)
            {
                sc.Add(new
                {
                    Text = item.Name,
                    Value = item.ID
                });
            }
            return sc;
        }

        public List<object> GetMagazine()
        {
            List<object> sc = new List<object>();
            var li = gygl.FindAll(false, o => o.RegDate).Take(6);
            foreach (var item in li)
            {
                sc.Add(new
                {
                    Text = item.Name,
                    Value = item.ID
                });
            }
            return sc;
        }

        public List<CategorySort> GetMagazineCategory(int mid)
        {
            List<CategorySort> sc = new List<CategorySort>();
            var li = gyglCategoryRelation.FindAll(n => n.GyglID == mid);
            foreach (var item in li)
            {
                sc.Add(new CategorySort
                {
                    Text = item.Category.Name,
                    Value = item.CategoryID.ToString(),
                    SortID = item.Category.SortID.Value,
                });
            }
            var r = sc.OrderBy(o => o.SortID);
            return r.ToList();
        }

        public List<CategorySort> GetMagazineCategory2(int y, int p)
        {
            var gl = gygl.Find(n => n.Year == y && n.Period == p);
            if (gl == null)
                return null;
            else
            {
                List<CategorySort> sc = new List<CategorySort>();
                var li = gyglCategoryRelation.FindAll(n => n.GyglID == gl.ID);
                foreach (var item in li)
                {
                    sc.Add(new CategorySort
                    {
                        Text = item.Category.Name,
                        Value = item.CategoryID.ToString(),
                        SortID = item.Category.SortID.Value,
                    });
                }
                var r = sc.OrderBy(o => o.SortID);
                return r.ToList();
            }
        }

        public void addMagazine(MagazineModel mm)
        {
            string imgfile = string.Empty;
            var img = new Uploader("Image", new string[] { ".jpg", ".png", ".gif", ".jpeg" });
            string delpath1 = "E:/boiler/gygl/gygl2016/Content/Upload/Magazine/Cover/";
            string delpath2 = "/Content/upload/magazine/cover/";
            if (img.checkUpload())
            {
                img.save(delpath1, new Cut { width = 190, height = 255, mode = "Cut" }, false);
                img.save(delpath2, new Cut { width = 80, height = 80, mode = "Cut" });
                //这边还可以继续保存
                img.end();
                imgfile = img.FileName;
            }

            var a = new Gygl
            {
                Name = mm.Name,
                Year = int.Parse(mm.Year),
                Period = int.Parse(mm.Period),
                TotalPeriod = int.Parse(mm.TotalPeriod),
                Publish = Convert.ToDateTime(mm.Publish),
                CoverImage = imgfile
            };
            this.gygl.Insert(a);
            foreach (var item in mm.Category)
            {
                this.gyglCategoryRelation.Insert(new GyglCategoryRelation
                {
                    GyglID = a.ID,
                    CategoryID = int.Parse(item)
                });
            }
        }

        public MagazineModel getMagezine(int id)
        {
            Func<int, IEnumerable<string>> cv = c =>
             {
                 var li = gyglCategoryRelation.FindAll(n => n.GyglID == c).AsEnumerable().Select(s => s.CategoryID.ToString());
                 return li;
             };

            var a = gygl.GetById(id);
            return new MagazineModel
            {
                ID = a.ID,
                Name = a.Name,
                Year = a.Year.ToString(),
                Period = a.Period.ToString(),
                TotalPeriod = a.TotalPeriod.ToString(),
                Category = cv(a.ID),
                Publish = a.Publish == null ? string.Empty : a.Publish.Value.ToString("MM/dd/yyyy"),
                CoverImage = a.CoverImage
            };
        }
        public void updateMagazine(MagazineModel mm)
        {
            var a = gygl.GetById(mm.ID);
            a.Name = mm.Name;
            a.Year = int.Parse(mm.Year);
            a.Period = int.Parse(mm.Period);
            a.TotalPeriod = int.Parse(mm.TotalPeriod);
            a.Publish = Convert.ToDateTime(mm.Publish);
            var img = new Uploader("Image", new string[] { ".jpg", ".png", ".gif", ".jpeg" });
            if (img.checkUpload())
            {
                string delpath1 = "E:/boiler/gygl/gygl2016/Content/Upload/Magazine/Cover/";
                string delpath2 = "/Content/upload/magazine/cover/";
                if (!string.IsNullOrEmpty(a.CoverImage))
                {
                    new FileDel(Path.Combine(delpath1, a.CoverImage), false);
                    new FileDel(Path.Combine(delpath2, a.CoverImage));
                    //这边还可以继续删除其他路径图片
                }

                img.save(delpath1, new Cut { width = 190, height = 255, mode = "Cut" }, false);
                img.save(delpath2, new Cut { width = 80, height = 80, mode = "Cut" });
                //这边还可以继续保存
                img.end();
                a.CoverImage = img.FileName;
            }
            gygl.Update(a);
            //var oldCategory = gyglCategoryRelation.FindAll(n => n.GyglID == mm.ID).AsEnumerable().Select(s => s.CategoryID.ToString());
            var oldCategory = a.Category.ToList();
            var newCategory = mm.Category.ToList();
            for (int i = 0; i < oldCategory.Count(); i++)
            {
                for (int j = 0; j < newCategory.Count(); j++)
                {
                    if (oldCategory[i].CategoryID.ToString() == newCategory[j])
                    {
                        oldCategory.Remove(oldCategory[i]);
                        newCategory.Remove(newCategory[j]);
                    }
                }
            }
            if (oldCategory.Count > 0)
            {
                foreach (var item in oldCategory)
                {
                    gyglCategoryRelation.Delete(item.ID);
                }
            }
            if (newCategory.Count > 0)
            {
                foreach (var item in newCategory)
                {
                    gyglCategoryRelation.Insert(new GyglCategoryRelation
                    {
                        CategoryID = int.Parse(item),
                        GyglID = a.ID
                    });
                }
            }
        }




        public void delMagazine(int id)
        {
            //分类部分删除
            var gr = gyglCategoryRelation.FindAll(n => n.GyglID == id);
            foreach (var item in gr)
            {
                gyglCategoryRelation.Delete(item.ID);
            }
            var cover = gygl.GetById(id).CoverImage;
            if (!string.IsNullOrEmpty(cover))
            {
                string delpath1 = "E:/boiler/gygl/gygl2016/Content/Upload/Magazine/Cover/";
                string delpath2 = "/Content/upload/magazine/cover/";
                new FileDel(Path.Combine(delpath1, cover), false);
                new FileDel(Path.Combine(delpath2, cover));
                //这边还可以继续删除其他路径图片
            }
            //期刊删除
            gygl.Delete(id);
        }


        public object GetArticleList(string title, int? yearid, int? periodid, int pageIndex, int pageSize)
        {
            Func<int, int> cv = c =>
            {
                return this.gyglAssist.FindAll(n => n.Assist == true).Count();
            };
            Expression<Func<GyglArticle, bool>> express = PredicateExtensions.True<GyglArticle>();
            if (yearid > 0)
            {
                var gl = gygl.FindAll(n => n.Year == yearid).Select(s => s.ID).ToList();
                express = express.And(n => gl.Contains(n.GyglID.Value));
            }
            if (periodid > 0)
            {
                var gl = gygl.FindAll(n => n.Period == periodid).Select(s => s.ID).ToList();
                express = express.And(n => gl.Contains(n.GyglID.Value));
            }
            if (!string.IsNullOrEmpty(title))
                express = express.And(n => n.Title.Contains(title) || n.Author.Contains(title));
            var _total = gyglArticle.FindAll(express).Count();
            var _rows = gyglArticle.FindAllByPage(express, o => o.RegDate, pageSize, pageIndex).AsEnumerable().Select(s => new
            {
                ID = s.ID,
                Title = s.Title,
                Gygl = s.Gygl.Name,
                Category = s.Category.Name,
                Author = s.Author,
                Vote = cv(s.ID),
                Hit = s.Hit,
                RegDate = s.RegDate.ToString()
            });
            return new { total = _total, rows = _rows };
        }

        //ArticleDel
        public void ArticleDel(int id)
        {
            var img = gyglImage.FindAll(n => n.ArticleID == id);
            foreach (var item in img)
            {
                DelImage(item.ID);
                //string delpath1 = "/Content/upload/magazine/thumbpic/";
                //new FileDel(Path.Combine(delpath1, item.ImageID));
                //gyglImage.Delete(item.ID);
            }
            gyglArticle.Delete(id);
        }

        public ArticleModel getArticleGuid()
        {
            return new ArticleModel { guid = Guid.NewGuid().ToString("N") };
        }

        //通过guid获取图片
        public object GetUploadPic(string g)
        {
            var img = gyglImage.FindAll(true, o => o.SortID, n => n.Guid == g);
            var _count = img.Count();
            var _row = img.ToList();
            return new { count = _count, row = _row };
        }

        public object GetUploadPic2(int a)
        {
            var img = gyglImage.FindAll(true, o => o.SortID, n => n.ArticleID == a);
            var _count = img.Count();
            var _row = img.ToList();
            return new { count = _count, row = _row };
        }


        public void DelImage(int id)
        {
            var del = gyglImage.GetById(id);
            if (del != null)
            {
                string delpath1 = "/Content/upload/magazine/thumbpic/";
                new FileDel(Path.Combine(delpath1, del.ImageID));
                string delpath2 = "E:/boiler/gygl/gygl2016/Content/Upload/Magazine/Page/";
                new FileDel(Path.Combine(delpath2, del.ImageID), false);
            }
            gyglImage.Delete(id);
        }

        public object getCommentList(string title, int pageIndex, int pageSize)
        {
            Func<int?, string> namecv = c =>
            {
                if (c != null)
                {
                    var u = users.GetById(c.Value).UserName;
                    return u;
                }
                else
                {
                    return string.Empty;
                }
            };
            Func<int?, string> acv = c =>
            {
                return gyglArticle.GetById(c.Value).Title;
            };
            Expression<Func<GyglComment, bool>> express = PredicateExtensions.True<GyglComment>();
            if (!string.IsNullOrEmpty(title))
            {
                var a = gyglArticle.FindAll(n => n.Title.Contains(title)).Select(n => n.ID).ToList();
                express = express.And(n => a.Contains(n.ArticleID.Value) || n.Comment.Contains(title));
            }
            var _total = gyglComment.FindAll(express).Count();
            var _rows = gyglComment.FindAllByPage(express, o => o.RegDate, pageSize, pageIndex).AsEnumerable().Select(s => new
            {
                ID = s.ID,
                Name = namecv(s.UserID),
                Reply = namecv(s.ReplyID),
                Comment = s.Comment,
                RegDate = s.RegDate.ToString(),
                Article = acv(s.ArticleID)
            });
            return new { total = _total, rows = _rows };
        }



        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="sortid">排序号</param>
        /// <param name="guid">文章的guid</param>
        /// <returns></returns>
        public object GetFileUpload(int sortid, string guid)
        {
            var img = new Uploader(new string[] { ".jpg", ".png", ".gif", ".jpeg" });
            if (!img.checkUpload())
            {
                return new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = "id" };
            }
            else
            {
                img.save("E:/boiler/gygl/gygl2016/Content/Upload/Magazine/Page/", new Cut { width = 796, height = 100, mode = "W" }, false);
                img.save("/Content/upload/magazine/thumbpic/", new Cut { width = 80, height = 80, mode = "H" });
                img.end();
                gyglImage.Insert(new GyglImage
                {
                    Guid = guid,
                    ImageID = img.FileName,
                    SortID = sortid,
                });
                return new { jsonrpc = 2.0, id = "id" };
            }
        }

        public object GetFileUpload2(int sortid, int aid)
        {
            var img = new Uploader(new string[] { ".jpg", ".png", ".gif", ".jpeg" });
            if (!img.checkUpload())
            {
                return new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = "id" };
            }
            else
            {
                //E:\boiler\gygl\gygl2016\Content\Upload\Magazine\Cover
                img.save("E:/boiler/gygl/gygl2016/Content/Upload/Magazine/Page/", new Cut { width = 796, height = 100, mode = "W" }, false);
                img.save("/Content/upload/magazine/thumbpic/", new Cut { width = 80, height = 80, mode = "H" });
                img.end();
                gyglImage.Insert(new GyglImage
                {
                    ArticleID = aid,
                    ImageID = img.FileName,
                    SortID = sortid,
                });
                return new { jsonrpc = 2.0, id = "id" };
            }
        }
    }
}
