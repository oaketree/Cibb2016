using System.Web;
using System.Web.Optimization;

namespace Cibb.Web.UI2
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Content/Scripts/jquery-1.11.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/idxjs").Include(
                        "~/Content/Scripts/Cibb.js",
                        "~/Content/Scripts/CibbLoad.js"));

            //论坛网站
            bundles.Add(new ScriptBundle("~/bundles/forum").Include(
                "~/Content/Scripts/common.js"));

            //新闻详细页面
            bundles.Add(new ScriptBundle("~/bundles/newsdjs").Include(
                        "~/Content/Scripts/Cibb.js",
                        "~/Content/Scripts/NewsDetail.js"));
            // 新闻首页
            bundles.Add(new ScriptBundle("~/bundles/newsjs").Include(
                       "~/Content/Scripts/Cibb.js",
                       "~/Content/Scripts/News.js"));
            //静态页面
            bundles.Add(new ScriptBundle("~/bundles/staticjs").Include(
                       "~/Content/Scripts/Cibb.js",
                       "~/Content/Scripts/Static.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/Scripts/jquery-1.11.1.min.js"));

            bundles.Add(new StyleBundle("~/Content/indexcss").Include("~/Content/css/zzsc.css", "~/Content/css/index_css1.css"));
            bundles.Add(new StyleBundle("~/Content/newscss").Include("~/Content/css/news.css", "~/Content/css/index_css1.css"));
            bundles.Add(new StyleBundle("~/Content/staticcss").Include("~/Content/css/static.css", "~/Content/css/index_css1.css"));



            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/Scripts/jquery.validate*"));

            //// 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            //// 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));
        }
    }
}
