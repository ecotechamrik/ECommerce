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
    public class CurrencyController : ControllerBase
    {
        IUnitOfWork uow;
        public CurrencyController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // GET: api/Currency
        [HttpGet]
        public IEnumerable<Currency> Get()
        {
            return GetCurrencyDetails(null);
        }

        // GET: api/Currency/5
        [HttpGet("{id}", Name = "Currency")]
        public IEnumerable<Currency> Get(int id)
        {
            return GetCurrencyDetails(id);
        }

        #region [ Show all or Selected Currency information ]
        /// <summary>
        /// Show all or selected Currency information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Currency> GetCurrencyDetails(int? id)
        {
            return uow.CurrencyRepo.GetAll().Where(c => (id != null ? c.CurrencyID == id : c.CurrencyID == c.CurrencyID)).OrderBy(o => o.CurrencyName);
        }
        #endregion

        // POST: api/Currency
        [HttpPost]
        public IActionResult Post([FromBody] Currency model)
        {
            try
            {
                uow.CurrencyRepo.Add(model);
                uow.SaveChanges();
                return Created("/api/Currency", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // PUT: api/Currency/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Currency model)
        {
            try
            {
                uow.CurrencyRepo.Update(model);
                uow.SaveChanges();
                return Created("/api/Currency", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/Currency/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                uow.CurrencyRepo.DeleteByID(id);
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