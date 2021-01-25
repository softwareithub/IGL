using IGL.Core.Entities.Master;
using IGL.Core.Entities.SIV;
using IGL.Core.Entities.StoreMaster;
using IGL.Core.Service.GenericService;
using IGL.Core.Service.IGLProductSIV;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            ViewBag.StoreList = await _IStoreService.GetList(x => x.IsActive == 1);
            ViewBag.ProductList = await _IProductService.GetList(x => x.IsPayable == 0 && x.IsActive == 1);
            return View("~/Views/SIV/SIVIGLProduct.cshtml");
        }
        public async Task<IActionResult> InsertIGLProduct(IGLProduct modelEntity, string[] ItemNumber)
        {
            modelEntity.Quantity = 1;
            var models = new List<IGLProduct>();
            foreach(var item in ItemNumber)
            {
                var model = new IGLProduct();
                model.ItemNumber = item;
                model.Quantity = 1;
                model.StoreId = modelEntity.StoreId;
                model.MaterialId = modelEntity.MaterialId;
                model.PoNumber = modelEntity.PoNumber;

                models.Add(model);
            }
            var response = await iIGLProductService.InsertIGLProduct(models);
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
        public async Task<IActionResult> IsItemExists(string itemNumber)
        {
            var response = await iIGLProductService.IsExistsItemNumber(itemNumber);
            return Json(response);
        }

        public async Task<IActionResult> GetPoWiseIGLProduct()
        {
            var response = await iIGLProductService.PoWiseIGLProducts();
            return PartialView("~/Views/SIV/PoWiseIGLProduct.cshtml", response);
        }
    }
}
