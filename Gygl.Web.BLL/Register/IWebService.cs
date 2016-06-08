using Gygl.Web.BLL.Register.Models;

namespace Gygl.Web.BLL.Register
{
    public interface IWebService
    {
        bool ckUserName(string username);
        string reg(RegViewModel rvm);
        string activate(int uid, string code);
        object login(string u, string p, bool auto);
        object logincheck();
        void loginout();
        UserViewModel getUser();
        void editUser(UserViewModel uvm);
        object forget(string u, string e);
        PasswordViewModel getUser(int uid, string code);
        PasswordViewModel updatePass(PasswordViewModel pvm);
    }
}
