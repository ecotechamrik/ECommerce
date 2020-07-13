using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoTechAPIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EcoTechAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        IList<WebsiteInfo> _websites;
        WebsiteInfo _websiteInfo1, _websiteInfo2;
        public HomeController()
        {
            _websites = new List<WebsiteInfo>();

            _websiteInfo1 = new WebsiteInfo
            {
                WebsiteID = 1,
                WebsiteName = "Doors Seattle",
                WebsiteBannerTitle = "Doors Seattle",
                WebsiteBannerTagLine = "Quality Residential and Commercial Doors for Seattle",
                CompanyName = "Eco Tech Doors",
                CompanyDesc = "What we are really good at however is service. Our professionally trained designers are experts in doors and thier use in residential applications. The team can walk you through the selection process, the opening requirments, the finishing, installation and use of each of our products. Our primary goal is 100 percent accuracy at the job site. Our people are dedicated to making that happen for you.",
                ContactEmailID = "info@doorex.com",
                OfficePhone = "1.888.238.2687",
                Cell = "604.677.1144",
                Fax = "604.677.1146",
                DevelopedBy = "Department of MIS, Eco Tech Doors",
                Address = "1927 Boblett Street, Blaine WA, 98230",
                AddressMap = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2618.0839154344626!2d-122.72870768432105!3d48.98996047930062!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x5485c6b02ed90c57%3A0xe795e1458885dc47!2s1927+Boblett+St%2C+Blaine%2C+WA+98230%2C+USA!5e0!3m2!1sen!2sca!4v1521407241440"
            };

            _websiteInfo2 = new WebsiteInfo
            {
                WebsiteID = 4,
                WebsiteName = "Doors BC",
                WebsiteBannerTitle = "Eco Tech Doors Kamloops",
                WebsiteBannerTagLine = "Welcome to Factory Direct Doors Kamloops. A web site chalk full of products specifically selected for the Kamloops area environment.",
                CompanyName = "Eco Tech Doors",
                CompanyDesc = "Although our finished product is doors, our expertise is impeccable workmanship, exceptional selection, extra-ordinary design, , considerate style, and professional solutions to your building requirements. <br/>" +
                              "A Creer Industries company dedicated to serving the greater Kamloops market with the widest range of man door products available today.From our custom built lines to commodity door products from some of the worlds largest manufacturer's we strive to provide our customers with the same answer to the most common question asked of us, that is \"Do you have\". The answer is yes!",
                ContactEmailID = "info@doorex.com",
                OfficePhone = "1.888.238.2687",
                Cell = "604.677.1144",
                Fax = "604.677.1146",
                DevelopedBy = "Department of MIS, Eco Tech Doors",
                Address = "4740 Vanguard Road, Richmond BC, V6X 2P8",
                AddressMap = "https://maps.google.com/maps?q=4740+Vanguard+Road&amp;sll=49.180568,-123.102320&amp;hl=en&amp;ie=UTF8&amp;hq=&amp;hnear=4740+Vanguard+Rd,+Richmond,+British+Columbia+V6X+2P8,+Canada&amp;t=m&amp;ll=49.180581,-123.102322&amp;spn=0.016832,0.025749&amp;z=14&amp;iwloc=A&amp;output=embed"
            };
            _websites.Add(_websiteInfo1);
            _websites.Add(_websiteInfo2);
        }

        // GET: api/Home
        [HttpGet]
        public List<WebsiteInfo> Get()
        {
            return _websites.ToList();
        }

        // GET: api/Home/5
        [HttpGet("{id}", Name = "Website")]
        public List<WebsiteInfo> Get(int id)
        {
            return _websites.Where(w => w.WebsiteID == id).ToList();
        }

        // POST: api/Home
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Home/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
