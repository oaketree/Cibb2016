using CMS.BLL.Attributes;
using CMS.BLL.User;
using CMS.BLL.User.Models;
using System;
using System.Web.Mvc;

namespace CMS.Web.UI.Areas.Admin.Controllers
{
    public class UserController : Controller
    {

        private IWebService webservice;
        public UserController(IWebService webservice)
        {
            this.webservice = webservice;
        }

        [Permission]
        public ActionResult UserList()
        {
            return View();
        }

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

        public PartialViewResult Menu()
        {
            return PartialView();
        }

        //[Permission]
        public ActionResult JsonUserList(string title, int? roleid, DateTime? fromDate, DateTime? toDate,int page = 1, int rows = 1)
        {
            var a = this.webservice.GetUserList(title, roleid, fromDate, toDate,page, rows);
            return Json(a,JsonRequestBehavior.AllowGet);
        }

        [Permission]
        public ActionResult JsonRoleList(int page = 1, int rows = 1)
        {
            return Json(this.webservice.GetRoleList(page, rows));
        }

        [Permission]
        public ActionResult JsonAuthList(int page = 1, int rows = 1)
        {
            return Json(this.webservice.GetAuthList(page, rows));
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
            if (webservice.AuthExit(al.Name))
                ModelState.AddModelError("Name", "权限名已存在");
            else
            {
                webservice.addAuth(al);
                ModelState.AddModelError("", "权限添加成功！");
            }
            return View();
        }

        [Permission]
        public ActionResult AuthEdit(int id)
        {
            return View(this.webservice.GetSelectAuth(id));
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
        public void JsonAuthDel(int id)
        {
            this.webservice.delAuth(id);
        }

        [Permission]
        public ActionResult AddRole()
        {
            return View(new RoleAuthModel {
                Authorises = this.webservice.GetAuthorises()
            });
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
            else
            {
                ModelState.AddModelError("", "请选择权限！");
            }
            ram.Authorises = this.webservice.GetAuthorises();
            return View(ram);
        }



        /// <summary>
        /// 角色修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Permission]
        public ActionResult RoleEdit(int id)
        {
            return View(this.webservice.GetSelectRole(id));
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
        public void JsonRoleDel(int id)
        {
            this.webservice.delRole(id);
        }

        [Permission]
        public ActionResult AddUser()
        {
            return View(new UserModel { 
            Roles=this.webservice.GetRoles(),
            ValidList=this.webservice.GetValid(),
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(UserModel um,string[] roles)
        {
            if (roles != null)
            {
                this.webservice.AddUser(um, roles);
                ModelState.AddModelError("", "注册成功！");
            }
            else
            {
                ModelState.AddModelError("", "请选择角色！");
            }
            um.Roles = this.webservice.GetRoles();
            um.ValidList = this.webservice.GetValid();
            return View(um);
        }

        [Permission]
        public ActionResult UserEdit(int id)
        {
            return View(this.webservice.GetSelectUser(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit(UserModel um, string[] roles)
        {
            if (roles != null)
            {
                this.webservice.updateUser(um, roles);
                ModelState.AddModelError("", "修改成功！");
                um.Roles = this.webservice.GetSelectRoles(roles);
            }
            else
            {
                ModelState.AddModelError("", "请选择角色！");
                um.Roles = this.webservice.GetRoles();
            }
            um.ValidList = this.webservice.GetValid();
            return View(um);
        }

        [Permission]
        public JsonResult CkUserName(string username)
        {
            return Json(this.webservice.ckUserName(username.Trim()), JsonRequestBehavior.AllowGet);
        }

        [Permission]
        public void JsonUserDel(int id)
        {
            this.webservice.delUser(id);
        }

        [Permission]
        public ActionResult JsonRoleTree()
        {
            return Json(this.webservice.getRoleTree());
        }
    }
}