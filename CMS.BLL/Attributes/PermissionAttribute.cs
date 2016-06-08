using Cibb.Core.Cache;
using CMS.BLL.Admin.Models;
using System.Web;
using System.Web.Mvc;

namespace CMS.BLL.Attributes
{
    public class PermissionAttribute : AuthorizeAttribute
    {
        public string authorize { set; get; }//要验证的权限的代码

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (AccessCheck(authorize)) return true;
            return base.AuthorizeCore(httpContext);
        }
        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    base.OnAuthorization(filterContext);
        //}

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.Write(" <script type='text/javascript'> alert('您没有访问当前页面的权限！');</script>");
            filterContext.RequestContext.HttpContext.Response.End();
            filterContext.Result = new EmptyResult();

            //filterContext.Result = new ContentResult { Content = "无执行权限！" };
            //filterContext.Result = new RedirectResult("/Admin/Admin/Login");
            //base.HandleUnauthorizedRequest(filterContext);
        }

        private bool AccessCheck(string authorize)
        {
            var login = (LoginAdminInfo)SessionHelper.Get("LoginAdminInfo");
            if (login == null)
            {
                return false;
            }
            else
            {
                if (string.IsNullOrEmpty(authorize))
                    return true;
                else
                {
                    //foreach (var item in login.Authorise) {
                    //    if (item.ToString() == authorize)
                    //        return true;
                    //}
                    if (login.Authorise.Contains(int.Parse(authorize)))
                        return true;
                    return false;
                    //var context = new WebRepository<Admins, WebDBContext>();
                    //var u = context.Find(n => n.UserName == a.ToString());
                    //if (u != null)
                    //{
                    //    foreach (var r in u.Roles)
                    //    {
                    //        foreach (var au in r.Role.Authorizes)
                    //        {
                    //            if (authorize == au.AuthoriseID.ToString())
                    //                return true;
                    //        }
                    //    }
                    //    return false;
                    //}
                    //else
                    //{
                    //    return false;
                    //}
                }
            }
        }
    }
}
