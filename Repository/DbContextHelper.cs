using DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Repository
{
    public static class DbContextHelper
    {
        #region [ Helper Class to Read Database ConnectionString Value from AppSettings.JSON file ]
        /// <summary>
        /// Helper Class to Read Database ConnectionString Value from AppSettings.JSON file
        /// </summary>
        /// <returns></returns>
        public static DbContextOptions<DatabaseContext> GetDbContextOptions()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return new DbContextOptionsBuilder<DatabaseContext>()
                  .UseSqlServer(new SqlConnection(configuration.GetConnectionString("EcoTechCon"))).Options;
        }
        #endregion
    }
}
