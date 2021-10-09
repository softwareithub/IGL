using IGL.Core.Comman.Helper;
using IGL.Core.Entities.Master;
using IGL.Core.Entities.UserManagement;
using IGL.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IGL.UI.Controllers.UserManagement
{
    public class RoleAccessController : Controller
    {
        private static string RoleDetails = @"MenuSubMenuAPI/GetRoleDetails";
        private static string RoleAccessDetails = @"MenuSubMenuAPI/GetRoleMenuDetails";
        private static string PostUrl = @"MenuSubMenuAPI/CreateRoleAccess";
        private readonly IConfiguration _configuration;
        private readonly string _apiURL;

        public RoleAccessController(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiURL = _configuration.GetSection("APIURL:BasicUrl").Value;
        }
        public async Task<IActionResult> RoleAccessIndex()
        {

            var responseDetails = await GenericAPIResponse<RoleMaster, string>.GetAPIResponse(_apiURL, RoleDetails);
            if (responseDetails.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ViewBag.RoleMaster = responseDetails.Entities;
            }
            return View("~/Views/RoleAccess/RoleAccessIndex.cshtml");
        }

        public async Task<IActionResult> GetRoleAccessDetails(int roleId)
        {
            ViewBag.roleId = roleId;
            var responseDetails = await GenericAPIResponse<MenuSubMenuModel, string>
                .GetAPIResponse(_apiURL, RoleAccessDetails + "?roleId=" + roleId);

            return PartialView("~/Views/RoleAccess/RoleAccessDetail.cshtml", responseDetails.Entities);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoleMap(string[] MenuId, int roleId)
        {
            var roleAccessDetails = new List<RoleAccess>();

            GenericResponseModel<string, string> responseModel;

            MenuId.ToList().ForEach(data =>
            {
                var model = new RoleAccess();
                model.IsActive = 1;
                model.IsDeleted = 0;
                model.CreatedBy = 1;
                model.CreatedDate = DateTime.Now;
                model.MenuId = Convert.ToInt32(data);
                model.RoleId = roleId;

                roleAccessDetails.Add(model);
            });

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiURL);

                StringContent content = new StringContent(JsonConvert.SerializeObject(roleAccessDetails),
                    Encoding.UTF8, "application/json");

                using var response = await client.PostAsync(PostUrl, content);
                string apiResponse = await response.Content.ReadAsStringAsync();
                responseModel = JsonConvert.DeserializeObject<GenericResponseModel<string, string>>(apiResponse);
            }
            if (responseModel.StatusCode == System.Net.HttpStatusCode.OK) {
                return Json(responseModel.ResponseMessage);
            }
            return Json(responseModel.ResponseMessage);
        }
    }
}
