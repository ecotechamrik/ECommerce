using BAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcoTechAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebsiteController : ControllerBase
    {
        static IUnitOfWork uow;
        public WebsiteController(IUnitOfWork _uow)
        {
            uow = _uow;
            uow.WebsiteInfoRepo.DbInitialize();
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
            return uow.WebsiteInfoRepo.GetAll().Where(w => (id != null ? w.WebsiteID == id : w.WebsiteID == w.WebsiteID)).OrderBy(o => o.WebsiteName).ToList();
        }
        #endregion

        // POST: api/website
        [HttpPost]
        public IActionResult Post([FromBody] WebsiteInfo model)
        {
            try
            {
                uow.WebsiteInfoRepo.Add(model);
                uow.SaveChanges();
                return Created("/api/website", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // PUT: api/website/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] WebsiteInfo model)
        {
            try
            {
                uow.WebsiteInfoRepo.Update(model);
                uow.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/website/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                uow.WebsiteInfoRepo.DeleteByID(id);
                uow.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
    }
}
