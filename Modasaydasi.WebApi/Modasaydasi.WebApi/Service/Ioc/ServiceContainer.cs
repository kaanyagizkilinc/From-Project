using Microsoft.Extensions.DependencyInjection;
using Modasaydasi.WebApi.Model;
using Modasaydasi.WebApi.Model.Interface;
using Modasaydasi.WebApi.Service; // Eğer KullaniciKayıtService burada ise
// veya KullaniciKayitManager kullanacaksanız aşağıdaki satırı ekleyin:
// using Modasaydasi.WebApi.Model;

namespace Modasaydasi.WebApi.Service.Ioc
{
    public static class ServiceContainer
    {
        public static void AddScopedService(this IServiceCollection services)
        {
            services.AddScoped<IKullaniciKayit, KullaniciKayitManager>();
        }
    }
}
