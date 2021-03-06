﻿using IGL.Core.Entities.Reports;
using IGL.Core.Repository.ReportsRepository;
using IGL.Core.Service.ReportsService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IGL.Infastructure.Service.ReportService
{
    public class ProductReportService : IProductReportService
    {
        private readonly IProductReportRepository _IProducReportRepository;

        public ProductReportService(IProductReportRepository productReportRepository)
        {
            _IProducReportRepository = productReportRepository;
        }
        public Task<List<ProductReport>> GetProductReport() =>  _IProducReportRepository.GetProductReport();

        public Task<List<ProductTransactionStatusReport>> GetProductTransactionStatusReport(int? EmployeeId) => _IProducReportRepository.GetProductTransactionStatusReport(EmployeeId);
        public Task<List<LowQuantityProductReport>> GetLowQuantityProductReport() => _IProducReportRepository.GetLowQuantityProductReport();

    }
}
