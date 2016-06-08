using Cibb.Core.Cache;
using Cibb.Core.DAL;
using Cibb.Core.Utility;
using Gygl.Web.BLL.Contribute.Models;
using Gygl.Web.BLL.Share;
using Gygl.Web.Contract.Models.Contribute;
using System.Web.Mvc;

namespace Gygl.Web.BLL.Contribute
{
    public class WebService : IWebService
    {
        private IWebRepository<Contributes> contributes;
        public WebService(IWebRepository<Contributes> contributes)
        {
            this.contributes = contributes;
        }

        public bool logincheck()
        {
            if (SessionHelper.Get("AccessInfo") != null)
                return true;
            else
                return false;
        }


        public bool addContribute(ContributeViewModel cvm, ModelStateDictionary ModelState)
        {
            string fufile = string.Empty;
            var fu = new Uploader("FileUpload", new string[] { ".docx", ".doc" });
            if (!fu.checkUpload(10485760))
            {
                ModelState.AddModelError("FileUpload", "文件必须小于10M且为word格式");
                return false;
            }
            else
            {
                string savepath = "/Content/Upload/Contribute/original/";
                fu.save(savepath);
                fu.end();
                fufile = fu.FileName;
                if (ModelState.IsValid)
                {
                    var login = (AccessInfo)SessionHelper.Get("AccessInfo");
                    var uid = login.UserID;
                    var c = new Contributes
                    {
                        Title = cvm.Title,
                        Author = cvm.Author,
                        Address = cvm.Address,
                        Company = cvm.Company,
                        Email = cvm.Email,
                        ZipCode = cvm.ZipCode,
                        Remark = cvm.Remark,
                        Tel = cvm.Tel,
                        FileUpload = fufile,
                        UserID = uid,
                        IsSubmit=false,
                    };
                    this.contributes.Insert(c);
                    return true;
                }
                else
                    return false;
            }
        }
    }
}
