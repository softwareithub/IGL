using IGL.Core.Comman.Comman;
using System;
using System.Collections.Generic;
using System.Text;

namespace IGL.Core.Comman.Helper
{
    public static class ResponseHelper
    {
        public static string GetResponseMessage(ResponseMessage responseMessage)
        {
            return responseMessage switch
            {
                ResponseMessage.AddedSuccessfully => "Record inserted successfully !!!",
                ResponseMessage.AlreadyExists => "Record already present, please check List !!!",
                ResponseMessage.DeletedSuccessfully => "Record deleted successfully",
                ResponseMessage.ServerError => "There is something wents wrong, Please refresh the page and try again !!!",
                ResponseMessage.UpdatedSuccessfully => "Record has been updated successfully !!!",
                _ => string.Empty,
            };
        }
    }
}
