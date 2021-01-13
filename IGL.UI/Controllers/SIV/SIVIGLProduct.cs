using IGL.Core.Entities.Master;
using IGL.Core.Entities.SIV;
using IGL.Core.Entities.StoreMaster;
using IGL.Core.Service.GenericService;
using IGL.Core.Service.IGLProductSIV;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IGL.UI.Controllers.SIV
{
    public class SIVIGLProduct : Controller
    {
        private readonly IIGLProductService iIGLProductService;
        private readonly IGenericService<StoreDetail, int> _IStoreService;
        private readonly IGenericService<MaterialMaster, int> _IProductService;
        public SIVIGLProduct(IIGLProductService iGLProductService,
            IGenericService<StoreDetail, int> storeDetailService, 
            IGenericService<MaterialMaster, int> productService)
        {
            iIGLProductService = iGLProductService;
            _IStoreService = storeDetailService;
            _IProductService = productService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.StoreList =await  _IStoreService.GetList(x => x.IsActive == 1);
            ViewBag.ProductList =await _IProductService.GetList(x => x.IsPayable == 0  && x.IsActive == 1);
            return View("~/Views/SIV/SIVIGLProduct.cshtml");
        }
        public async Task<IActionResult> InsertIGLProduct(IGLProduct modelEntity)
        {
            modelEntity.Quantity = 1;
            var response =await iIGLProductService.InsertIGLProduct(modelEntity);
            return Json(response.responseMessage);
        }
        public async Task<IActionResult> GetApprovedSIVDetails()
        {
            var response = await iIGLProductService.GetSIVApprovedDetail();
            return PartialView("~/Views/SIV/SIVDetailListPartial.cshtml", response);
        }
        public async Task<IActionResult> GetApprovedSIVCount()
        {
            var response = await iIGLProductService.ApprovedSIVCount();
            return Json(response);
        }
    }
}
