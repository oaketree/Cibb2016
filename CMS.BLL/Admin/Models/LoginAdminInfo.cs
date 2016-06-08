using System.Collections.Generic;

namespace CMS.BLL.Admin.Models
{
    public class LoginAdminInfo
    {
        public int AdminID { get; set; }
        public string UserName { get; set; }
        public List<int> Authorise { get; set; }
        public string DisplayName { get; set; }
    }
}
