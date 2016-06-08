using Cibb.Core.DAL;
using Cibb.Core.Utility;
using CMS.Contract.Models.User;
using System.Linq;
using System.Text;
/// <summary>
/// 数据处理类
/// </summary>
namespace CMS.BLL.Data
{
    public class WebService:IWebService
    {
        private IWebRepository<Users> Users;
        private IWebRepository<UserRole> UserRoles;
        private IWebRepository<UserDetail> UserDetails;
        private IWebRepository<Company> Companys;
        private IWebRepository<Person> Persons;

        public WebService(IWebRepository<UserDetail> UserDetails, IWebRepository<Users> Users, IWebRepository<UserRole> UserRoles, IWebRepository<Company> Companys, IWebRepository<Person> Persons)
        {
            this.UserDetails = UserDetails;
            this.Users = Users;
            this.UserRoles = UserRoles;
            this.Companys = Companys;
            this.Persons = Persons;
        }

        public string getPasswordXH()
        {
            //协会会员password
            var uid = this.UserRoles.FindAll(n => n.RoleID == 1).Select(s => s.UserID).ToList();
            //协会会员密码
            var pp = this.Users.FindAll(n => uid.Contains(n.UserID)).Select(s=>s.UserPassword).ToList();

            StringBuilder a = new StringBuilder();
            foreach (var p in pp)
            {
                a.Append(p);
                a.Append("|");
            }
            return a.ToString();

        }
        /// <summary>
        /// 协会会员密码copy
        /// </summary>
        /// <returns></returns>
        public string PasswordXHCopy()
        {
            //协会会员password
            //var uid = this.UserRoles.FindAll(n => n.RoleID == 1).Select(s => s.UserID).ToList();
            ////用户
            //var pp = this.Users.FindAll(n => uid.Contains(n.UserID));

            //foreach (var p in pp)
            //{
            //    p.Password = p.UserPassword;
            //}
            //this.Users.Update();
            return "ok";
        }
        //其他copy进入userdetail
        public string copyotherXH()
        {
            //协会会员password
            var uid = this.UserRoles.FindAll(n => n.RoleID == 1).Select(s => s.UserID).ToList();
            //用户
            var pp = this.Users.FindAll(n => uid.Contains(n.UserID));

            //foreach (var p in pp)
            //{
            //    this.UserDetails.Insert(new UserDetail
            //    {
            //        Email = p.Email,
            //        Address = p.Address,
            //        DisplayName = p.DisplayName,
            //        CompanyName = p.CompanyName,
            //        Tel = p.Tel,
            //        UserID = p.UserID,
            //    });
            //}
            return "ok";
        }

        public string copyother()
        {
            //var co = this.Companys.FindAll();
            //foreach (var item in co)
            //{
            //    if (!string.IsNullOrEmpty(item.CompanyEmail))
            //    {
            //        this.UserDetails.Insert(new UserDetail
            //        {
            //            Email = item.CompanyEmail,
            //            Address = item.CompanyAddress,
            //            DisplayName = item.CompanyPeople,
            //            CompanyName = item.CompanyName,
            //            Tel = item.CompanyPhone,
            //            QQ = item.QQ,
            //            UserID = item.UserID,
            //        });
            //    }
            //}
            return "ok";

        }

        public string changeName()
        {
            var co = this.Companys.FindAll();
            foreach (var item in co)
            {
                var uid = item.UserID;
                var uds = this.UserDetails.GetById(uid);
                uds.DisplayName = item.Delegate;
            }
            this.UserDetails.Update();
            return "ok";
        }
        public string copyperson()
        {
            //var pson = this.Persons.FindAll();
            //foreach (var item in pson)
            //{
            //    if (!string.IsNullOrEmpty(item.Email))
            //    {
            //        this.UserDetails.Insert(new UserDetail
            //        {
            //            Email = item.Email,
            //            Address = item.Address,
            //            DisplayName = item.Name,
            //            Tel = item.Tel,
            //            UserID = item.UserID,
            //        });
            //    }
            //}
            return "ok";
        }



        /// <summary>
        /// 密码转换copy
        /// </summary>
        /// <returns></returns>
        public string pcv()
        {
            //var user = this.Users.FindAll();
            //foreach (var item in user)
            //{
            //    var up = item.UserPassword;
            //    item.Password= Security.Sha256(up);
            //}
            //this.Users.Update();
            return "ok";
        }
    }
}
