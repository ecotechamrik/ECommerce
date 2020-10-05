#region [ Namespace ]
using BAL.ViewModels.Product;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
#endregion

namespace BAL
{
    public class ProductImagesAPIResponse: GenerateAPIResponse<ProductImageViewModel>, IProductImagesAPIResponse
    {
        #region [ Local Variables ]
        // Get API URL from appsettings.json
        IConfiguration config;

        // HttpClient Variable to access the Web APIs
        HttpClient client;
        #endregion

        #region [ Default Constructor  ]
        public ProductImagesAPIResponse(IConfiguration _config) : base(_config)
        {
            client = new HttpClient();
            config = _config;
            client.BaseAddress = new Uri(config["URL:API"]);
        }
        #endregion

        #region [ Update Image Order ]
        /// <summary>
        /// Update Image Order
        /// </summary>
        /// <param name="apiMethod"></param>
        /// <param name="id"></param>
        /// <param name="orderno"></param>
        /// <returns></returns>
        public async Task<Boolean> UpdateOrder(string apiMethod, int id, int orderno)
        {
            var response = await client.GetAsync(client.BaseAddress + apiMethod + "/" + id + "/" + orderno);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
        #endregion
    }
}