using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BAL
{
    public class Common
    {
        #region [ Read Any Section Value from AppSettings.JSON file ]
        /// <summary>
        /// Read Section Value from AppSettings.JSON file
        /// </summary>
        /// <returns></returns>
        public static string GetSectionString(String section, String subsection)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            if (subsection != String.Empty)
                return configuration.GetSection(section).GetSection(subsection).Value;
            else
                return configuration.GetSection(section).Value;
        }
        #endregion
    }
}