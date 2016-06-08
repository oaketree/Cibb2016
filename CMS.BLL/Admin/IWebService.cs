using CMS.BLL.Admin.Models;
using CMS.Contract.Models.Admin;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CMS.BLL.Admin
{
    public interface IWebService
    {
        IEnumerable<SelectListItem> GetAuthorises(); 
        IEnumerable<SelectListItem> GetRoles();
        IEnumerable<SelectListItem> GetSelectRoles(string[] roles);
        IEnumerable<SelectListItem> GetSelectAuth(string[] auth);
        RoleAuthModel GetSelectRole(int roleid);
        AdminModel GetSelectUser(int userid);
        AuthListModel GetSelectAuth(int authid);
        bool Exist(string username);
        bool RoleExit(string rolename);
        bool AuthExit(string authname);
        Admins FindUser(string username);
        Admins FindUser(int userid);
        void delUser(int userid);
        void delRole(int roleid);
        void delAuth(int authid);
        Admins CheckUser(string username, string password);
        void addAuth(AuthListModel al);
        void addRole(RoleAuthModel ram, string[] auth);
        void AddUser(RegisterModel rm,string[] roles);
        void updateAuth(AuthListModel a);
        void updateUser(Admins user);
        void updateRole(RoleAuthModel ram, string[] auth);
        void updateUser(AdminModel am, string[] roles);
        void setLoginInfo(Admins user);
        Dictionary<string, object> GetAdminsList(int pageIndex, int pageSize);
        Dictionary<string, object> GetRoleList(int pageIndex, int pageSize);
        Dictionary<string, object> GetAuthList(int pageIndex, int pageSize);
    }
}
