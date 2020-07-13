using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.ViewModels
{
    public class WebsiteInfoViewModel
    {
        public int WebsiteID { get; set; }
        public string WebsiteName { get; set; }
        public string WebsiteBannerTitle { get; set; }
        public string WebsiteBannerTagLine { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDesc { get; set; }
        public string ContactEmailID { get; set; }
        public string Cell { get; set; }
        public string OfficePhone { get; set; }
        public string Fax { get; set; }
        public string DevelopedBy { get; set; }
        public string Address { get; set; }
        public string AddressMap { get; set; }
    }
}
