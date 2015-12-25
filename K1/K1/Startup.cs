using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(K1.Startup))]
namespace K1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
