using System.Web;
using System.Web.Optimization;

namespace OnlineShop
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css", "https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.3.0/css/font-awesome.min.css").Include(
                      "~/Assets/Client/css/bootstrap.min.css",
                      "~/Assets/Client/css/jquery-ui.css",
                      "~/Assets/Client/css/fontawesome-all.min.css",
                      "~/Assets/Client/css/flexslider.css",
                      "~/Assets/Client/css/JiSlider.css",
                      "~/Assets/Client/css/shop.css",
                      "~/Assets/Client/css/style.css",
                      "~//fonts.googleapis.com/css?family=Sunflower:500,700",
                      "~//fonts.googleapis.com/css?family=Open+Sans:400,600,700",
                      "~/Assets/Client/css/bootstrap-social.css"
                      ));
          //  BundleTable.EnableOptimizations = false;
        }
    }
}
