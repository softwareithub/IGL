using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGL.Core.Comman.Helper;
using IGL.Core.Entities.Master;
using IGL.Core.Service.GenericService;
using IGL.Core.ViewModelEntities.MasterVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGL.UI.Controllers.Master
{
    [Authorize]
    public class EmployeeDetailController : Controller
    {
        private readonly IGenericService<EmployeeDetail, int> _IEmployeeDetailService;
        private readonly IGenericService<EmployeeType, int> _IEmployeeTypeService;

        public EmployeeDetailController(IGenericService<EmployeeDetail, int> employeeDetailService, IGenericService<EmployeeType, int> employeeTypeService)
        {
            _IEmployeeDetailService = employeeDetailService;
            _IEmployeeTypeService = employeeTypeService;
        }
        public async Task<IActionResult> Index()
        {
            return await Task.Run(()=> View("~/Views/Master/Employee/EmployeeIndex.cshtml"));
        }

        public async Task<IActionResult> GetEmployeeDetails()
        {
            var employeeTypeDetails = await _IEmployeeTypeService.GetList(x => x.IsActive == 1);
            var employeeDetails = await _IEmployeeDetailService.GetList(x => x.IsActive == 1);

            var models = (from ED in employeeDetails
                          join ET in employeeTypeDetails
                          on ED.EmployeeTypeId equals ET.Id
                          select new EmployeeDetailVm
                          {
                              Id = ED.Id,
                              Name = ED.Name,
                              EmployeeType = ET.Name,
                              EmailId = ED.EmailId,
                              Phone = ED.Phone,
                              Address = ED.PanNo

                          }).ToList();
            return PartialView("~/Views/Master/Employee/_EmployeeListPartial.cshtml", models);
        }

        public async Task<IActionResult> UpsertEmployeeDetail(int id)
        {
            ViewBag.EmployeeType = await _IEmployeeTypeService.GetList(x => x.IsActive == 1);
            var model = await _IEmployeeDetailService.GetSingle(x => x.Id == id);
            return PartialView("~/Views/Master/Employee/_CreateEmployeePartial.cshtml", model);
        }


        [HttpPost]
        public async Task<IActionResult> UpSertEmployeeDetail(EmployeeDetail model)
        {
            if (model.Id > 0)
            {
                var updateModel = CommanCRUDHelper.CommanUpdateCode(model, 1);
                var updateResponse = await _IEmployeeDetailService.Update(updateModel);
                return Json(ResponseHelper.GetResponseMessage(updateResponse));
            }
            var createModel = CommanCRUDHelper.CommanCreateCode(model, 1);
            var createResponse = await _IEmployeeDetailService.CreateEntity(createModel);
            return Json(ResponseHelper.GetResponseMessage(createResponse));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _IEmployeeDetailService.GetSingle(x => x.Id == id);
            var deleteModel = CommanCRUDHelper.CommanDeleteCode(model, 1);
            await _IEmployeeDetailService.CreateNewContext();
            var response = await _IEmployeeDetailService.Update(deleteModel);
            return Json(ResponseHelper.GetResponseMessage(response));
        }

    }
}