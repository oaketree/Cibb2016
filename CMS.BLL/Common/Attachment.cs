using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace CMS.BLL.Common
{
    public class Attachment
    {
        private string savePath;
        public Attachment(string savePath)
        {
            this.savePath = savePath;
        }
        public Dictionary<string, object> save()
        {
            //定义允许上传的文件扩展名
            Hashtable extTable = new Hashtable();
            extTable.Add("image", "gif,jpg,jpeg,png");
            //extTable.Add("flash", "swf,flv");
            //extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
            extTable.Add("file", "doc,docx,xls,xlsx,ppt,pptx,pdf,zip,rar");

            //最大文件大小
            int maxSize = 1000000;

            HttpPostedFile imgFile = HttpContext.Current.Request.Files["imgFile"];
            if (imgFile == null)
            {
                return showError("请选择文件。");
            }

            //物理地址
            string dirPath = HttpContext.Current.Server.MapPath(savePath);
            if (!Directory.Exists(dirPath))
            {
                return showError("上传目录不存在。");
            }

            string dirName = HttpContext.Current.Request.QueryString["dir"];
            if (string.IsNullOrEmpty(dirName))
            {
                dirName = "image";
            }
            if (!extTable.ContainsKey(dirName))
            {
                return showError("目录名不正确。");
            }

            string fileName = imgFile.FileName;
            string fileExt = Path.GetExtension(fileName).ToLower();

            if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            {
                return showError("上传文件大小超过限制。");
            }

            if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(((string)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                return showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((string)extTable[dirName]) + "格式。");
            }

            //创建文件夹
            dirPath += dirName + "/";
            savePath += dirName + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMdd_hhmmssfff") + fileExt;
            string filePath = dirPath + newFileName;
            imgFile.SaveAs(filePath);

            string fileUrl = savePath + newFileName;

            Dictionary<string, object> json = new Dictionary<string, object>();
            json.Add("error", 0);
            json.Add("message", fileUrl);
            return json;

        }

        public Dictionary<string, object> showError(string message)
        {
            Dictionary<string, object> json = new Dictionary<string, object>();
            json.Add("error", 1);
            json.Add("message", message);
            return json;
        }
    }
}
