using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(FileTransferService.Startup))]

namespace FileTransferService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
                ConfigureMobileApp(app);
        }
    }
}