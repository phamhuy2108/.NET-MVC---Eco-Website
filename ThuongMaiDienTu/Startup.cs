using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ThuongMaiDienTu.Startup))]
namespace ThuongMaiDienTu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
