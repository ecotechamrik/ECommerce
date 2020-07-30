#region [ Namespace ]
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
    public class GenerateAPIResponse<TEntity> : IGenerateAPIResponse<TEntity> where TEntity : class
    {
        #region [ Local Variables ]
        // Get API URL from appsettings.json
        IConfiguration config;

        // HttpClient Variable to access the Web APIs
        HttpClient client;
        #endregion

        #region [ Default Constructor  ]
        public GenerateAPIResponse(IConfiguration _config)
        {
            client = new HttpClient();
            config = _config;
            client.BaseAddress = new Uri(config["URL:API"]);
        }
        #endregion

        #region [ Get All Entities From DB ]
        /// <summary>
        /// Get API Response with for the given TEntity
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAll(String apiMethod)
        {
            var response = await client.GetAsync(client.BaseAddress + apiMethod);
            IEnumerable<TEntity> model = new List<TEntity>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(data);
            }

            return model;
        }
        #endregion

        #region [ Get Entity By ID ]
        /// <summary>
        /// Get API Response with for the given TEntity
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <returns></returns>
        public async Task<TEntity> GetByID(String apiMethod, Int32 id)
        {
            var response = await client.GetAsync(client.BaseAddress + apiMethod + "/" + id);
            TEntity model;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                if (data != "" && data.StartsWith('['))
                    data = data.Substring(1, data.Length - 2);
                model = JsonConvert.DeserializeObject<TEntity>(data);

                if (model != null)
                    return model;                
            }
            return null;
        }
        #endregion

        #region [ Save Entity ]
        /// <summary>
        /// Save Entity Record into DB
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <returns></returns>
        public async Task<Boolean> Save(String apiMethod, TEntity entity)
        {
            string data = JsonConvert.SerializeObject(entity);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(client.BaseAddress + apiMethod, content);            

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
        #endregion

        #region [ Update Entity ]
        /// <summary>
        /// Update Entity Record into DB
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <returns></returns>
        public async Task<Boolean> Update(String apiMethod, TEntity entity)
        {
            string data = JsonConvert.SerializeObject(entity);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(client.BaseAddress + apiMethod, content);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
        #endregion

        #region [ Delete Entity ]
        /// <summary>
        /// Delete Entity Record into DB
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <returns></returns>
        public async Task<Boolean> Delete(String apiMethod)
        {
            var response = await client.DeleteAsync(client.BaseAddress + apiMethod);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
        #endregion
    }
}
