﻿using IGL.Core.Service.GenericService;
using IGL.Core.Service.MaterialDetail;
using IGL.Core.Service.ReportsService;
using IGL.Infastructure.Service.GenericService;
using IGL.Infastructure.Service.MaterialDetail;
using IGL.Infastructure.Service.ReportService;
using Microsoft.Extensions.DependencyInjection;

namespace IGL.Infastructure.Service.Extensions
{
    public static class ServiceConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            #region GenericService

            services.AddTransient(typeof(IGenericService<,>), typeof(GenericService<,>));
            services.AddTransient<IMaterialDetailService, MaterialDetailService>();
            services.AddTransient<IProductReportService, ProductReportService>();
            services.AddTransient<IVendorWisePOStatusReportService, VendorWisePoStatusReportService>();

            #endregion
        }
    }
}
