using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGL.Core.Entities.Master;
using IGL.Core.Service.GenericService;
using IGL.Core.ViewModelEntities.MasterVm;
using Microsoft.AspNetCore.Mvc;

namespace IGL.UI.Controllers.Reports
{
    public class ProductRateDetailController : Controller
    {
        private readonly IGenericService<RateMaster, int> _IRateMasterService;
        private readonly IGenericService<MaterialMaster, int> _IProductService;

        public ProductRateDetailController(IGenericService<RateMaster, int> rateSevice, IGenericService<MaterialMaster, int> materialService)
        {
            _IRateMasterService = rateSevice;
            _IProductService = materialService;
        }
        public async Task<IActionResult> Index()
        {
            var models = (from RM in await _IRateMasterService.GetList(x => x.IsActive == 1)
                          join PM in await _IProductService.GetList(x => x.IsActive == 1)
                          on RM.ProductId equals PM.Id
                          select new RateMasterVm
                          {
                              Id= RM.Id,
                              ProductName= PM.Name,
                              Rate= RM.Rate,
                              FromDate= RM.FromDate,
                              ToDate=string.IsNullOrEmpty(RM.ToDate.ToString())? "Current Price": RM.ToDate.ToString()

                          }).ToList();
            return View("~/Views/Reports/ProductRateDetail.cshtml", models);
        }
    }
}