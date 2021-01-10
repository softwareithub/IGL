using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGL.Core.Comman.Comman;
using IGL.Core.Comman.Helper;
using IGL.Core.Entities.Inventory;
using IGL.Core.Entities.Master;
using IGL.Core.Entities.SIV;
using IGL.Core.Entities.StoreMaster;
using IGL.Core.Service.GenericService;
using IGL.Core.ViewModelEntities.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace IGL.UI.Controllers.SIV
{
    public class SIVMasterController : Controller
    {
        private readonly IGenericService<StoreDetail, int> _IStoreService;
        private readonly IGenericService<PurchaseOrder, int> _IPoDetailService;
        private readonly IGenericService<POItem, int> _IPOItemService;
        private readonly IGenericService<SIVDetail, int> _ISIVDetailService;
        private readonly IGenericService<SIVMaterialTransaction, int> _ISIVTransactionService;
        private readonly IGenericService<VendorMaster, int> _IVendorService;
        private readonly IGenericService<MaterialMaster, int> _IProductService;
        private readonly IGenericService<UnitMaster, int> _IunitMasterService;
        private readonly IGenericService<RateMaster, int> _IRateMasterService;

        public SIVMasterController(IGenericService<StoreDetail, int> storeService, IGenericService<PurchaseOrder, int> poDetailService, IGenericService<POItem, int> poItemService, IGenericService<SIVDetail, int> sivDetailService, IGenericService<SIVMaterialTransaction, int> sivTransactionService, IGenericService<VendorMaster, int> vendorService, IGenericService<MaterialMaster, int> productService, IGenericService<UnitMaster, int> unitService, IGenericService<RateMaster, int> rateMasterService)
        {
            _IStoreService = storeService;
            _IPOItemService = poItemService;
            _IPoDetailService = poDetailService;
            _ISIVDetailService = sivDetailService;
            _ISIVTransactionService = sivTransactionService;
            _IVendorService = vendorService;
            _IProductService = productService;
            _IunitMasterService = unitService;
            _IRateMasterService = rateMasterService;
        }
        public IActionResult Index(int poId)
        {
            TempData["poId"] = poId;
            return View("~/Views/SIV/SIVIndex.cshtml");
        }

        public async Task<IActionResult> CreateSIV(int sivStatus)
        {
            int poId = Convert.ToInt32(TempData["poId"]);
            ViewBag.StoreList = await _IStoreService.GetList(x => x.IsActive == 1);
            /*SIV status==0 for created and sivStatus ==1 for Approve*/
            if (sivStatus == 0)
            {
                if (poId != 0)
                {
                    ViewBag.POList = (await _IPoDetailService.GetList(x => x.IsActive == 1 && x.POStatus == "POApproved")).Where(x => x.Id == poId).ToList();
                }
                else
                {
                    ViewBag.POList = (await _IPoDetailService.GetList(x => x.IsActive == 1 && x.POStatus == "POApproved"));
                }
            }
            else
            {
                if (poId != 0)
                {
                    ViewBag.POList = (await _IPoDetailService.GetList(x => x.IsActive == 1 && x.POStatus == "SIVCreated")).Where(x => x.Id == poId).ToList();
                }
                else
                {
                    ViewBag.POList = (await _IPoDetailService.GetList(x => x.IsActive == 1 && x.POStatus == "SIVCreated"));
                }

            }


            return PartialView("~/Views/SIV/_SIVCreatePartial.cshtml");
        }

        public async Task<IActionResult> GetPODetail(int id)
        {
            var sivDetails = await _ISIVDetailService.GetList(x => x.PoId == id && x.IsActive == 1);
            var poDetail = await _IPoDetailService.GetList(x => x.IsActive == 1);
            var vendorDetail = await _IVendorService.GetList(x => x.IsActive == 1);
            if (sivDetails.Count() == 0)
            {
                var model = from PO in poDetail
                            join VM in vendorDetail
                            on PO.VendorId equals VM.Id
                            where PO.Id == id
                            select new
                            {
                                poDate = PO.PODate.ToShortDateString(),
                                vendorName = VM.VendorName,
                                isApprove = false,
                                poApproved = PO.POStatus
                            };
                return Json(model.First());
            }
            else
            {
                var model = from SV in sivDetails
                            join PO in poDetail
                            on SV.PoId equals PO.Id
                            join VM in vendorDetail
                            on PO.VendorId equals VM.Id
                            where PO.Id == id
                            select new
                            {
                                poDate = PO.PODate.ToShortDateString(),
                                vendorName = VM.VendorName,
                                invoiceNumber = SV.InvoiceNumber,
                                invoiceDate = SV.InvoiceDate,
                                invoicePatg = SV.InvoicePath,
                                isApprove = true,
                                poApproved = PO.POStatus
                            };
                return Json(model.First());

            }

        }

        public async Task<IActionResult> GetPOItems(int id)
        {
            var sivDetails = await _ISIVDetailService.GetSingle(x => x.PoId == id);
            if (sivDetails == null)
            {
                List<PurchaseOrderDetail> models = await GetPOItemDetailFromPurchaseOrder(id);
                return PartialView("~/Views/SIV/_SIVPoItemDetailPartial.cshtml", models);
            }
            else
            {
                List<PurchaseOrderDetail> models = await GetPOItemDetailFromSIV(id);
                return PartialView("~/Views/SIV/_SIVPoItemDetailPartial.cshtml", models);
            }

        }

        public async Task<IActionResult> CreateSIVDetail(SIVDetail model, string[] matId, string[] unitPrice, string[] qty, string[] Remarks, string Save)
        {
            if (Save.ToLower().Trim() == "save")
            {
                var createHelper = CommanCRUDHelper.CommanCreateCode(model, 1);
                var createResponse = await _ISIVDetailService.CreateEntity(createHelper);
                var purchasedetail = await _IPoDetailService.GetSingle(x => x.Id == model.PoId);
                purchasedetail.POStatus = "SIVCreated";
                var updatePo = await _IPoDetailService.Update(purchasedetail);

                if (createResponse == ResponseMessage.AddedSuccessfully)
                {
                    var SIVDetails = await _ISIVDetailService.GetList(x => x.IsActive == 1);

                    List<SIVMaterialTransaction> models = new List<SIVMaterialTransaction>();
                    for (int i = 0; i < matId.Count(); i++)
                    {
                        SIVMaterialTransaction itemData = new SIVMaterialTransaction();
                        itemData.ItemId = Convert.ToInt32(matId[i]);
                        itemData.UnitPrice = Convert.ToDecimal(unitPrice[i]);
                        itemData.Quantity = Convert.ToInt32(qty[i]);
                        itemData.ItemNumber = string.Empty;
                        itemData.SIVId = SIVDetails.Max(x => x.Id);
                        models.Add(itemData);
                    }

                    var createSIVItemResponse = await _ISIVTransactionService.Add(models.ToArray());
                    return Json(ResponseHelper.GetResponseMessage(createSIVItemResponse));
                }
            }
            else
            {
                var purchasedetail = await _IPoDetailService.GetSingle(x => x.Id == model.PoId);
                purchasedetail.POStatus = "SIVApproved";
                var updatePo = await _IPoDetailService.Update(purchasedetail);

                var sivDetail = await _ISIVDetailService.GetSingle(x => x.PoId == model.PoId && x.IsActive == 1);
                sivDetail.InvoiceDate = model.InvoiceDate;
                sivDetail.InvoiceNumber = model.InvoiceNumber;
                sivDetail.InvoicePath = model.InvoicePath;
                sivDetail.StoreId = model.StoreId;
                var updateSivModelHelper = CommanCRUDHelper.CommanUpdateCode(sivDetail, 1);
                var updateSIVResponse = await _ISIVDetailService.Update(updateSivModelHelper);
                if (updateSIVResponse == ResponseMessage.UpdatedSuccessfully)
                {
                    var sivItemDetails = await _ISIVTransactionService.GetList(x => x.SIVId == sivDetail.Id && x.IsActive == 1);

                    sivItemDetails.ToList().ForEach(item =>
                    {
                        item.IsActive = 0;
                        item.IsDeleted = 1;
                        item.UpdatedDate = DateTime.Now.Date;
                        item.UpdatedBy = 1;
                    });

                    var deleteResponse = await _ISIVTransactionService.Update(sivItemDetails.ToArray());
                    if (deleteResponse == ResponseMessage.UpdatedSuccessfully)
                    {
                        List<SIVMaterialTransaction> models = new List<SIVMaterialTransaction>();
                        for (int i = 0; i < matId.Count(); i++)
                        {
                            SIVMaterialTransaction itemData = new SIVMaterialTransaction();
                            itemData.ItemId = Convert.ToInt32(matId[i]);
                            itemData.UnitPrice = Convert.ToDecimal(unitPrice[i]);
                            itemData.Quantity = Convert.ToInt32(qty[i]);
                            itemData.ItemNumber = string.Empty;
                            itemData.SIVId = sivDetail.Id;
                            models.Add(itemData);
                        }

                        var createSIVItemResponse = await _ISIVTransactionService.Add(models.ToArray());
                        var responseUpdate = await UpdateProductOnSIVApprove(matId, qty, unitPrice);
                        var updateRateMaster = await UpdateRateMaster(matId, unitPrice);
                        return Json(ResponseHelper.GetResponseMessage(createSIVItemResponse));
                    }
                }


            }
            return Json("There is somethng wents wrong. Please contact Admin Team.");
        }

        private async Task<List<PurchaseOrderDetail>> GetPOItemDetailFromPurchaseOrder(int id)
        {
            var poDetail = await _IPoDetailService.GetList(x => x.IsActive == 1);
            var poItems = await _IPOItemService.GetList(x => x.IsActive == 1);
            var vendorDetails = await _IVendorService.GetList(x => x.IsActive == 1);
            var productDetails = await _IProductService.GetList(x => x.IsActive == 1);
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
                          where (PO.Id == id)
                          select new PurchaseOrderDetail
                          {
                              ItemId = PI.ItemId,
                              ItemName = PRD.Name,
                              ItemCode = PRD.Code,
                              HSNCode = PRD.HSNCode,
                              UnitName = UM.Name,
                              Quantity = PI.Quantity,
                              UnitPrice = PRD.PerUnitCost,
                              Amount = PI.Quantity * PRD.PerUnitCost,
                              Remarks = PO.Remarks,
                          }).ToList();
            return models;
        }


        private async Task<List<PurchaseOrderDetail>> GetPOItemDetailFromSIV(int id)
        {
            var sivDetails = await _ISIVDetailService.GetList(x => x.IsActive == 1 && x.PoId == id);
            var sivItems = await _ISIVTransactionService.GetList(x => x.IsActive == 1);
            var productDetails = await _IProductService.GetList(x => x.IsActive == 1);
            var unitDetail = await _IunitMasterService.GetList(x => x.IsActive == 1);
            var poDetail = await _IPoDetailService.GetList(x => x.IsActive == 1);


            var models = (from SV in sivDetails
                          join SI in sivItems
                          on SV.Id equals SI.SIVId
                          join PRD in productDetails
                          on SI.ItemId equals PRD.Id
                          join UM in unitDetail
                          on PRD.UnitId equals UM.Id
                          join PO in poDetail
                          on SV.PoId equals PO.Id
                          where (SV.PoId == id)
                          select new PurchaseOrderDetail
                          {
                              ItemId = SI.ItemId,
                              ItemName = PRD.Name,
                              ItemCode = PRD.Code,
                              HSNCode = PRD.HSNCode,
                              UnitName = UM.Name,
                              Quantity = SI.Quantity,
                              UnitPrice = SI.UnitPrice,
                              Amount = SI.Quantity * SI.UnitPrice,
                              InvoiceDate = Convert.ToDateTime(SV.InvoiceDate),
                              InvoiceNumber = SV.InvoiceNumber,
                              InvoicePath = SV.InvoicePath,
                              POStatus = PO.POStatus
                          }).ToList();
            return models;
        }

        private async Task<bool> UpdateProductOnSIVApprove(string[] matId, string[] qty, string[] price)
        {
            try
            {

                var productDetails = await _IProductService.GetList(x => x.IsActive == 1);
                List<MaterialMaster> productList = new List<MaterialMaster>();
                for(int i=0; i< matId.Count(); i++)
                {
                    int productId = Convert.ToInt32(matId[i]);
                    var prdDetail = productDetails.ToList().Where(z => z.Id == productId).FirstOrDefault();
                    prdDetail.OpeningQuantity += Convert.ToInt32(qty[i]);
                    prdDetail.PerUnitCost = Convert.ToDecimal(price);
                    productList.Add(prdDetail);
                }

                var response = await _IProductService.Update(productList.ToArray());
                return response == ResponseMessage.UpdatedSuccessfully ? true : false;
            }
            catch (Exception ex) 
            {
                return false;
            }

        }

        private async Task<bool> UpdateRateMaster(string[] prodId, string[] price)
        {
            ResponseMessage createResponse = ResponseMessage.AddedSuccessfully;
            for (int i = 0; i < prodId.Count(); i++)
            {
                //Check product exists in RateMaster
                var productId = Convert.ToInt32(prodId[i]);
                var rateModel = (await _IRateMasterService.GetList(x => x.ProductId == productId && x.ToDate == null)).FirstOrDefault();

                if (rateModel != null)
                {
                    rateModel.ToDate = DateTime.Now.Date;
                    var updateResponse = await _IRateMasterService.Update(rateModel);
                }
                RateMaster model = new RateMaster();
                model.ProductId = Convert.ToInt32(prodId[i]);
                model.Rate = Convert.ToDecimal(price[i]);
                model.FromDate = DateTime.Now.Date;
                model.ToDate = null;
                createResponse = await _IRateMasterService.CreateEntity(model);
            }

            return createResponse == Core.Comman.Comman.ResponseMessage.AddedSuccessfully ? true : false;
        }
    }
}