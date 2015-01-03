using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LV2015.Startup))]
namespace LV2015
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
