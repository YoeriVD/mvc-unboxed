using System.Web;
using System.Web.Optimization;

namespace unboxed.web
{
    public static partial class Bundles
    {
        public static partial class Scripts
        {
            public static readonly string jquery = "~/scripts/jquery";
            public static readonly string jqueryvalidate = "~/scripts/jqueryvalidate";
            public static readonly string bootstrap = "~/scripts/bootstrap";
        }
        public static partial class Styles
        {
            public static readonly string bootstrap = "~/styles/bootstrap";
        }
    }

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle(Bundles.Scripts.jquery).Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle(Bundles.Scripts.jqueryvalidate).Include(
                "~/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle(Bundles.Scripts.bootstrap).Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle(Bundles.Styles.bootstrap).Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"));
        }
    }
}