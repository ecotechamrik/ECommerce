using System.ComponentModel.DataAnnotations;

namespace BAL.ViewModels
{
    public class WebsiteInfoViewModel
    {
        public int WebsiteID { get; set; }

        [Display(Name = "Website Name", Prompt = "Website Name")]
        [Required(ErrorMessage = "Enter Website Name")]
        public string WebsiteName { get; set; }

        [Display(Name = "Website Banner Title", Prompt = "Website Banner Title")]
        public string WebsiteBannerTitle { get; set; }

        [Display(Name = "Website Banner Tag Line", Prompt = "Website Banner Tag Line")]
        public string WebsiteBannerTagLine { get; set; }

        [Display(Name = "Company Name", Prompt = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Company Desc", Prompt = "Company Desc")]
        public string CompanyDesc { get; set; }

        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        [Display(Name = "Contact Email ID", Prompt = "example@example.org")]
        public string ContactEmailID { get; set; }

        [Display(Name = "Cell", Prompt = "Cell")]
        public string Cell { get; set; }

        [Display(Name = "Office Phone", Prompt = "Office Phone")]
        public string OfficePhone { get; set; }

        [Display(Name = "Fax", Prompt = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "Developed By", Prompt = "Developed By")]
        public string DevelopedBy { get; set; }

        [Display(Name = "Address", Prompt = "Address")]
        public string Address { get; set; }

        [Display(Name = "Address Map", Prompt = "Address Map")]
        public string AddressMap { get; set; }

        [Display(Name = "URL", Prompt = "URL")]
        public string URL { get; set; }

        [Display(Name = "Cpanel User", Prompt = "Cpanel User")]
        public string CpanelUser { get; set; }

        [Display(Name = "Cpanel Password", Prompt = "Cpanel Password")]
        public string CpanelPassword { get; set; }

        [Display(Name = "FTP User", Prompt = "FTP User")]
        public string FTPUser { get; set; }

        [Display(Name = "FTP Password", Prompt = "FTP Password")]
        public string FTPPassword { get; set; }

        [Display(Name = "DB User", Prompt = "DB User")]
        public string DBUser { get; set; }

        [Display(Name = "DB Password", Prompt = "DB Password")]
        public string DBPassword { get; set; }

        [Display(Name = "Admin User", Prompt = "Admin User")]
        public string AdminUser { get; set; }

        [Display(Name = "Admin Password", Prompt = "Admin Password")]
        public string AdminPassword { get; set; }

        [Display(Name = "Domain Renew Date", Prompt = "Domain Renew Date")]
        public string DomainRenewDate { get; set; }

        [Display(Name = "Hosting Renew Date", Prompt = "Hosting Renew Date")]
        public string HostingRenewDate { get; set; }

        [Display(Name = "Hosting Provider Name", Prompt = "Hosting Provider Name")]
        public string HostingProviderName { get; set; }

        [Display(Name = "Hosting Provider Desc", Prompt = "Hosting Provider Desc")]
        public string HostingProviderDesc { get; set; }

        [Display(Name = "Hosting Provider Contact No.", Prompt = "Hosting Provider Contact No.")]
        public string HostingProviderContactNo { get; set; }
    }
}
