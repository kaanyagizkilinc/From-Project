using Microsoft.Extensions.DependencyInjection;
using Modasyafasi.Webapp.Models.Interface;
using Modasyafasi.Webapp.Service;

namespace Modasyafasi.Webapp.Service.IoC
{
    public static class ServiceContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddHttpClient<IKullaniciService, KullaniciService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44397/");
            });
            services.AddScoped<IKullaniciService, KullaniciService>();

        }
    }
}
