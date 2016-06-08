using System.Web;
using System.Web.Optimization;

namespace CMS.Web.UI
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/scripts/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryuno").Include(
                        "~/Content/scripts/jquery.unobtrusive-ajax.min.js"));

            //// 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            //// 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/easyui").Include(
            //          "~/Content/scripts/jquery.easyui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/cms").Include(
                      "~/Content/scripts/cms.js",
                      "~/Content/scripts/cmsloader.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/css/bootstrap.min.css",
                       "~/Content/css/easyui.css",
                        "~/Content/css/icon.css",
                      "~/Content/css/Site.css"));

            

        }
    }
}
