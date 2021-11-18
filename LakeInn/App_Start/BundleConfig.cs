using System.Web;
using System.Web.Optimization;

namespace LakeInn
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

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new ScriptBundle("~/admin/js").Include(
                      "~/Areas/Administrator/TempLTE/plugins/jquery/jquery.min.js",
                      "~/Areas/Administrator/TempLTE/plugins/jquery-ui/jquery-ui.min.js",
                      "~/Scripts/jquery.validate.min.js",
                      "~/Scripts/jquery.validate.unobtrusive.min.js",
                      "~/Areas/Administrator/TempLTE/plugins/bootstrap/js/bootstrap.bundle.min.js",
                      "~/Areas/Administrator/TempLTE/plugins/chart.js/Chart.min.js",
                      "~/Areas/Administrator/TempLTE/plugins/sparklines/sparkline.js",
                      "~/Areas/Administrator/TempLTE/plugins/jqvmap/jquery.vmap.min.js",
                      "~/Areas/Administrator/TempLTE/plugins/jqvmap/maps/jquery.vmap.usa.js",
                      "~/Areas/Administrator/TempLTE/plugins/jquery-knob/jquery.knob.min.js",
                      "~/Areas/Administrator/TempLTE/plugins/moment/moment.min.js",
                      "~/Areas/Administrator/TempLTE/plugins/daterangepicker/daterangepicker.js",
                      "~/Areas/Administrator/TempLTE/plugins/tempusdominus-bootstrap-4/js/tempusdominus-bootstrap-4.min.js",
                      "~/Areas/Administrator/TempLTE/plugins/summernote/summernote-bs4.min.js",
                      "~/Areas/Administrator/TempLTE/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js",
                      "~/Areas/Administrator/TempLTE/dist/js/adminlte.js",
                      "~/Areas/Administrator/TempLTE/dist/js/demo.js",
                      "~/Scripts/chosen.jquery.min.js",
                      "~/Areas/Administrator/TempLTE/dist/js/pages/dashboard.js",
                      "~/Scripts/toastr.min.js",
                      "~/Areas/Administrator/TempLTE/dist/js/script.js"));

            bundles.Add(new StyleBundle("~/admin/css").Include(
                      "~/Areas/Administrator/TempLTE/plugins/fontawesome-free/css/all.min.css",
                      "~/Areas/Administrator/TempLTE/plugins/tempusdominus-bootstrap-4/css/tempusdominus-bootstrap-4.min.css",
                      "~/Areas/Administrator/TempLTE/plugins/icheck-bootstrap/icheck-bootstrap.min.css",
                      "~/Areas/Administrator/TempLTE/plugins/jqvmap/jqvmap.min.css",
                      "~/Areas/Administrator/TempLTE/dist/css/adminlte.min.css",
                      "~/Areas/Administrator/TempLTE/plugins/overlayScrollbars/css/OverlayScrollbars.min.css",
                      "~/Areas/Administrator/TempLTE/plugins/daterangepicker/daterangepicker.css",
                      "~/Areas/Administrator/TempLTE/plugins/summernote/summernote-bs4.min.css"));

            bundles.Add(new StyleBundle("~/DataTable/css").Include(
                      "~/Areas/Administrator/TempLTE/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css",
                      "~/Areas/Administrator/TempLTE/plugins/datatables-responsive/css/responsive.bootstrap4.min.css",
                      "~/Areas/Administrator/TempLTE/plugins/datatables-buttons/css/buttons.bootstrap4.min.css"));
            bundles.Add(new ScriptBundle("~/DataTable/js").Include(
             "~/Areas/Administrator/TempLTE/plugins/datatables/jquery.dataTables.min.js",
             "~/Areas/Administrator/TempLTE/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js",
             "~/Areas/Administrator/TempLTE/plugins/datatables-responsive/js/dataTables.responsive.min.js",
             "~/Areas/Administrator/TempLTE/plugins/datatables-responsive/js/responsive.bootstrap4.min.js",
             "~/Areas/Administrator/TempLTE/plugins/datatables-buttons/js/dataTables.buttons.min.js",
             "~/Areas/Administrator/TempLTE/plugins/datatables-buttons/js/buttons.bootstrap4.min.js",
             "~/Areas/Administrator/TempLTE/plugins/jszip/jszip.min.js",
             "~/Areas/Administrator/TempLTE/plugins/pdfmake/pdfmake.min.js",
             "~/Areas/Administrator/TempLTE/plugins/pdfmake/vfs_fonts.js",
             "~/Areas/Administrator/TempLTE/plugins/datatables-buttons/js/buttons.html5.min.js",
             "~/Areas/Administrator/TempLTE/plugins/datatables-buttons/js/buttons.print.min.js",
             "~/Areas/Administrator/TempLTE/plugins/datatables-buttons/js/buttons.colVis.min.js",
             "~/Areas/Administrator/TempLTE/javascript/script.js"));

            bundles.Add(new StyleBundle("~/Frontend/css").Include(
                      "~/Content/css/shared.css",
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/jquery-ui.css",
                      "~/Content/css/css/all.css",
                      "~/fonts/slick/slick.css",
                      "~/fonts/slick/slick-theme.css",
                      "~/Content/css/bootstrap-datepicker3.css",
                      "~/Content/css/flaticon.css")
                      );
            bundles.Add(new ScriptBundle("~/Frontend/js").Include(
                      "~/Scripts/js/jquery-3.4.1.min.js",
                      "~/Scripts/js/bootstrap.min.js",
                      "~/Scripts/js/shared.js",
                      "~/fonts/slick/slick.min.js",
                      "~/Scripts/js/bootstrap-datepicker.min.js",
                      "~/Scripts/js/jquery.waypoints.min.js",
                      "~/Scripts/js/jquery.counterup.js"));
        }
    }
}
