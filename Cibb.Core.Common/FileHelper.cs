using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cibb.Core.Common
{
    public class FileHelper
    {
        public static File GetFile(string name,string ext){
            string absoluFilePath = HttpContext.Current.Server.MapPath(string.Format("/Content/files/{0}.{1}",name,ext));
            return File(new FileStream(absoluFilePath, FileMode.Open),)

        }
    }
}
