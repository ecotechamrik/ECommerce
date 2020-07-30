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
    public class DoorTypeController : ControllerBase
    {
        IUnitOfWork uow;
        public DoorTypeController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // GET: api/doortype
        [HttpGet]
        public IEnumerable<DoorType> Get()
        {
            return GetDoorTypeDetails(null);
        }

        // GET: api/doortype/5
        [HttpGet("{id}", Name = "DoorType")]
        public IEnumerable<DoorType> Get(int id)
        {
            return GetDoorTypeDetails(id);
        }

        #region [ Show all or Selected Door Type information ]
        /// <summary>
        /// Show all or selected Door Type information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<DoorType> GetDoorTypeDetails(int? id)
        {
            return uow.DoorTypeRepo.GetAll().Where(c => (id != null ? c.DoorTypeID == id : c.DoorTypeID == c.DoorTypeID));            
        }
        #endregion

        // POST: api/doortype
        [HttpPost]
        public IActionResult Post([FromBody] DoorType model)
        {
            try
            {
                uow.DoorTypeRepo.Add(model);
                uow.SaveChanges();
                return Created("/api/doortype", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // PUT: api/doortype/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DoorType model)
        {
            try
            {
                uow.DoorTypeRepo.Update(model);
                uow.SaveChanges();
                return Created("/api/doortype", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/doortype/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                uow.DoorTypeRepo.DeleteByID(id);
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