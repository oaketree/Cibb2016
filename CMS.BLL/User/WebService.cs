using Cibb.Core.DAL;
using Cibb.Core.Utility;
using CMS.BLL.User.Models;
using CMS.Contract.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace CMS.BLL.User
{
    public class WebService : IWebService
    {
        private IWebRepository<URole> Roles;
        private IWebRepository<Users> Users;
        private IWebRepository<UAuthorise> Authorises;
        private IWebRepository<UserRole> UserRoles;
        private IWebRepository<URoleAuthorise> RoleAuthorises;
        private IWebRepository<UserDetail> UserDetails;
        public WebService(IWebRepository<URole> Roles, IWebRepository<Users> Users, IWebRepository<UAuthorise> Authorises, IWebRepository<UserRole> UserRoles, IWebRepository<URoleAuthorise> RoleAuthorises, IWebRepository<UserDetail> UserDetails)
        {
            this.Roles = Roles;
            this.Authorises = Authorises;
            this.Users = Users;
            this.UserRoles = UserRoles;
            this.RoleAuthorises = RoleAuthorises;
            this.UserDetails = UserDetails;
        }

        public object GetUserList(string title, int? roleid, DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            //Func<ICollection<UserRole>, string> convert = c =>
            //{
            //    var str=c.Select(s => s.Role.Name).ToList();
            //    return string.Join(",", str);
            //};
            //Func<bool?, string> verify = v =>
            //{
            //    if (v == null)
            //        return "无验证数据";
            //    if (v == true)
            //        return "已验证";
            //    else
            //        return "未验证";
            //};

            var ur = this.UserRoles.FindAll(n => n.RoleID == roleid).Select(s=>s.UserID).ToList();

            Expression<Func<Users, bool>> express = PredicateExtensions.True<Users>();
            if (roleid > 0)
                express = express.And(n => ur.Contains(n.UserID));
            if (!string.IsNullOrEmpty(title))
                express = express.And(n => n.UserDetail.CompanyName.Contains(title));
            if (fromDate != null)
                express = express.And(n => n.RegDate >= fromDate);
            if (toDate != null)
                express = express.And(n => n.RegDate <= toDate);
            var _total = this.Users.FindAll(express).Count();
            var _rows = this.Users.FindAllByPage(express, o => o.RegDate, pageSize, pageIndex).Select(c => new
            {
                UserID = c.UserID,
                UserName = c.UserName,
                DisplayName = c.UserDetail.DisplayName,
                LoginTime = c.lastdate,
                UserLoginNum = c.UserLoginNum,
                RegistrationTime = c.RegDate,
                LoginIP = c.LoginIP,
                Email = c.UserDetail.Email,
                Tel = c.UserDetail.Tel,
                CompanyName = c.UserDetail.CompanyName,
                Roles = c.Roles,
                Valid = c.Valid,
                EntryPoint=c.EntryPoint
            });
            //AsEnumerable()没有的话，就是延时加载，这个时候无法对提取值进行操作
            var retRow = _rows.AsEnumerable().Select(c => new
            {
                UserID = c.UserID,
                UserName = c.UserName,
                DisplayName = c.DisplayName,
                Roles = string.Join(",", c.Roles.Select(s => s.Role.Name)),
                Valid = c.Valid==true?"已验证": "未验证",
                LoginTime = c.LoginTime,
                UserLoginNum = c.UserLoginNum,
                RegistrationTime = c.RegistrationTime,
                LoginIP = c.LoginIP,
                Email = c.Email,
                Tel = c.Tel,
                CompanyName = c.CompanyName,
                EntryPoint=c.EntryPoint.ToString()
        });
            return new { total = _total, rows = retRow };
        }

        public Dictionary<string, object> GetRoleList(int pageIndex, int pageSize)
        {
            Func<ICollection<URoleAuthorise>, string> convert = c =>
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
                GetID=s.RoleID.ToString()
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
                Description = s.Description,
                GetID=s.AuthoriseID.ToString()
            });
            Dictionary<string, object> json = new Dictionary<string, object>();
            json.Add("total", total);
            json.Add("rows", rows);
            return json;
        }

        public List<object> getRoleTree()
        {
            List<object> ddl = new List<object>();
            ddl.Add(new  { id = 0, text = "所有角色" });
            ddl.AddRange(Roles.FindAll().Select(s => new 
            {
                id = s.RoleID,
                text = s.Name,
            }).AsEnumerable());
            //List<DropDownListJson> ddl = this.Roles.FindAll().Select(s => new DropDownListJson
            //{
            //    id=s.RoleID,
            //    text=s.Name,
            //}).ToList();
            return ddl;
        }

        public bool AuthExit(string authname)
        {
            var f = this.Authorises.Find(n => n.Name == authname);
            if (f != null)
                return true;
            else
                return false;
        }

        public void addAuth(AuthListModel al)
        {
            this.Authorises.Insert(new UAuthorise
            {
                Name = al.Name,
                Description = al.Description
            });
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

        public void updateAuth(AuthListModel a)
        {
            //var auth = this.Authorises.Find(n => n.AuthoriseID == a.AuthoriseID);
            var auth = this.Authorises.GetById(a.AuthoriseID);
            auth.Name = a.Name;
            auth.Description = a.Description;
            this.Authorises.Update(auth);
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
        /// 角色存在判断
        /// </summary>
        /// <param name="rolename">角色名</param>
        /// <returns></returns>
        public bool RoleExit(string rolename)
        {
            var f = this.Roles.Find(n => n.Name == rolename);
            if (f != null)
                return true;
            else
                return false;
        }

        public void addRole(RoleAuthModel ram, string[] auth)
        {
            var r = new URole
            {
                Name = ram.Name,
                Description = ram.Description,
            };
            this.Roles.Insert(r);
            foreach (var item in auth)
            {
                this.RoleAuthorises.Insert(new URoleAuthorise
                {
                    RoleID = r.RoleID,
                    AuthoriseID = int.Parse(item)
                });
            }
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

        public RoleAuthModel GetSelectRole(int roleid)
        {
            Func<URole, IEnumerable<SelectListItem>> sa = c =>
            {
                List<SelectListItem> selectroles = this.Authorises.FindAll().Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.AuthoriseID.ToString(),
                    Selected = false
                }).ToList();
                foreach (var item in selectroles)
                {
                    foreach (var sr in c.Authorizes)
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
            var r = this.Roles.GetById(roleid);
            var ra = new RoleAuthModel
            {
                RoleID = r.RoleID,
                Name = r.Name,
                Description = r.Description,
                Authorises = sa(r)
            };
            return ra;
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
                    this.RoleAuthorises.Insert(new URoleAuthorise
                    {
                        AuthoriseID = int.Parse(item),
                        RoleID = r.RoleID
                    });
                }
            }
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

            var ar = this.UserRoles.FindAll(n => n.RoleID == roleid).ToList();
            foreach (var item in ar)
            {
                this.UserRoles.Delete(item);
            }

            this.Roles.Delete(roleid);
        }


        public List<SelectListItem> GetValid()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text="已验证",Value="true"},
                new SelectListItem{Text="未验证",Value="false"},
            };
        }

        public bool ckUserName(string username)
        {
            return this.Users.FindAll(n => n.UserName == username).Count() == 0;
        }


        public void AddUser(UserModel um, string[] roles)
        {
            var u = new Users
            {
                UserName = um.UserName,
                Password= Security.Sha256(um.UserPassword),
                UserPassword =um.UserPassword,
                //DisplayName = um.DisplayName,
                Valid = Convert.ToBoolean(um.Valid),
                LoginIP = HttpContext.Current.Request.UserHostAddress,
                //CompanyName = um.CompanyName,
                //Email = um.Email,
                //Tel = um.Tel,
                //Address = um.Address,
                UserLoginNum = 0,
                lastdate = DateTime.Now,
            };
            this.Users.Insert(u);

            this.UserDetails.Insert(new UserDetail
            {
                DisplayName = um.DisplayName,
                CompanyName = um.CompanyName,
                Email = um.Email,
                Address = um.Address,
                Tel = um.Tel,
                UserID = u.UserID,
            });
            foreach (var item in roles)
            {
                this.UserRoles.Insert(new UserRole
                {
                    UserID = u.UserID,
                    RoleID = int.Parse(item)
                });
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userid"></param>
        public void delUser(int userid)
        {
            var ur = this.UserRoles.FindAll(n => n.UserID == userid).ToList();
            foreach (var item in ur)
            {
                this.UserRoles.Delete(item);
            }
            this.Users.Delete(userid);
            this.UserDetails.Delete(userid);
        }

        public UserModel GetSelectUser(int userid)
        {
            Func<ICollection<UserRole>, IEnumerable<SelectListItem>> sr = c =>
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
            Func<bool?, List<SelectListItem>> vl = v =>
            {
                List<SelectListItem> ddl = new List<SelectListItem>
                {
                    new SelectListItem{Text="已验证",Value="true"},
                    new SelectListItem{Text="未验证",Value="false"},
                };
                if (v != null)
                {
                    foreach (var item in ddl)
                    {
                        if (Convert.ToBoolean(item.Value) == v)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
                return ddl;
            };
            //var u = this.Users.Find(n => n.UserID == userid);
            var u = this.Users.GetById(userid);
            var uv = new UserModel
            {
                UserID = u.UserID,
                UserName = u.UserName,
                DisplayName = u.UserDetail.DisplayName,
                CompanyName = u.UserDetail.CompanyName,
                Email = u.UserDetail.Email,
                Tel = u.UserDetail.Tel,
                Address = u.UserDetail.Address,
                Roles = sr(u.Roles),
                UserPassword=u.UserPassword,
                ValidList = vl(u.Valid),
            };
            return uv;
        }

        public void updateUser(UserModel um, string[] roles)
        {
            
            var u = this.Users.GetById(um.UserID);
            u.UserPassword =um.UserPassword;
            u.Password= Security.Sha256(um.UserPassword);
            u.UserDetail.DisplayName = um.DisplayName;
            u.UserDetail.CompanyName = um.CompanyName;
            u.UserDetail.Email = um.Email;
            u.UserDetail.Tel = um.Tel;
            u.UserDetail.Address = um.Address;
            u.Valid = Convert.ToBoolean(um.Valid);
            this.Users.Update();


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
            if (oldroles.Count > 0)
            {
                foreach (var item in oldroles)
                {
                    this.UserRoles.Delete(item.RelationID);
                }
            }
            if (newroles.Count > 0)
            {
                foreach (var item in newroles)
                {
                    this.UserRoles.Insert(new UserRole
                    {
                        UserID = u.UserID,
                        RoleID = int.Parse(item)
                    });
                }
            }
        }
    }


}
