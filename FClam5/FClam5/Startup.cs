using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FClam5.Startup))]
namespace FClam5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
