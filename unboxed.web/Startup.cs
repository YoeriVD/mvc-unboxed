using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(unboxed.web.Startup))]
namespace unboxed.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
