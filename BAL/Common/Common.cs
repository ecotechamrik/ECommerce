using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BAL
{
    public class Common
    {
        #region [ Read Website URL Path from appsettings.json ]
        /// <summary>
        /// Read Website URL Path from appsettings.json
        /// </summary>
        // Return Website Path URL
        public static readonly string WEBSITEPATH;
        static Common()
        {
            // Set Websith Path URL value from appsettings.json file
            if (WEBSITEPATH == null)
            {
                WEBSITEPATH = GetSectionString("WebsiteURL", "");
            }
        }
        #endregion

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
                return configuration[section + ":" + subsection];// configuration.GetSection(section).GetSection(subsection).Value;
            else
                return configuration[section]; // configuration.GetSection(section).Value;
        }
        #endregion
    }
}