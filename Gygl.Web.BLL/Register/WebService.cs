using Cibb.Core.Cache;
using Cibb.Core.DAL;
using Cibb.Core.Utility;
using Gygl.Web.BLL.Register.Models;
using Gygl.Web.BLL.Share;
using Gygl.Web.Contract.Models.User;
using System;
using System.Linq;
using System.Web;

namespace Gygl.Web.BLL.Register
{
    public class WebService : IWebService
    {
        private IWebRepository<Users> users;
        private IWebRepository<UserRole> usersRole;
        private IWebRepository<RoleAuthorise> roleAuthorise;
        private IWebRepository<UserDetail> userDetails;

        public WebService(IWebRepository<Users> users, IWebRepository<UserRole> usersRole, IWebRepository<RoleAuthorise> roleAuthorise, IWebRepository<UserDetail> userDetails)
        {
            this.users = users;
            this.usersRole = usersRole;
            this.roleAuthorise = roleAuthorise;
            this.userDetails = userDetails;
        }

        public bool ckUserName(string username)
        {
            return  this.users.FindAll(n => n.UserName == username).Count() == 0;
        }

        public string reg(RegViewModel rvm)
        {
            var au = new Users
            {
                UserName = rvm.UserName,
                UserPassword = rvm.UserPassword,
                Password = Security.Sha256(rvm.UserPassword),
                LoginIP = HttpContext.Current.Request.UserHostAddress,
                UserLoginNum = 0,
                Valid = false,
                lastdate = DateTime.Now,
                ActiveCode = Guid.NewGuid().ToString("N")
            };
            this.users.Insert(au);

            this.userDetails.Insert(new UserDetail
            {
                DisplayName = rvm.DisplayName,
                CompanyName = rvm.CompanyName,
                Email = rvm.Email,
                Address = rvm.Address,
                Tel = rvm.Tel,
                UserID = au.UserID,
            });

            this.usersRole.Insert(new UserRole
            {
                UserID = au.UserID,
                RoleID = 4
            });
            return sendRegistermail(au.UserName, au.UserID.ToString(), au.ActiveCode, rvm.Email);
        }

        private string sendRegistermail(string username, string userid, string activecode, string email)
        {
            string sub = "《工业锅炉》杂志—邮箱验证";
            string[] b ={"<p>亲爱的{0}</p>",
"<p>您好！感谢您注册《工业锅炉》杂志会员！</p>",
"<p>请点击以下链接验证您的接收邮箱：<a href=\"http://gygl.boilerchina.cn/Registration/Activate?uid={1}&amp;code={2}\" target=\"_blank\">验证电子邮件地址</a></p>"};
            string body = string.Format(string.Join(",", b), username, userid, activecode);
            string emailFrom = "shgygl@126.com";
            string smtp = "smtp.126.com";
            string emailLoginName = "shgygl";
            string emailLoginpassword = "bjb142900";
            var e = new Email(sub, body, emailFrom, email);
            return e.send(smtp, emailLoginName, emailLoginpassword);
        }

        public string activate(int uid, string code)
        {
            string r = string.Empty;
            var u = this.users.Find(n => n.UserID == uid && n.ActiveCode == code);
            if (u != null)
            {
                if (!u.Valid.Value)
                {
                    u.Valid = true;
                    this.users.Update(u);
                    r = "恭喜您的帐号已经激活。";
                }
                else
                {
                    r = "帐号已经激活。";
                }
            }
            else
            {
                r = "用户信息不存在。";
            }
            return r;
        }
        public object login(string u, string p,bool auto)
        {
            var sp = Security.Sha256(p);
            var usr = this.users.Find(n => n.UserName == u && n.Password == sp);
            if (usr == null)
            {
                return new { status = 0, text = "用户名或密码错误！" };
            }
            else
            {
                if (usr.Valid == false)
                {
                    //注册邮箱激活处理
                    if (string.IsNullOrEmpty(usr.ActiveCode))
                    {
                        var ac = Guid.NewGuid().ToString("N");
                        usr.ActiveCode = ac;
                        this.users.Update(usr);
                        sendRegistermail(usr.UserName, usr.UserID.ToString(), ac, usr.UserDetail.Email);
                    }
                    return new { status = 0, text = "该账号尚未激活，请至注册邮箱激活后登录！" };
                }
                else
                {
                    var ur = this.usersRole.FindAll(n => n.UserID == usr.UserID);//查询用户角色
                    //自动添加杂志会员
                    if (ur.Count() == 0)
                    {
                        this.usersRole.Insert(new UserRole
                        {
                            UserID = usr.UserID,
                            RoleID = 4
                        });
                    }
                    else
                    {
                        var rid = ur.Select(s => s.RoleID).ToList();
                        if (!rid.Contains(4))
                        {
                            this.usersRole.Insert(new UserRole
                            {
                                UserID = usr.UserID,
                                RoleID = 4
                            });
                        }
                    }
                    usr.LoginIP = HttpContext.Current.Request.UserHostAddress;
                    usr.UserLoginNum = usr.UserLoginNum + 1;
                    usr.lastdate = DateTime.Now;
                    usr.EntryPoint = EntryPoint.Magazine;
                    this.users.Update(usr);

                    var roles = usr.Roles.Select(s => s.RoleID);
                    SessionHelper.SetSession("AccessInfo", new AccessInfo{
                        UserID = usr.UserID,
                        UserName = usr.UserName,
                        Authorise = this.roleAuthorise.FindAll(n => roles.Contains(n.RoleID)).Select(s => s.AuthoriseID).ToList()
                    });
                    if (auto){
                        CookieHelper.SetCookie("User", usr.UserName);
                        CookieHelper.SetCookie("Password", usr.Password);
                    }
                    return new { status = 1, text = string.Format("您好，{0}", usr.UserName) };
                }
            }
        }

        public object logincheck()
        {
            if (SessionHelper.Get("AccessInfo") != null)
            {
                var login = (AccessInfo)SessionHelper.Get("AccessInfo");
                return new { status = 1, text = string.Format("您好，{0}", login.UserName) };
            }
            else
            {
                var u = CookieHelper.GetCookieValue("User");
                var p = CookieHelper.GetCookieValue("Password");
                if (u != string.Empty && p!= string.Empty)
                {
                    var user = this.users.Find(n => n.UserName == u && n.Password == p);
                    if (user != null)
                    {
                        var roles = user.Roles.Select(s => s.RoleID);
                        SessionHelper.SetSession("AccessInfo", new AccessInfo
                        {
                            UserID = user.UserID,
                            UserName = user.UserName,
                            Authorise = this.roleAuthorise.FindAll(n => roles.Contains(n.RoleID)).Select(s => s.AuthoriseID).ToList()
                        });
                        return new { status = 1, text = string.Format("您好，{0}", user.UserName) };
                    }
                    else {
                        CookieHelper.ClearCookie("User");
                        CookieHelper.ClearCookie("Password");
                        return new { status = 0 };
                    }
                }
                else {
                    return new { status = 0 };
                }
            }
        }
        public void loginout()
        {
            CookieHelper.ClearCookie("User");
            CookieHelper.ClearCookie("Password");
            SessionHelper.Del("AccessInfo");
        }

        public object forget(string u, string e)
        {
            var usr = this.users.Find(n => n.UserName == u && n.UserDetail.Email==e);
            if (usr == null){
                return new { status = 0, text = "用户名或邮箱错误！" };
            }
            else {
                if (string.IsNullOrEmpty(usr.ActiveCode))
                {
                    var ac = Guid.NewGuid().ToString("N");
                    usr.ActiveCode = ac;
                    this.users.Update(usr);
                }
                return new { status = 1,uid=usr.UserID,code=usr.ActiveCode };
            }
        }

        public PasswordViewModel getUser(int uid, string code)
        {
            var user = this.users.Find(n => n.UserID == uid && n.ActiveCode == code);
            if (user == null){
                return null;
            }
            else
            {
                return new PasswordViewModel
                {
                    UserID = user.UserID,
                    UserName = user.UserName
                };
            }
        }

        public PasswordViewModel updatePass(PasswordViewModel pvm)
        {
            var usr = this.users.GetById(pvm.UserID);
            usr.UserPassword = pvm.UserPassword;
            usr.Password= Security.Sha256(pvm.UserPassword);
            this.users.Update(usr);
            return new PasswordViewModel
            {
                UserName = usr.UserName,
                UserPassword = usr.UserPassword,
            };
        }
        public UserViewModel getUser()
        {
            var login = (AccessInfo)SessionHelper.Get("AccessInfo");
            var uid = login.UserID;
            var user = this.userDetails.GetById(uid);
            return new UserViewModel
            {
                UserID=user.UserID,
                DisplayName=user.DisplayName,
                Email=user.Email,
                CompanyName=user.CompanyName,
                Address=user.Address,
                Tel=user.Tel
            };
        }
        public void editUser(UserViewModel uvm)
        {
            var ud = this.userDetails.GetById(uvm.UserID);
            ud.DisplayName = uvm.DisplayName;
            ud.CompanyName = uvm.CompanyName;
            ud.Email = uvm.Email;
            ud.Address = uvm.Address;
            this.userDetails.Update(ud);
        }
    }
}
