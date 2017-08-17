using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArgenGrill.Startup))]

namespace ArgenGrill
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}