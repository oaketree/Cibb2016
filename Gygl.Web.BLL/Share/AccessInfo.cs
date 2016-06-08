using System.Collections.Generic;

namespace Gygl.Web.BLL.Share
{
    /// <summary>
    /// 登录信息，以后其他类调用
    /// </summary>
    public class AccessInfo
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public List<int> Authorise { get; set; }
    }
}
