using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGL.Core.Comman.Helper;
using IGL.Core.Entities.Inventory;
using IGL.Core.Entities.Master;
using IGL.Core.Service.GenericService;
using IGL.Core.ViewModelEntities.Inventory;
using IGL.Core.ViewModelEntities.MasterVm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IGL.UI.Controllers.Inventory
{
    public class PurchaseOrderController : Controller
    {
        private readonly IGenericService<PurchaseOrder, int> _IPurchaseOrderService;
        private readonly IGenericService<POItem, int> _IPOItemService;
        private readonly IGenericService<VendorMaster, int> _IVendorService;
        private readonly IGenericService<MaterialMaster, int> _IProductService;
        private readonly IGenericService<UnitMaster, int> _IunitMasterService;
        private readonly IGenericService<IGL.Core.Entities.Organization.Organisation, int> _IOrganisationService;

        public PurchaseOrderController(IGenericService<PurchaseOrder, int> purchaseOrderService, IGenericService<POItem, int> poItemService, IGenericService<VendorMaster, int> vendorService, IGenericService<MaterialMaster, int> productService, IGenericService<UnitMaster, int> unitMasterService, IGenericService<IGL.Core.Entities.Organization.Organisation, int> organisationService)
        {
            _IPOItemService = poItemService;
            _IPurchaseOrderService = purchaseOrderService;
            _IVendorService = vendorService;
            _IProductService = productService;
            _IunitMasterService = unitMasterService;
            _IOrganisationService = organisationService;
        }
        public IActionResult Index()
        {
            return View("~/Views/PurchaseOrder/POIndex.cshtml");
        }

        public async Task<IActionResult> CreatePO()
        {
            string poNumber = "PO00" + Convert.ToString(1);
            var poDetails = await _IPurchaseOrderService.GetList(x => x.IsActive == 1);
            if(poDetails.Count()>0)
            {
                poNumber = poNumber + (poDetails.Max(x => x.Id)+1).ToString();
            }
            ViewData["poNumber"] = poNumber;
            var vendorDetails = await _IVendorService.GetList(x => x.IsActive == 1);
            ViewBag.VendorList = vendorDetails;
            return await Task.Run(() => PartialView("~/Views/PurchaseOrder/_CreatePurchaseOrderPartial.cshtml"));
        }

        public async Task<IActionResult> GetPOList()
        {
            var poDetail = await _IPurchaseOrderService.GetList(x => x.IsActive == 1);
            var poItems = await _IPOItemService.GetList(x => x.IsActive == 1);
            var vendorDetails = await _IVendorService.GetList(x => x.IsActive == 1);
            var productDetails = await _IProductService.GetList(x => x.IsActive == 1);


            var models = (from PO in poDetail
                          join VM in vendorDetails
                          on PO.VendorId equals VM.Id
                          join PI in poItems
                          on PO.Id equals PI.PoId
                          join PRD in productDetails
                          on PI.ItemId equals PRD.Id
                          select new PurchaseOrderDetail
                          {
                              PoId = PO.Id,
                              PoNUmber = PO.PONumber,
                              PoDate = PO.PODate,
                              VendorName = VM.VendorName,
                              ItemId = PI.ItemId,
                              ItemName = PRD.Name,
                              Quantity = PI.Quantity,
                              UnitPrice = PRD.PerUnitCost,
                              Amount = PI.Quantity * PRD.PerUnitCost,
                              Remarks = PO.Remarks,
                              POStatus = PO.POStatus
                          }).ToList();

            return PartialView("~/Views/PurchaseOrder/_PurchaseOrderDetails.cshtml", models);

        }

        public async Task<IActionResult> MaterialForPo()
        {
            var unitModels = await _IunitMasterService.GetList(x => x.IsActive == 1);
            var materialModels = await _IProductService.GetList(x => x.IsActive == 1);


            var models = (from mt in materialModels
                          join UM in unitModels
                          on mt.UnitId equals UM.Id
                          select new MaterialMasterVm
                          {
                              Id = mt.Id,
                              MaterialName = mt.Name,
                              Code = mt.Code,
                              Unit = UM.Name,
                              PerUnitCost = mt.PerUnitCost,
                              IsPayable = mt.IsPayable == 1 ? "Payable" : "Free (IGL Product)"
                          }).ToList();
            return PartialView("~/Views/PurchaseOrder/_AddMaterialToPOPartial.cshtml", models);
        }

        public async Task<IActionResult> CreatePurchaseOrder(PurchaseOrder model, string[] matId, string[] remarks, string[] qty, string[] unitPrice)
        {
            var createHelper = CommanCRUDHelper.CommanCreateCode(model, 1);
            createHelper.POStatus = "Created";
            var response = await _IPurchaseOrderService.CreateEntity(model);
            var poId = (await _IPurchaseOrderService.GetList(x => x.IsActive == 1)).Max(x => x.Id);

            List<POItem> poItems = new List<POItem>();

            for (int i = 0; i < matId.Count(); i++)
            {
                POItem item = new POItem();
                item.PoId = poId;
                item.ItemId = Convert.ToInt32(matId[i]);
                item.Quantity = Convert.ToInt32(qty[i]);
                item.Amount = Convert.ToDecimal(Convert.ToInt32(qty[i]) * Convert.ToDecimal(unitPrice[i]));
                item.Remarks = remarks[i];
                item.IsActive = 1;
                item.IsDeleted = 0;
                item.CreatedBy = 1;
                item.CreatedDate = DateTime.Now.Date;
                poItems.Add(item);
            }

            var itemResponse = await _IPOItemService.Add(poItems.ToArray());
            return Json(ResponseHelper.GetResponseMessage(itemResponse));
        }

        public async Task<IActionResult> POPDF(int id)
        {
            List<PurchaseOrderDetail> models = await GetPoDetail(id);
            return PartialView("~/Views/PurchaseOrder/_POPDFPartial.cshtml", models);
        }

        public async Task<IActionResult> ApprovePo(int poId)
        {
            var poDetail = await _IPurchaseOrderService.GetList(x => x.IsActive == 1);
            var poItems = await _IPOItemService.GetList(x => x.IsActive == 1);
            var vendorDetails = await _IVendorService.GetList(x => x.IsActive == 1);
            var productDetails = await _IProductService.GetList(x => x.IsActive == 1);
            var organisationModel = (await _IOrganisationService.GetList(x => x.IsActive == 1)).First();
            var unitDetail = await _IunitMasterService.GetList(x => x.IsActive == 1);

            var models = (from PO in poDetail
                          join VM in vendorDetails
                          on PO.VendorId equals VM.Id
                          join PI in poItems
                          on PO.Id equals PI.PoId
                          join PRD in productDetails
                          on PI.ItemId equals PRD.Id
                          join UM in unitDetail
                          on PRD.UnitId equals UM.Id
                          where(PO.Id==poId)
                          select new PurchaseOrderDetail
                          {
                              PoId = PO.Id,
                              PoNUmber = PO.PONumber,
                              PoDate = PO.PODate,
                              VendorName = VM.VendorName,
                              ItemId = PI.ItemId,
                              ItemName = PRD.Name,
                              ItemCode = PRD.Code,
                              HSNCode = PRD.HSNCode,
                              UnitName = UM.Name,
                              Quantity = PI.Quantity,
                              UnitPrice = PRD.PerUnitCost,
                              Amount = PI.Quantity * PRD.PerUnitCost,
                              Remarks = PO.Remarks,
                              POStatus = PO.POStatus,
                              VendorAddress = VM.AddressLine1 + "/" + VM.AddressLine2,
                              VendorGST = VM.GSTNumber,
                              VendorEmail = VM.VendorEmail,
                              VendorPhone = VM.VendorPhone
                          }).ToList();
            return PartialView("~/Views/PurchaseOrder/_ApprovePoDetail.cshtml", models);
        }

        #region PrivateRegion
        private async Task<List<PurchaseOrderDetail>> GetPoDetail(int id)
        {
            var poDetail = await _IPurchaseOrderService.GetList(x => x.IsActive == 1);
            var poItems = await _IPOItemService.GetList(x => x.IsActive == 1);
            var vendorDetails = await _IVendorService.GetList(x => x.IsActive == 1);
            var productDetails = await _IProductService.GetList(x => x.IsActive == 1);
            var organisationModel = (await _IOrganisationService.GetList(x => x.IsActive == 1)).First();
            var unitDetail = await _IunitMasterService.GetList(x => x.IsActive == 1);

            var models = (from PO in poDetail
                          join VM in vendorDetails
                          on PO.VendorId equals VM.Id
                          join PI in poItems
                          on PO.Id equals PI.PoId
                          join PRD in productDetails
                          on PI.ItemId equals PRD.Id
                          join UM in unitDetail
                          on PRD.UnitId equals UM.Id
                          where PO.Id== id
                          select new PurchaseOrderDetail
                          {
                              PoId = PO.Id,
                              PoNUmber = PO.PONumber,
                              PoDate = PO.PODate,
                              VendorName = VM.VendorName,
                              ItemId = PI.ItemId,
                              ItemName = PRD.Name,
                              ItemCode = PRD.Code,
                              HSNCode = PRD.HSNCode,
                              UnitName = UM.Name,
                              Quantity = PI.Quantity,
                              UnitPrice = PRD.PerUnitCost,
                              Amount = PI.Quantity * PRD.PerUnitCost,
                              Remarks = PO.Remarks,
                              POStatus = PO.POStatus,
                              VendorAddress = VM.AddressLine1 + "/" + VM.AddressLine2,
                              VendorGST = VM.GSTNumber,
                              VendorEmail = VM.VendorEmail,
                              VendorPhone = VM.VendorPhone
                          }).ToList();
            if (models.Count() > 0)
            {
                models.First().Organisation = organisationModel;
            }

            return models;
        }

        #endregion
    }
}