using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FerrumChromeDev.Startup))]
namespace FerrumChromeDev
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
