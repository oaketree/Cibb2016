using Cibb.Core.Cache;
using Cibb.Core.DAL;
using Cibb.Core.Utility;
using CMS.BLL.Admin.Models;
using CMS.Contract.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.BLL.Admin
{
    public class WebService:IWebService
    {
        private IWebRepository<Role> Roles;
        private IWebRepository<Admins> Admins;
        private IWebRepository<Authorise> Authorises;
        private IWebRepository<AdminRole> AdminRoles;
        private IWebRepository<RoleAuthorise> RoleAuthorises;
        public WebService(IWebRepository<Role> Roles, IWebRepository<Admins> Admins, IWebRepository<AdminRole> AdminRoles, IWebRepository<RoleAuthorise> RoleAuthorises, IWebRepository<Authorise> Authorises)
        {
            this.Roles = Roles;
            this.Authorises = Authorises;
            this.Admins = Admins;
            this.AdminRoles = AdminRoles;
            this.RoleAuthorises = RoleAuthorises;
        }

        public IEnumerable<SelectListItem> GetAuthorises() 
        {
            IEnumerable<SelectListItem> authorises = this.Authorises.FindAll().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.AuthoriseID.ToString(),
                Selected = false
            });
            return authorises;
        }


        public IEnumerable<SelectListItem> GetRoles()
        {
            IEnumerable<SelectListItem> roles = Roles.FindAll().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.RoleID.ToString(),
                Selected = false
            });
            return roles;
        }

        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool Exist(string username)
        {
            var f = this.Admins.Find(n => n.UserName == username);
            if (f!=null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 角色存在判断
        /// </summary>
        /// <param name="rolename">角色名</param>
        /// <returns></returns>
        public bool RoleExit(string rolename) 
        {
            var f = this.Roles.Find(n=>n.Name==rolename);
            if (f != null)
                return true;
            else
                return false;
        }

        public bool AuthExit(string authname)
        {
            var f = this.Authorises.Find(n => n.Name == authname);
            if (f != null)
                return true;
            else
                return false;
        }


        public void addRole(RoleAuthModel ram, string[] auth)
        {
            var r = new Role
            {
                Name=ram.Name,
                Description=ram.Description,
            };
            this.Roles.Insert(r);
            foreach (var item in auth) 
            {
                this.RoleAuthorises.Insert(new RoleAuthorise
                {
                    RoleID=r.RoleID,
                    AuthoriseID=int.Parse(item)
                });
            }
        }

        public void addAuth(AuthListModel al)
        {
            this.Authorises.Insert(new Authorise { 
                Name=al.Name,
                Description=al.Description
            });
        }

        public Admins FindUser(string username) 
        {
            var u = this.Admins.Find(n => n.UserName == username);
            return u;
        }

        public Admins FindUser(int userid)
        {
            //return this.Admins.Find(n => n.AdminID == userid);
            return this.Admins.GetById(userid);
        }

        public IEnumerable<SelectListItem> GetSelectAuth(string[] auth)
        {
            List<SelectListItem> selectroles = this.Authorises.FindAll().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.AuthoriseID.ToString(),
                Selected = false
            }).ToList();
            foreach (var item in selectroles)
            {
                foreach (var sr in auth)
                {
                    if (sr == item.Value)
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
            return selectroles;
        }

        public IEnumerable<SelectListItem> GetSelectRoles(string[] roles)
        {
            List<SelectListItem> selectroles = Roles.FindAll().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.RoleID.ToString(),
                Selected = false
            }).ToList();
            foreach (var item in selectroles)
            {
                foreach (var sr in roles)
                {
                    if (sr == item.Value)
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
            return selectroles;
        }
        public AdminModel GetSelectUser(int userid)
        {
            Func<ICollection<AdminRole>, IEnumerable<SelectListItem>> sr = c =>
            {
                List<SelectListItem> selectroles = this.Roles.FindAll().Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.RoleID.ToString(),
                    Selected = false
                }).ToList();
                foreach (var item in selectroles)
                {
                    foreach (var s in c)
                    {
                        if (item.Value == s.RoleID.ToString())
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
                return selectroles;
            };

            //var u = this.Admins.Find(n => n.AdminID == userid);
            var u = this.Admins.GetById(userid);
            var uv = new AdminModel
            {
                AdminID = u.AdminID,
                UserName = u.UserName,
                DisplayName = u.DisplayName,
                //Password=u.Password,
                Roles = sr(u.Roles)
            };
            return uv;
        }


        public RoleAuthModel GetSelectRole(int roleid)
        {
            Func<ICollection<RoleAuthorise>, IEnumerable<SelectListItem>> sa = c =>
            {
                List<SelectListItem> selectroles = this.Authorises.FindAll().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.AuthoriseID.ToString(),
                Selected = false
            }).ToList();
                foreach (var item in selectroles)
                {
                    foreach (var sr in c)
                    {
                        if (item.Value == sr.AuthoriseID.ToString())
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
                return selectroles;
            };
            //var r = this.Roles.Find(n => n.RoleID == roleid);
            var r = this.Roles.GetById(roleid);
            var ra = new RoleAuthModel
            {
                RoleID = r.RoleID,
                Name = r.Name,
                Description = r.Description,
                Authorises = sa(r.Authorizes)
            };
            return ra;
        }

        public AuthListModel GetSelectAuth(int authid)
        {
            //var a = this.Authorises.Find(n => n.AuthoriseID == authid);
            var a = this.Authorises.GetById(authid);
            var al = new AuthListModel
            {
                Name = a.Name,
                Description = a.Description,
                AuthoriseID = a.AuthoriseID
            };
            return al;
        }



        public Admins CheckUser(string username, string password) 
        {
            var u = this.Admins.Find(n => n.UserName == username && n.Password == password);
            return u;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userid"></param>
        public void delUser(int userid)
        {
            var ar = this.AdminRoles.FindAll(n => n.AdminID == userid).ToList();
            foreach (var item in ar)
            {
                this.AdminRoles.Delete(item);
            }
            this.Admins.Delete(userid);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleid"></param>
        public void delRole(int roleid)
        {
            var ra = this.RoleAuthorises.FindAll(n => n.RoleID == roleid).ToList();
            foreach (var item in ra)
            {
                this.RoleAuthorises.Delete(item);
            }

            var ar = this.AdminRoles.FindAll(n => n.RoleID == roleid).ToList();
            foreach (var item in ar)
            {
                this.AdminRoles.Delete(item);
            }

            this.Roles.Delete(roleid);
        }

        public void delAuth(int authid)
        {
            var ra = this.RoleAuthorises.FindAll(n => n.AuthoriseID == authid).ToList();
            foreach (var item in ra)
            {
                this.RoleAuthorises.Delete(item);
            }
            this.Authorises.Delete(authid);
        }
        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="a"></param>
        public void updateAuth(AuthListModel a)
        {
            //var auth = this.Authorises.Find(n => n.AuthoriseID == a.AuthoriseID);
            var auth = this.Authorises.GetById(a.AuthoriseID);
            auth.Name = a.Name;
            auth.Description = a.Description;
            this.Authorises.Update(auth);
        }


        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="r"></param>
        /// <param name="auth"></param>
        public void updateRole(RoleAuthModel ram, string[] auth)
        {
            //var r = this.Roles.Find(n => n.RoleID == ram.RoleID);
            var r = this.Roles.GetById(ram.RoleID);
            r.Name = ram.Name;
            r.Description = ram.Description;
            this.Roles.Update(r);
            var oldauth = r.Authorizes.ToList();
            var newauth = auth.ToList();
            for (int i = 0; i < oldauth.Count(); i++)
            {
                for (int j = 0; j < newauth.Count(); j++)
                {
                    if (oldauth[i].AuthoriseID.ToString() == newauth[j])
                    {
                        oldauth.Remove(oldauth[i]);
                        newauth.Remove(newauth[j]);
                    }
                }
            }
            if (oldauth.Count > 0)
            {
                foreach (var item in oldauth)
                {
                    this.RoleAuthorises.Delete(item.RelationID);
                }
            }
            if (newauth.Count > 0)
            {
                foreach (var item in newauth)
                {
                    this.RoleAuthorises.Insert(new RoleAuthorise
                    {
                        AuthoriseID=int.Parse(item),
                        RoleID=r.RoleID
                    });
                }
            }
        }


        public void updateUser(Admins user) 
        {
            this.Admins.Update(user);
        }

        //正确的roles更新办法
        public void updateUser(AdminModel am, string[] roles)
        {
            //var u=this.Admins.Find(n =>n.AdminID == am.AdminID);
            var u = this.Admins.GetById(am.AdminID);
            u.UserName = am.UserName;
            u.DisplayName = am.DisplayName;
            if (!string.IsNullOrEmpty(am.Password))
                u.Password = Security.Sha256(am.Password);
            this.Admins.Update(u);
            var oldroles = u.Roles.ToList();
            var newroles = roles.ToList();
            for (int i = 0; i < oldroles.Count(); i++)
            {
                for (int j = 0; j < newroles.Count(); j++)
                {
                    if (oldroles[i].RoleID.ToString() == newroles[j])
                    {
                        oldroles.Remove(oldroles[i]); 
                        newroles.Remove(newroles[j]);
                    }
                }
            }
            //foreach不能添加和删除内部元素
                //foreach (var itemold in oldroles)
                //{
                //    foreach (var itemnew in newroles)
                //    {
                //        if (itemold.RoleID.ToString() == itemnew)
                //        {
                //            oldroles.Remove(itemold);
                //            newroles.Remove(itemnew);
                //        }
                //    }
                //}
            if (oldroles.Count > 0)
            {
                foreach (var item in oldroles)
                {
                    this.AdminRoles.Delete(item.RelationID);
                }
            }
            if (newroles.Count > 0)
            {
                foreach (var item in newroles)
                {
                    this.AdminRoles.Insert(new AdminRole
                    {
                        AdminID = u.AdminID,
                        RoleID = int.Parse(item)
                    });
                }
            }
        }

        public void AddUser(RegisterModel rm, string[] roles)
        {
            var a = new Admins
            {
                UserName = rm.UserName,
                Password = Security.Sha256(rm.Password),
                DisplayName=rm.DisplayName,
                LoginTime=DateTime.Now,
                LoginIP =HttpContext.Current.Request.UserHostAddress
            };
            this.Admins.Insert(a);
            foreach (var item in roles)
            {
                this.AdminRoles.Insert(new AdminRole
                {
                    AdminID = a.AdminID,
                    RoleID = int.Parse(item)
                });
            }
        }

        //public void setLoginInfo(Admins user, string[] roles)
        //{
        //    var login = new LoginAdminInfo
        //    {
        //        AdminID = user.AdminID,
        //        UserName = user.UserName,
        //        DisplayName = user.DisplayName,
        //        Authorise=this.RoleAuthorises.FindAll(n=>roles.Contains(n.RoleID.ToString())).Select(s=>s.AuthoriseID).ToList()
        //    };
        //    SessionHelper.SetSession("LoginAdminInfo", login);
        //}

        public void setLoginInfo(Admins user) 
        {
            var Roles = user.Roles.Select(s => s.RoleID);
            var login = new LoginAdminInfo
            {
                AdminID = user.AdminID,
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                Authorise = this.RoleAuthorises.FindAll(n => Roles.Contains(n.RoleID)).Select(s=>s.AuthoriseID).ToList()
            };
            SessionHelper.SetSession("LoginAdminInfo", login);
        }

        public Dictionary<string, object> GetAdminsList(int pageIndex, int pageSize) 
        {
            Func<ICollection<AdminRole>, string> convert = c =>
            {
                List<string> str = new List<string>();

                foreach (var item in c)
                {
                    str.Add(item.Role.Name);
                }
                return string.Join(",", str);
            };
            var total = this.Admins.FindAll().Count();
            //AsEnumerable()没有的话，就是延时加载，这个时候无法对提取值进行操作
            var rows = this.Admins.FindAllByPage(null, o => o.AdminID, pageSize, pageIndex).AsEnumerable().Select(cm => new
            {
                AdminID = cm.AdminID,
                DisplayName = cm.DisplayName,
                UserName = cm.UserName,
                Roles = convert(cm.Roles),
                LoginTime = cm.LoginTime.Value.ToShortDateString(),
                RegistrationTime = cm.RegistrationTime.Value.ToShortDateString(),
                LoginIP = cm.LoginIP
            });

            Dictionary<string, object> json = new Dictionary<string, object>();
            json.Add("total", total);
            json.Add("rows", rows);
            return json;
        }
        public Dictionary<string, object> GetRoleList(int pageIndex, int pageSize) 
        {
            Func<ICollection<RoleAuthorise>, string> convert = c =>
            {
                List<string> str = new List<string>();
                foreach (var item in c)
                {
                    str.Add(item.Authorise.Name);
                }
                return string.Join(",", str);
            };

            var total = this.Roles.FindAll().Count();
            var rows = this.Roles.FindAllByPage(null, o => o.RoleID, pageSize, pageIndex).AsEnumerable().Select(s => new
            {
                RoleID = s.RoleID,
                Name = s.Name,
                Description = s.Description,
                Authorizes = convert(s.Authorizes),
            });
            Dictionary<string, object> json = new Dictionary<string, object>();
            json.Add("total", total);
            json.Add("rows", rows);
            return json;
        }

        public Dictionary<string, object> GetAuthList(int pageIndex, int pageSize)
        {
            
            var total = this.Authorises.FindAll().Count();
            var rows = this.Authorises.FindAllByPage(null, o => o.AuthoriseID, pageSize, pageIndex).Select(s => new
            {
                AuthoriseID = s.AuthoriseID,
                Name = s.Name,
                Description = s.Description
            });
            Dictionary<string, object> json = new Dictionary<string, object>();
            json.Add("total", total);
            json.Add("rows", rows);
            return json;
        }
    }
}
