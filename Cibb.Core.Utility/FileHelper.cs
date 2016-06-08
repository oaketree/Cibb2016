using System.IO;
using System.Web;

namespace Cibb.Core.Utility
{
    public class FileHelper
    {
        public static FileStream GetFile(string name, string ext)
        {
            string absoluFilePath = HttpContext.Current.Server.MapPath(string.Format("/Content/files/{0}.{1}", name, ext));
            //return new FileStream(absoluFilePath, FileMode.Open);
            return new FileStream(absoluFilePath, FileMode.Open, FileAccess.Read);
        }

        public static FileStream GetFile(string name)
        {
            string absoluFilePath = HttpContext.Current.Server.MapPath(string.Format("/Content/upload/forum/files/{0}", name));
            return new FileStream(absoluFilePath, FileMode.Open, FileAccess.Read);
        }

    }
}
