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
    public class SectionController : ControllerBase
    {
        IUnitOfWork uow;
        public SectionController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // GET: api/section
        [HttpGet]
        public IEnumerable<Section> Get()
        {
            return GetSectionDetails(null);
        }

        // GET: api/section/5
        [HttpGet("{id}", Name = "Section")]
        public IEnumerable<Section> Get(int id)
        {
            return GetSectionDetails(id);
        }

        #region [ Show all or Selected Section information ]
        /// <summary>
        /// Show all or selected Section information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Section> GetSectionDetails(int? id)
        {
            return uow.SectionRepo.GetAll().Where(c => (id != null ? c.SectionID == id : c.SectionID == c.SectionID)).OrderBy(o => o.SectionName);
        }
        #endregion

        // POST: api/section
        [HttpPost]
        public IActionResult Post([FromBody] Section model)
        {
            try
            {
                uow.SectionRepo.Add(model);
                uow.SaveChanges();
                return Created("/api/section", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // PUT: api/section/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Section model)
        {
            try
            {
                uow.SectionRepo.Update(model);
                uow.SaveChanges();
                return Created("/api/section", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/section/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                uow.SectionRepo.DeleteByID(id);
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