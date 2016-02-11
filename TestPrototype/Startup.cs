using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestPrototype.Startup))]
namespace TestPrototype
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
