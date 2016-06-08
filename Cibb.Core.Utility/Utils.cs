using System.Web;

namespace Cibb.Core.Utility
{
    public class Utils
    {
        public static string GetIP()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }
    }
}
