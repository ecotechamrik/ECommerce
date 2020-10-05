#region [ Namespace ]
using BAL.ViewModels.Product;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace BAL
{
    public class ProductSizeAndPriceAPIResponse: GenerateAPIResponse<ProductSizeAndPriceViewModel>, IProductSizeAndPriceAPIResponse
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

        #region [ Save Entity ]
        /// <summary>
        /// Save Entity Record into DB
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <returns></returns>
        public async Task<ProductSizeAndPriceViewModel> SaveModel(String apiMethod, ProductSizeAndPriceViewModel entity)
        {
            string entitydata = JsonConvert.SerializeObject(entity);
            StringContent content = new StringContent(entitydata, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(client.BaseAddress + apiMethod, content);

            ProductSizeAndPriceViewModel model = new ProductSizeAndPriceViewModel();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<ProductSizeAndPriceViewModel>(data);
            }

            return model;
        }
        #endregion

        #region [ Update Price Void ]
        /// <summary>
        /// Update Price Void
        /// </summary>
        /// <param name="apiMethod"></param>
        /// <param name="id"></param>
        /// <param name="pricevoid"></param>
        /// <returns></returns>
        public async Task<Boolean> UpdatePriceVoid(string apiMethod, int id, double pricevoid)
        {
            var response = await client.GetAsync(client.BaseAddress + apiMethod + "/" + id + "/" + pricevoid);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
        #endregion
    }
}