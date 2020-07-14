using System.Collections.Generic;
using System.Linq;
using Repository;
using BAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using BAL.Entities;
using System;

namespace EcoTechAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebsiteController : ControllerBase
    {
        static IUnitOfWork db;        
        public WebsiteController(IUnitOfWork _db)
        {
            db = _db;
            db.WebsiteInfoRepo.DbInitialize();
        }

        // GET: api/website
        [HttpGet]
        public IList<WebsiteInfo> Get()
        {
            return websites(null);
        }

        // GET: api/website/5
        [HttpGet("{id}", Name = "Website")]
        public List<WebsiteInfo> Get(int id)
        {
            return websites(id);
        }

        #region [ Show all or selected website information ]
        /// <summary>
        /// Show all or selected website information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // Func is not Required here but used just for practice purposes.
        Func<int?, List<WebsiteInfo>> websites = new Func<int?, List<WebsiteInfo>>(GetWebsiteInfoDetails); 
        static List<WebsiteInfo> GetWebsiteInfoDetails(int? id)
        {
            return db.WebsiteInfoRepo.GetAll().Where(w => (id!=null ? w.WebsiteID == id: w.WebsiteID== w.WebsiteID)).ToList();
        }
        #endregion

        // POST: api/website
        [HttpPost]
        public IActionResult Post([FromBody] WebsiteInfo model)
        {
            try
            {
                db.WebsiteInfoRepo.Add(model);
                db.SaveChanges();
                return Created("/api/website", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // PUT: api/website/5
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
