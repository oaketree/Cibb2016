using System.Collections.Generic;

namespace Cibb.Web.BLL.Model
{
    public class LoginUserInfo
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public List<int> Authorise { get; set; }
        //public string DisplayName { get; set; }
    }
}
