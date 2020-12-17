using IGL.Core.Repository.GenericRepository;
using IGL.Core.Repository.IGLProductSIV;
using IGL.Core.Repository.MaterialDetail;
using IGL.Core.Repository.PurchaseOrder;
using IGL.Core.Repository.ReportsRepository;
using IGL.Infrastructure.Repository.GenericRepository;
using IGL.Infrastructure.Repository.IGLProductRepository;
using IGL.Infrastructure.Repository.MaterialDetail;
using IGL.Infrastructure.Repository.PurchaseOrder;
using IGL.Infrastructure.Repository.Reports;
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
            services.AddTransient<IProductReportRepository, ProductReportRepository>();
            services.AddTransient<IVendorWisePoStatusReportRepository, VendorWisePoStatusReportRepository>();
            services.AddTransient<IProductReturnRepository, ProductReturnRepository>();
            services.AddTransient<ISIVIGLProductRepository, SIVIGLProductRepository>();
            services.AddTransient<IPurchaseOrderRepository, PurchaseOrderRepository>();
        }
    }
}
