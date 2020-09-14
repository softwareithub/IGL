using IGL.Core.Repository.GenericRepository;
using IGL.Core.Service.GenericService;
using IGL.Infastructure.Service.GenericService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IGL.Infastructure.Service.Extensions
{
    public static class ServiceConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            #region GenericService

            services.AddTransient(typeof(IGenericService<,>), typeof(GenericService<,>));

            #endregion
        }
    }
}
