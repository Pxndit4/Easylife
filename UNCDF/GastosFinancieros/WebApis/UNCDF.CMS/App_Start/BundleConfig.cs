using System.Web;
using System.Web.Optimization;

namespace UNCDF.CMS
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.min.js"));

            //jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            //componentes
            bundles.Add(new ScriptBundle("~/bundles/unitlifeui").Include(
                "~/Scripts/bootstrap-table.min.js",
                "~/Scripts/bootstrap-datepicker.min.js",
                //"~/Scripts/locale/bootstrap-table-es-ES.min.js",
                "~/Scripts/locale/bootstrap-datepicker.es.min.js"));

            //jquery validation
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js"
                        ));

            //css
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap/bootstrap.min.css",
                "~/Content/bootstrap/bootstrap-table.css",
                "~/Content/Site.css",
                "~/Content/Unitlife.ui.css",
                "~/Content/bootstrap/bootstrap-datepicker.min.css"));

            //css
            bundles.Add(new StyleBundle("~/Content/cpcss").Include(
                "~/Content/jquery.bootgrid.min.css"));


            BundleTable.EnableOptimizations = true;
        }
    }
}
