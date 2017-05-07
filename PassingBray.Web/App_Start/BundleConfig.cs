using System.Web.Optimization;

namespace PassingBray.Web
{

    public static class BundleConfig
    {
        /// <summary>
        /// Add  Css and Js bundles
        /// </summary>
        /// <param name="bundles"></param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Add CSS files
            bundles.Add(new StyleBundle("~/css/sitecss").Include(
                        "~/Content/Vendor/bootstrap.min.css",
                        "~/Content/Css/passingbray.css"
                        ));

            //Add SST CSS files
            bundles.Add(new ScriptBundle("~/js/sitejs").Include(
                        "~/Scripts/Vendor/jquery-1.9.1.min.js",
                        "~/Scripts/Vendor/bootstrap.min.js",
                        "~/Scripts/JqueryEvents.js",
                        "~/Scripts/ChatHubEvent.js"
                        ));
        }
    }
}