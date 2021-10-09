using IGL.API.Helper;
using IGL.Core.Comman.Helper;
using IGL.Core.Entities.Master;
using IGL.Core.Entities.UserManagement;
using IGL.Core.Service.GenericService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGL.API.Controllers.UserManagement
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuSubMenuAPI : ControllerBase
    {
        private readonly IGenericService<MenuSubMenuModel, int> _IMenuSubMenuService;
        private readonly IGenericService<RoleAccess, int> _IRoleAccessService;
        private readonly IGenericService<RoleMaster, int> _IRoleMasterService;

        public MenuSubMenuAPI(IGenericService<MenuSubMenuModel, int> menuSubMenuService,
            IGenericService<RoleAccess, int> roleAccessService, IGenericService<RoleMaster, int> roleMasterService)
        {
            _IMenuSubMenuService = menuSubMenuService;
            _IRoleAccessService = roleAccessService;
            _IRoleMasterService = roleMasterService;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<IActionResult> CreateMenuSubMenu(MenuSubMenuModel requestModel)
        {
            var response = await _IMenuSubMenuService.CreateEntity(requestModel);
            if (response != Core.Comman.Comman.ResponseMessage.ServerError)
            {
                return new HttpResponseHelper<GenericResponseModel<string, int>, string>
                    (new GenericResponseModel<GenericResponseModel<string, int>, string>().
                    GetResponseModel("Created", null, null, "Created", System.Net.HttpStatusCode.OK));

            }

            return new HttpResponseHelper<GenericResponseModel<string, int>, string>
                  (new GenericResponseModel<GenericResponseModel<string, int>, string>().
                  GetResponseModel("internal server error", null, null, "internal server error",
                  System.Net.HttpStatusCode.InternalServerError));
        }


        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<IActionResult> GetSubMenuDetails()
        {
            var responseModel = await _IMenuSubMenuService.GetList(x => x.IsActive == 1 && x.IsDeleted == 0);
            if (responseModel.Any())
            {
                return new HttpResponseHelper<MenuSubMenuModel, string>
                  (new GenericResponseModel<MenuSubMenuModel, string>().
                  GetResponseModel("Success", null, responseModel.ToList(),
                  "Created", System.Net.HttpStatusCode.OK));
            }
            else
            {
                return new HttpResponseHelper<MenuSubMenuModel, string>
                      (new GenericResponseModel<MenuSubMenuModel, string>().
                      GetResponseModel("No record Found", null, responseModel.ToList(), "No Data Found",
                      System.Net.HttpStatusCode.NoContent));
            }
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetRoleMenuDetails(int roleId)
        {
            var responseModel = await _IMenuSubMenuService.GetList(x => x.IsActive == 1 && x.IsDeleted == 0);
            var mappedRoleMenu = await _IRoleAccessService.GetList(x => x.IsActive == 1 && x.IsDeleted == 0 && x.RoleId == roleId);

            responseModel.ToList().ForEach(data =>
            {
                mappedRoleMenu.ToList().ForEach(item =>
                {
                    if (data.Id == item.MenuId)
                    {
                        data.IsMapped = 1;
                    }

                });
            });

            return new HttpResponseHelper<MenuSubMenuModel, string>
                (new GenericResponseModel<MenuSubMenuModel, string>().
                GetResponseModel("Success", null, responseModel.ToList(),
                "Created", System.Net.HttpStatusCode.OK));
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateRoleAccess(List<RoleAccess> roleAccess)
        {
            var response = await _IRoleAccessService.Add(roleAccess.ToArray());

            if (response != Core.Comman.Comman.ResponseMessage.ServerError)
            {
                return new HttpResponseHelper<string, string>
               (new GenericResponseModel<string, string>().
               GetResponseModel("Success", null, null,
               "Created", System.Net.HttpStatusCode.OK));
            }

            return new HttpResponseHelper<string, string>
              (new GenericResponseModel<string, string>().
              GetResponseModel("internal server", null, null,
              "internal server", System.Net.HttpStatusCode.InternalServerError));

        }


        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetRoleDetails()
        {
            var roleDetails = await _IRoleMasterService.GetList(x => x.IsActive == 1 && x.IsDeleted == 0);

            return new HttpResponseHelper<RoleMaster, string>
              (new GenericResponseModel<RoleMaster, string>().
              GetResponseModel("Success", null, roleDetails.ToList(),
              "Success", System.Net.HttpStatusCode.OK));

        }
    }
}
