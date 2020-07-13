using System.Collections.Generic;
using System.Linq;
using Repository;
using BAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using BAL.Entities;

namespace EcoTechAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        IUnitOfWork db;
        public HomeController(IUnitOfWork _db)
        {
            db = _db;
        }

        // GET: api/Home
        [HttpGet]
        public IList<WebsiteInfo> Get()
        {
            db.WebsiteInfoRepo.DbInitialize();
            return GetWebsiteInfoDetails(null);
        }

        // GET: api/Home/5
        [HttpGet("{id}", Name = "Website")]
        public List<WebsiteInfo> Get(int id)
        {
            return GetWebsiteInfoDetails(id);
        }

        private List<WebsiteInfo> GetWebsiteInfoDetails(int? id)
        {
            db.WebsiteInfoRepo.DbInitialize();
            return db.WebsiteInfoRepo.GetAll().Where(w => (id!=null ? w.WebsiteID == id: w.WebsiteID== w.WebsiteID)).ToList();
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
