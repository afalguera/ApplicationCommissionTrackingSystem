using System.Web;
using System.Web.Optimization;

namespace MvcApplication1
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive-ajax-new.min.js",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/datatablesmin").Include(
                       "~/Scripts/DataTables-1.9.4/media/js/jquery.dataTables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bxslidermin").Include(
                       "~/Plugins/bxslider/jquery.bxslider.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajaxWrapper").Include(
                       "~/Scripts/JSHelpers/ajaxWrapper.js"));

            bundles.Add(new ScriptBundle("~/bundles/koBindings").Include(
                       "~/Scripts/knockout.bindings.dataTables.js"));

             bundles.Add(new ScriptBundle("~/bundles/koMappings").Include(
                       "~/Scripts/knockout.mapping-latest.js"));

             //Knockout
             bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                  "~/Scripts/knockout-2.2.1.js"));

            //JqueryUI JS
             bundles.Add(new ScriptBundle("~/bundles/jqueryUIJS").Include(
                  "~/Scripts/jquery-ui-1.10.2*"));

           
                     
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
             
            bundles.Add(new StyleBundle("~/Content/datatables").Include(
                "~/Content/DataTables-1.9.4/media/css/jquery.dataTables.css",
                "~/Content/DataTables-1.9.4/media/css/jquery.dataTables_themeroller.css"));

            bundles.Add(new StyleBundle("~/Content/bxslider").Include(
                "~/Plugins/bxslider/jquery.bxslider.css"));

            bundles.Add(new StyleBundle("~/Content/jqueryUICSS").Include(
                "~/Content/themes/base/minified/jquery-ui.min.css"));
            

        }
    }
}