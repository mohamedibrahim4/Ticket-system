using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Platform_Task__Management_System.Startup))]
namespace Platform_Task__Management_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
