using IGL.Core.Comman.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGL.API.Helper
{
    public class HttpResponseHelper<TEntity,T> : IActionResult where TEntity : class
    {
        private  GenericResponseModel<TEntity, T> ResponseHelper { get; set; }
        public HttpResponseHelper(GenericResponseModel<TEntity, T> responseModel)
        {
            this.ResponseHelper = responseModel;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(ResponseHelper)
            {
                StatusCode = Convert.ToInt32(ResponseHelper.StatusCode)
            };
            await objectResult.ExecuteResultAsync(context);
        }
    }
}
