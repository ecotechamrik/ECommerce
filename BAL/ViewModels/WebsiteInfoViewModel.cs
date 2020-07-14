using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BAL.ViewModels
{
    public class WebsiteInfoViewModel
    {
        public int WebsiteID { get; set; }

        [DisplayName("Website Name")]
        public string WebsiteName { get; set; }
        public string URL { get; set; }

        [DisplayName("Cpanel User")]
        public string CpanelUser { get; set; }

        [DisplayName("Cpanel Password")]
        public string CpanelPassword { get; set; }

        [DisplayName("FTP User")]
        public string FTPUser { get; set; }

        [DisplayName("FTP Password")]
        public string FTPPassword { get; set; }
        
        [DisplayName("DB User")] 
        public string DBUser { get; set; }

        [DisplayName("DB Password")]
        public string DBPassword { get; set; }

        [DisplayName("Admin User")]
        public string AdminUser { get; set; }

        [DisplayName("Admin Password")]
        public string AdminPassword { get; set; }

        [DisplayName("Domain Renew Date")]
        public string DomainRenewDate { get; set; }

        [DisplayName("Hosting Renew Date")]
        public string HostingRenewDate { get; set; }

        [DisplayName("Hosting Provider Name")]
        public string HostingProviderName { get; set; }

        [DisplayName("Hosting Provider Desc")]
        public string HostingProviderDesc { get; set; }

        [DisplayName("Hosting Provider Contact No.")]
        public string HostingProviderContactNo { get; set; }
        
        [DisplayName("Website Banner Title")]
        public string WebsiteBannerTitle { get; set; }
        
        [DisplayName("Website Banner Tag Line")]
        public string WebsiteBannerTagLine { get; set; }

        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [DisplayName("Company Desc")]
        public string CompanyDesc { get; set; }

        [DisplayName("Contact Email ID")]
        public string ContactEmailID { get; set; }
        public string Cell { get; set; }

        [DisplayName("Office Phone")]
        public string OfficePhone { get; set; }
        public string Fax { get; set; }

        [DisplayName("Developed By")]
        public string DevelopedBy { get; set; }
        public string Address { get; set; }

        [DisplayName("Address Map")]
        public string AddressMap { get; set; }
    }
}
