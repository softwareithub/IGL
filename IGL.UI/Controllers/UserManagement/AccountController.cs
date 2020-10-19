using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IGL.Core.Comman.Helper;
using IGL.Core.Entities.Master;
using IGL.Core.Service.GenericService;
using IGL.Core.ViewModelEntities.Authenticate;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IGL.UI.Controllers.UserManagement
{
    public class AccountController : Controller
    {
        private readonly IGenericService<UserDetail, int> _UserDetailService;

        public AccountController(IGenericService<UserDetail, int> userDetailService)
        {
            _UserDetailService = userDetailService;
        }
        [AllowAnonymous]
        public IActionResult Index(string message)
        {
            ViewData["ErrorMessage"] = message;
            return View("~/Views/Authenticate/Login.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticateUser(AuthenticateModel model)
        {
            var userModels = await _UserDetailService.GetList(x => x.IsActive == 1);

            if(userModels.Any(x=>x.UserName==model.UserName && x.Password== model.Password))
            {

                var userModel = userModels.Where(x => x.UserName == model.UserName && x.Password == model.Password).First();

                var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name,model.UserName),
                        new Claim(ClaimTypes.Role, userModel.RoleId.ToString())
                };
                var claimIdentity = new ClaimsIdentity(claims, "Login");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));

                HttpContext.Session.SetInt32("userId", userModel.Id);

                return RedirectToAction("Index", "DashBoard");
            }
            else
            {

                return RedirectToAction("Index", "Account", new { message = "Invalid User Name or Password." });
            }
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Account");
        }

        public IActionResult Lock()
        {
            return View("~/Views/Authenticate/LockScreen.cshtml");
        }

        public IActionResult ChangePassword()
        {
            return PartialView("~/Views/UserDetail/_ChangePasswordPartial.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(UserDetail entity)
        {
            var model = await _UserDetailService.GetSingle(x => x.Id == Convert.ToInt32(HttpContext.Session.GetInt32("userId")));
            model.Password = entity.Password;
            var udpateModel = CommanCRUDHelper.CommanUpdateCode(model, Convert.ToInt32(HttpContext.Session.GetInt32("userId")));
            var response = await _UserDetailService.Update(udpateModel);
            return Json(ResponseHelper.GetResponseMessage(response));
        }
    }
}