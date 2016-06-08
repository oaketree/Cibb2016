using Cibb.Core.Cache;
using Cibb.Web.BLL.Model;
using System.Web;
using System.Web.Mvc;

namespace Cibb.Web.BLL.Attributes
{
    public class PermissionAttribute : AuthorizeAttribute
    {
        public string authorize { set; get; }//要验证的权限的代码
        private string text;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (AccessCheck(authorize)) return true;
            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new ContentResult { Content= "<script>alert('" + this.text+ "');window.close();</script>" };
            //filterContext.Result = new JavaScriptResult { Script = "alert('" + this.text + "')" };
            //filterContext.Result = new RedirectResult("/Admin/Admin/Login");
            //base.HandleUnauthorizedRequest(filterContext);


        }

        private bool AccessCheck(string authorize)
        {
            var login = (LoginUserInfo)SessionHelper.Get("LoginUserInfo");
            if (login == null)
            {
                this.text = "请登录后下载演讲报告";
                return false;
            }
            else
            {
                if (string.IsNullOrEmpty(authorize))
                    return true;
                else
                {
                    //无权限
                    if (login.Authorise.Contains(6))
                    {
                        this.text = "该帐号已禁用!";
                        return false;
                    }
                    else 
                    {
                        if (login.Authorise.Contains(int.Parse(authorize)))
                            return true;
                        this.text = "需要访问权限"+authorize;
                        return false;
                    }
                }
            }
        }
    }
}
