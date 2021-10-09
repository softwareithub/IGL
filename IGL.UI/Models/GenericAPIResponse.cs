using IGL.Core.Comman.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IGL.UI.Models
{
    public static class GenericAPIResponse<TEntity, T> where TEntity : class
    {
        public static async Task<GenericResponseModel<TEntity, T>> GetAPIResponse(string baseUrl, string apiUrl)
        {
            using var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            using var response = await client.GetAsync(apiUrl);

            string apiResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GenericResponseModel<TEntity, T>>(apiResponse);
        }
    }
}
