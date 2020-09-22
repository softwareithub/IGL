using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGL.Core.Comman.Helper;
using IGL.Core.Service.GenericService;
using Microsoft.AspNetCore.Mvc;

namespace IGL.UI.Controllers.Organisation
{
    public class OrganisationController : Controller
    {
        private readonly IGenericService<IGL.Core.Entities.Organization.Organisation, int> _IOrganisationService;

        public OrganisationController(IGenericService<IGL.Core.Entities.Organization.Organisation, int>  organisationService)
        {
            _IOrganisationService = organisationService;
        }
        public IActionResult Index()
        {
            return View("~/Views/Organisation/OrganisationIndex.cshtml");
        }

        public async Task<IActionResult> GetDetails()
        {
            var models = await _IOrganisationService.GetList(x => x.IsActive == 1);
            return PartialView("~/Views/Organisation/_OrganisationListPartial.cshtml", models);
        }

        public async Task<IActionResult> CreateDetail(int id)
        {
            var model = await _IOrganisationService.GetSingle(x => x.Id == id);
            return PartialView("~/Views/Organisation/_OrganisationCreatePartial.cshtml", model);
        }

        public async Task<IActionResult> UpsertDetail(IGL.Core.Entities.Organization.Organisation modelEntity)
        {
            if(modelEntity.Id==0)
            {
                var createModel = CommanCRUDHelper.CommanCreateCode(modelEntity, 1);
                var response = await _IOrganisationService.CreateEntity(createModel);
                return Json(ResponseHelper.GetResponseMessage(response));
            }
            var updateModel = CommanCRUDHelper.CommanUpdateCode(modelEntity, 1);
            var updateResponse = await _IOrganisationService.Update(updateModel);
            return Json(ResponseHelper.GetResponseMessage(updateResponse));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _IOrganisationService.GetSingle(x => x.Id == id);
            var deleteModel = CommanCRUDHelper.CommanDeleteCode(model,1);
            var response = await _IOrganisationService.Delete(deleteModel);
            return Json(ResponseHelper.GetResponseMessage(response));

        }


    }
}