using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Version3withdropdown.Startup))]
namespace Version3withdropdown
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
