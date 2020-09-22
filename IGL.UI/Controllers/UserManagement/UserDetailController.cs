using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGL.Core.Comman.Helper;
using IGL.Core.Entities.Master;
using IGL.Core.Service.GenericService;
using IGL.Core.ViewModelEntities.MasterVm.UserManagementVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGL.UI.Controllers.UserManagement
{
    [Authorize]
    public class UserDetailController : Controller
    {
        private readonly IGenericService<RoleMaster, int> _IRoleMasterService;
        private readonly IGenericService<UserDetail, int> _IUserDetailService;

        public UserDetailController(IGenericService<RoleMaster, int> _roleMasterRepo, IGenericService<UserDetail, int> _userDetailRepo)
        {
            _IRoleMasterService = _roleMasterRepo;
            _IUserDetailService = _userDetailRepo;
        }

        public async Task<IActionResult> Index()
        {
            return  await Task.Run(()=> View("~/Views/UserDetail/UserDetailIndex.cshtml"));
        }

        public async Task<IActionResult> GetUserDetails()
        {
            var userDetailModels = await _IUserDetailService.GetList(x => x.IsActive == 1);
            var roleDetailModels = await _IRoleMasterService.GetList(x => x.IsActive == 1);
            var models = (from UD in userDetailModels
                          join RM in roleDetailModels
                          on UD.RoleId equals RM.Id
                          select new UserDetailVm
                          {
                              Id = UD.Id,
                              UserName = UD.UserName,
                              Name = UD.Name,
                              EmailId = UD.EmailId,
                              Phone = UD.Phone,
                              Address = UD.Address,
                              RoleName = RM.RoleName
                          }).ToList();

            return PartialView("~/Views/UserDetail/_UserDetailListPartial.cshtml", models);
        }
        [HttpPost]
        public async Task<IActionResult> UpSertUserDetail(UserDetail modelEntity)
        {
            if(modelEntity.Id>0)
            {
                var updateModel = CommanCRUDHelper.CommanUpdateCode(modelEntity, 1);
                var updateResponse = await _IUserDetailService.Update(updateModel);
                return Json(ResponseHelper.GetResponseMessage(updateResponse));
            }
            var createModel = CommanCRUDHelper.CommanCreateCode(modelEntity, 1);
            var createReposnse = await _IUserDetailService.Update(createModel);
            return Json(ResponseHelper.GetResponseMessage(createReposnse));
        }

        public async Task<IActionResult> UpsertUserDetail(int id)
        {
            ViewBag.RoleDetail = await _IRoleMasterService.GetList(x => x.IsActive == 1);
            var model = await _IUserDetailService.GetSingle(x => x.Id == id);
            return PartialView("~/Views/UserDetail/_UserDetailCreatePartial.cshtml", model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _IUserDetailService.GetSingle(x => x.Id == id);
            var deleteModel = CommanCRUDHelper.CommanDeleteCode(model, 1);
            await _IUserDetailService.CreateNewContext();
            var response = await _IUserDetailService.Update(deleteModel);
            return Json(ResponseHelper.GetResponseMessage(response));
        }
    }
}