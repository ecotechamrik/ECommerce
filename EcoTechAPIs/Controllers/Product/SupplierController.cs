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
    public class SupplierController : ControllerBase
    {
        IUnitOfWork uow;
        public SupplierController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // GET: api/Supplier
        [HttpGet]
        public IEnumerable<Supplier> Get()
        {
            return GetSupplierDetails(null);
        }

        // GET: api/supplier/5
        [HttpGet("{id}", Name = "Supplier")]
        public IEnumerable<Supplier> Get(int id)
        {
            return GetSupplierDetails(id);
        }

        #region [ Show all or Selected Supplier information ]
        /// <summary>
        /// Show all or selected Supplier information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Supplier> GetSupplierDetails(int? id)
        {
            return uow.SupplierRepo.GetAll().Where(c => (id != null ? c.SupplierID == id : c.SupplierID == c.SupplierID)).OrderBy(o => o.SupplierName);
        }
        #endregion

        // POST: api/supplier
        [HttpPost]
        public IActionResult Post([FromBody] Supplier model)
        {
            try
            {
                uow.SupplierRepo.Add(model);
                uow.SaveChanges();
                return Created("/api/supplier", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // PUT: api/Supplier/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Supplier model)
        {
            try
            {
                uow.SupplierRepo.Update(model);
                uow.SaveChanges();
                return Created("/api/supplier", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/Supplier/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                uow.SupplierRepo.DeleteByID(id);
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