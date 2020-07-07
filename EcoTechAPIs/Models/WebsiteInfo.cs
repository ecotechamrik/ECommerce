using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoTechAPIs.Models
{
    public class WebsiteInfo
    {
        public int WebsiteID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string CpanelUser { get; set; }
        public string CpanelPassword { get; set; }
        public int BannerWebsiteTitle { get; set; }
        public int BannerWebsiteTagLine { get; set; }
        public int CompanyName { get; set; }

    }
}
