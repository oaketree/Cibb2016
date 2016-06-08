using Cibb.Core.Cache;
using Cibb.Core.DAL;
using Cibb.Core.Utility;
using Cibb.Web.BLL.Model;
using Cibb.Web.BLL.Registration.Models;
using Cibb.Web.Contract.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cibb.Web.BLL.Registration
{
    public class WebService:IWebService
    {
        private IWebRepository<Users> users;
        private IWebRepository<UserRole> usersRole;
        private IWebRepository<RoleAuthorise> roleAuthorise;
        private IWebRepository<UserDetail> UserDetails;
        public WebService(IWebRepository<Users> users, IWebRepository<UserRole> usersRole, IWebRepository<RoleAuthorise> roleAuthorise, IWebRepository<UserDetail> UserDetails)
        {
            this.users = users;
            this.usersRole = usersRole;
            this.roleAuthorise = roleAuthorise;
            this.UserDetails = UserDetails;
        }
        public bool ckUserName(string username)
        {
            return this.users.FindAll(n => n.UserName == username).Count() == 0;
        }

        public string register(UserModel um)
        {
            var au = new Users
            {
                UserName = um.UserName,
                UserPassword = um.UserPassword,
                Password = Security.Sha256(um.UserPassword),
                //DisplayName = um.DisplayName,
                LoginIP = HttpContext.Current.Request.UserHostAddress,
                //CompanyName = um.CompanyName,
                //Email = um.Email,
                //Tel = um.Tel,
                //Address = um.Address,
                UserLoginNum = 0,
                Valid = false,
                lastdate = DateTime.Now,
                ActiveCode = Guid.NewGuid().ToString("N")
            };
            this.users.Insert(au);

            this.UserDetails.Insert(new UserDetail
            {
                DisplayName = um.DisplayName,
                CompanyName = um.CompanyName,
                Email = um.Email,
                Address = um.Address,
                Tel = um.Tel,
                UserID = au.UserID,
            });

            this.usersRole.Insert(new UserRole
            {
                UserID = au.UserID,
                RoleID = 1
            });
            return sendRegistermail(au.UserName, au.UserID.ToString(), au.ActiveCode, um.Email);
        }

        private string sendRegistermail(string username, string userid,string activecode,string email)
        {
            string sub = "中国电器工业协会工业锅炉分会—邮箱验证";
            string[] b ={"<p>亲爱的{0}</p>",
"<p>您好！感谢您注册工业协会工业锅炉分会会员！</p>",
"<p>请点击以下链接验证您的接收邮箱：<a href=\"http://www.cibb.net.cn/Forum/Registration/Activate?uid={1}&amp;code={2}\" target=\"_blank\">验证电子邮件地址</a></p>"};
            string body = string.Format(string.Join(",", b), username, userid, activecode);
            string emailFrom = "glxh09@163.com";
            string smtp = "smtp.163.com";
            string emailLoginName = "glxh09";
            string emailLoginpassword = "51803299";
            var e = new Email(sub, body, emailFrom, email);
            return e.send(smtp, emailLoginName, emailLoginpassword);
        }

        public string activate(int uid, string code)
        {
            string r = string.Empty;
            var u = this.users.Find(n=>n.UserID==uid&&n.ActiveCode==code);
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
            else {
                r = "用户信息不存在。";
            }
            return r;
        }

        public Dictionary<string, object> login(string u, string p)
        {
            Dictionary<string, object> json = new Dictionary<string, object>();
            var sp = Security.Sha256(p);
            var usr = this.users.Find(n => n.UserName == u && n.Password == sp);
            if (usr == null)
            {
                json.Add("status", 0);
                json.Add("text", "用户名或密码错误！");
                return json;
            }
            else
            {
                if (usr.Valid == false)
                {
                    //注册邮箱激活处理
                    if (string.IsNullOrEmpty(usr.ActiveCode))
                    {
                        var ac= Guid.NewGuid().ToString("N");
                        usr.ActiveCode = ac;
                        this.users.Update();
                        sendRegistermail(usr.UserName, usr.UserID.ToString(), ac, usr.UserDetail.Email);
                    }
                    json.Add("status", 0);
                    json.Add("text", "该账号尚未激活，请至注册邮箱激活后登录！");
                    return json;
                }
                else 
                {
                    var ur = this.usersRole.FindAll(n => n.UserID == usr.UserID);//查询用户角色
                    //自动转换为协会会员
                    if (ur.Count() == 0)
                    {
                        this.usersRole.Insert(new UserRole
                        {
                            UserID = usr.UserID,
                            RoleID = 1
                        });
                    }
                    else
                    {
                        var rid=ur.Select(s => s.RoleID).ToList();
                        if (!rid.Contains(1))
                        {
                            this.usersRole.Insert(new UserRole
                            {
                                UserID = usr.UserID,
                                RoleID = 1
                            });
                        }
                    }
                    usr.LoginIP = HttpContext.Current.Request.UserHostAddress;
                    usr.UserLoginNum = usr.UserLoginNum + 1;
                    usr.lastdate = DateTime.Now;
                    usr.EntryPoint = EntryPoint.Forum;
                    this.users.Update(usr);

                    var roles = usr.Roles.Select(s => s.RoleID);
                    SessionHelper.SetSession("LoginUserInfo", new LoginUserInfo
                    {
                        UserID = usr.UserID,
                        UserName = usr.UserName,
                        //DisplayName = usr.DisplayName,
                        Authorise = this.roleAuthorise.FindAll(n => roles.Contains(n.RoleID)).Select(s => s.AuthoriseID).ToList()
                    });
                    json.Add("status", 1);
                    json.Add("text", "您好，"+usr.UserName);
                    return json;
                }
            }

        }
        public void loginout()
        {
            SessionHelper.Del("LoginUserInfo");
        }

        //刷新页面检查session
        public Dictionary<string, object> logincheck()
        {
            Dictionary<string, object> json = new Dictionary<string, object>();
            if (SessionHelper.Get("LoginUserInfo") != null)
            {
                var login = (LoginUserInfo)SessionHelper.Get("LoginUserInfo");
                json.Add("status", 1);
                json.Add("text", "您好，" + login.UserName);
            }
            else
            {
                json.Add("status", 0);
            }
            return json;
        }
    }
}
