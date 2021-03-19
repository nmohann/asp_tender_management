using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TMSCodeFirst.Startup))]
namespace TMSCodeFirst
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
