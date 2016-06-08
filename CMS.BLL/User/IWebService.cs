using CMS.BLL.User.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CMS.BLL.User
{
    public interface IWebService
    {
        object GetUserList(string title, int? roleid, DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize);
        Dictionary<string, object> GetRoleList(int pageIndex, int pageSize);
        Dictionary<string, object> GetAuthList(int pageIndex, int pageSize);
        bool AuthExit(string authname);
        void addAuth(AuthListModel al);
        void updateAuth(AuthListModel a);
        void delAuth(int authid);
        bool RoleExit(string rolename);
        void addRole(RoleAuthModel ram, string[] auth);
        IEnumerable<SelectListItem> GetRoles();
        RoleAuthModel GetSelectRole(int roleid);
        IEnumerable<SelectListItem> GetSelectRoles(string[] roles);
        IEnumerable<SelectListItem> GetAuthorises();
        IEnumerable<SelectListItem> GetSelectAuth(string[] auth);
        AuthListModel GetSelectAuth(int authid);
        void updateRole(RoleAuthModel ram, string[] auth);
        List<SelectListItem> GetValid();
        bool ckUserName(string username);
        void AddUser(UserModel um, string[] roles);
        void delUser(int userid);
        void delRole(int roleid);
        UserModel GetSelectUser(int userid);
        void updateUser(UserModel um, string[] roles);
        List<object> getRoleTree();
    }
}
