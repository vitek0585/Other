using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Ninject;
using Owin;
using TimePicker.Identity;

[assembly: OwinStartup(typeof(Startup))]
namespace TimePicker.Identity
{
    public partial class Startup
    {
        public static IDataProtectionProvider DataProtectionProvider { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            DataProtectionProvider = app.GetDataProtectionProvider();
            ConfigureAuth(app);
        }
    }
}
