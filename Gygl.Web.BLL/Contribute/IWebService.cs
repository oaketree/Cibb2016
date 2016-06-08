using Gygl.Web.BLL.Contribute.Models;
using System.Web.Mvc;

namespace Gygl.Web.BLL.Contribute
{
    public interface IWebService
    {
        bool logincheck();
        bool addContribute(ContributeViewModel cvm, ModelStateDictionary ModelState);

    }
}
