using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Focus8W.Startup))]
namespace Focus8W
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
