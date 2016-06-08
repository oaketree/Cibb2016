using Cibb.Web.BLL.Registration.Models;
using System.Collections.Generic;

namespace Cibb.Web.BLL.Registration
{
    public interface IWebService
    {
        bool ckUserName(string username);
        //int addUser(UserModel um);
        //bool sendEmail(int uid);
        string register(UserModel um);
        string activate(int uid, string code);
        Dictionary<string, object> login(string u, string p);
        void loginout();
        Dictionary<string, object> logincheck();
    }
}
