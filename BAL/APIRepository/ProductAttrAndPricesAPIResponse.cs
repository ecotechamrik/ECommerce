#region [ Namespace ]
using BAL.ViewModels.Product;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
#endregion

namespace BAL
{
    public class ProductSizeAndPriceAPIResponse : GenerateAPIResponse<ProductSizeAndPriceViewModel>, IProductSizeAndPriceAPIResponse
    {
        #region [ Local Variables ]
        // Get API URL from appsettings.json
        IConfiguration config;

        // HttpClient Variable to access the Web APIs
        HttpClient client;
        #endregion

        #region [ Default Constructor  ]
        public ProductSizeAndPriceAPIResponse(IConfiguration _config) : base(_config)
        {
            client = new HttpClient();
            config = _config;
            client.BaseAddress = new Uri(config["URL:API"]);
        }
        #endregion

        #region [ Get By ProductID ]
        /// <summary>
        /// Get By ProductID
        /// </summary>
        /// <param name="apiMethod"></param>
        /// <param name="pid"></param>        
        /// <returns></returns>
        public async Task<IEnumerable<ProductSizeAndPriceViewModel>> GetByProductID(string apiMethod, int? pid)
        {
            var response = await client.GetAsync(client.BaseAddress + apiMethod + "/" + pid );

            IEnumerable<ProductSizeAndPriceViewModel> model = new List<ProductSizeAndPriceViewModel>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<IEnumerable<ProductSizeAndPriceViewModel>>(data);
            }

            return model;
        }
        #endregion
    }
}