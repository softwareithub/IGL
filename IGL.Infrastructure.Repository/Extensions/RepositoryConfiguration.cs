using IGL.Core.Repository.GenericRepository;
using IGL.Core.Repository.MaterialDetail;
using IGL.Infrastructure.Repository.GenericRepository;
using IGL.Infrastructure.Repository.MaterialDetail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IGL.Infrastructure.Repository.Extensions
{
    public static class RepositoryConfiguration
    {
        public static void AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddTransient<IMaterialDetailRepo, MaterialDetailRepository>();
        }
    }
}
