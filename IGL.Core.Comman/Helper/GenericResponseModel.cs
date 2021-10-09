using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace IGL.Core.Comman.Helper
{
    public class GenericResponseModel<TEntity, T> where TEntity : class
    {
        public T ResponseData { get; set; }
        public TEntity EntityModel { get; set; }
        public List<TEntity> Entities { get; set; }
        public string ResponseMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public GenericResponseModel<TEntity, T> GetResponseModel(T responseData, TEntity model, List<TEntity> models,
            string message, HttpStatusCode statusCode)
        {

            return new GenericResponseModel<TEntity, T>()
            {
                ResponseData = responseData,
                EntityModel = model,
                Entities = models,
                ResponseMessage = message,
                StatusCode = statusCode
            };
        }
    }
}
