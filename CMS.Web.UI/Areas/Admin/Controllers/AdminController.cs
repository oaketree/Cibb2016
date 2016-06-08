using Cibb.Core.Cache;
using Cibb.Core.Utility;
using CMS.BLL.Admin;
using CMS.BLL.Admin.Models;
using CMS.BLL.Attributes;
using System;
using System.Drawing;
using System.Web.Mvc;

namespace CMS.Web.UI.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private IWebService webservice;
        public AdminController(IWebService webservice)
        {
            this.webservice = webservice;
        }
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            if (SessionHelper.Get("LoginAdminInfo")!=null)
                return RedirectToAction("Index","Home");
            if(CookieHelper.GetCookieValue("User")!=string.Empty&&CookieHelper.GetCookieValue("Password")!=string.Empty)
            {
                var user = this.webservice.CheckUser(CookieHelper.GetCookieValue("User"), CookieHelper.GetCookieValue("Password"));
                if (user!=null)
                {
                    this.webservice.setLoginInfo(user);
                    return RedirectToAction("Index","Home");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel Login)
        {
            if (TempData["VerificationCode"] == null || TempData["VerificationCode"].ToString() != Login.VerificationCode.ToUpper())
            {
                ModelState.AddModelError("VerificationCode", "验证码不正确");
                return View();
            }
            if (ModelState.IsValid) {
                var user = this.webservice.FindUser(Login.UserName);
                if (user==null)
                    ModelState.AddModelError("UserName", "用户名不存在");
                else if (user.Password == Security.Sha256(Login.Password))
                {
                    user.LoginTime = DateTime.Now;
                    user.LoginIP = Request.UserHostAddress;
                    webservice.updateUser(user);
                    if (Login.RememberMe)
                    {
                        CookieHelper.SetCookie("User",user.UserName);
                        CookieHelper.SetCookie("Password", user.Password);
                        this.webservice.setLoginInfo(user);
                    }
                    else
                    {
                        this.webservice.setLoginInfo(user);
                    }
                    return RedirectToAction("Index","Home");
                }else
                    ModelState.AddModelError("Password", "密码错误");
            }
            return View();
        }
        //管理员列表
        [Permission]
        public ActionResult List() 
        {
            return View();
        }

        //角色列表
        [Permission]
        public ActionResult RoleList() 
        {
            return View();
        }

        [Permission]
        public ActionResult AuthList() 
        {
            return View();
        }



        [Permission]
        public ActionResult JsonList(int page = 1, int rows = 1)
        {
            var a = this.webservice.GetAdminsList(page, rows);
            return Json(a,JsonRequestBehavior.AllowGet);
        }

        [Permission]
        public ActionResult JsonRoleList(int page = 1, int rows = 1) 
        {
            var a = this.webservice.GetRoleList(page, rows);
            return Json(a);
        }

        [Permission]
        public ActionResult JsonAuthList(int page = 1, int rows = 1)
        {
            return Json(this.webservice.GetAuthList(page, rows));
        }


        [Permission]
        public ActionResult JsonDel(int id) {
            this.webservice.delUser(id);
            return Json(true);
        }

        [Permission]
        public void JsonRoleDel(int id) {
            this.webservice.delRole(id);
        }

        [Permission]
        public void JsonAuthDel(int id)
        {
            this.webservice.delAuth(id);
        }

        /// <summary>
        /// 增加角色
        /// </summary>
        /// <returns></returns>
        [Permission]
        public ActionResult AddRole()
        {
            RoleAuthModel ram = new RoleAuthModel
            {
                Authorises = this.webservice.GetAuthorises()
            };
            return View(ram);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRole(RoleAuthModel ram, string[] auth) 
        {
            if (this.webservice.RoleExit(ram.Name))
                ModelState.AddModelError("Name", "角色名已存在");
            else if (auth != null)
            {
                this.webservice.addRole(ram, auth);
                ModelState.AddModelError("", "角色添加成功！");
            }
            else {
                ModelState.AddModelError("", "请选择权限！");
            }
            ram.Authorises = this.webservice.GetAuthorises();
            return View(ram);
        }

        [Permission]
        public ActionResult AddAuth()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAuth(AuthListModel al) 
        {
            if (this.webservice.AuthExit(al.Name))
                ModelState.AddModelError("Name", "角色名已存在");
            else 
            {
                this.webservice.addAuth(al);
                ModelState.AddModelError("", "角色添加成功！");
            }
            return View();
        }

        [Permission]
        public ActionResult Register()
        {
            RegisterModel rm = new RegisterModel
            {
                Roles = this.webservice.GetRoles()
            };
            return View(rm);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel r, string[] roles)
        {
            if (!r.TermAccepted)
            {
                ModelState.AddModelError("TermAccepted", "你必须接受用户协议！");
            }
            else
            {
                if (this.webservice.Exist(r.UserName))
                    ModelState.AddModelError("UserName", "用户名已存在");
                else if (roles != null)
                {
                    this.webservice.AddUser(r, roles);
                    ModelState.AddModelError("", "注册成功！");
                }
                else {
                    ModelState.AddModelError("", "请选择角色！");
                }
            }
            r.Roles = webservice.GetRoles();
            return View(r);

            //查看验证错误代码，此处因为转换问题验证肯定不能通过，故不用isvalid属性
            //代码很重要不要删
            //if (!ModelState.IsValid) {
            //    List<string> sb=new List<string>();
            //    var errors2 = ModelState.Values.SelectMany(v => v.Errors);
            //    foreach (var item in errors2) {
            //        sb.Add(item.Exception.Message);
            //    }
            //    return Content(string.Join(";", sb.ToArray()));
            //}
        }

        public PartialViewResult Menu() 
        {
            return PartialView();
        }


        [Permission(authorize = "1")]
        public ActionResult ChangePassword() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword cp)
        {
            if (ModelState.IsValid)
            {
                var login = (LoginAdminInfo)SessionHelper.Get("LoginAdminInfo");
                var u = this.webservice.FindUser(login.AdminID);
                if (u.Password == Security.Sha256(cp.OriginalPassword))
                {
                    u.Password = Security.Sha256(cp.Password);
                    webservice.updateUser(u);
                    ModelState.AddModelError("", "修改密码成功");
                }
                else
                {
                    ModelState.AddModelError("", "原密码错误");
                }
            }
            return View(cp);
        }


        public ActionResult Logout() 
        {
            CookieHelper.ClearCookie("User");
            CookieHelper.ClearCookie("Password");
            SessionHelper.Del("LoginAdminInfo");
            return Redirect(Url.Content("~/"));
        }

        [Permission]
        public ActionResult AuthEdit(int id)
        {
            var r = this.webservice.GetSelectAuth(id);
            return View(r);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AuthEdit(AuthListModel al)
        {
            this.webservice.updateAuth(al);
            ModelState.AddModelError("", "修改成功！");
            return View();
        }

        [Permission]
        public ActionResult RoleEdit(int id)
        {
            var r = this.webservice.GetSelectRole(id);
            return View(r);
        }

        /// <summary>
        /// 角色修改
        /// </summary>
        /// <param name="ram"></param>
        /// <param name="auth"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleEdit(RoleAuthModel ram, string[] auth)
        {
            if (auth != null)
            {
                this.webservice.updateRole(ram, auth);
                ModelState.AddModelError("", "修改成功！");
                ram.Authorises = this.webservice.GetSelectAuth(auth);
            }
            else
            {
                ModelState.AddModelError("", "请选择权限！");
                ram.Authorises = this.webservice.GetAuthorises();
            }
            return View(ram);
        }

        [Permission]
        public ActionResult Edit(int id) 
        {
            var uv = this.webservice.GetSelectUser(id);
            return View(uv);
        }

        /// <summary>
        /// 用户修改
        /// </summary>
        /// <param name="am"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdminModel am, string[] roles)
        {
            if (roles != null)
            {
                this.webservice.updateUser(am,roles);
                ModelState.AddModelError("", "修改成功！");
                am.Roles = this.webservice.GetSelectRoles(roles);
            }
            else
            {
                ModelState.AddModelError("", "请选择角色！");
                am.Roles = this.webservice.GetRoles();
            }
            return View(am);
        }



        [Permission]
        public ActionResult Details()
        {
            var login = (LoginAdminInfo)SessionHelper.Get("LoginAdminInfo");
            var uv = this.webservice.GetSelectUser(login.AdminID);
            return View(uv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(AdminModel am,string[] roles) 
        {
            if (roles != null)
            {
                this.webservice.updateUser(am, roles);
                ModelState.AddModelError("", "修改成功！");
                //am.UserName = am.UserName;
                am.Roles = webservice.GetSelectRoles(roles);
            }
            else {
                ModelState.AddModelError("", "请选择角色！");
                //am.UserName = u.UserName;
                am.Roles = webservice.GetRoles();
            }
            return View(am);
        }





        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult VerificationCode()
        {
            string verificationCode = Security.CreateVerificationText(4);
            Bitmap _img = Security.CreateVerificationImage(verificationCode, 100, 30);
            _img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            TempData["VerificationCode"] = verificationCode.ToUpper();
            return null;
        }
    }
}