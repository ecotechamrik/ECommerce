using BAL.Entities;
using BAL.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcoTechAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWidthController : ControllerBase
    {
        IUnitOfWork uow;
        public ProductWidthController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // GET: api/productWidth
        [HttpGet]
        public IEnumerable<ProductWidth> Get()
        {
            return GetProductWidthDetails(null);
        }

        // GET: api/productWidth/5
        [HttpGet("{id}", Name = "ProductWidth")]
        public IEnumerable<ProductWidth> Get(int id)
        {
            return GetProductWidthDetails(id);
        }

        #region [ Show all or Selected Product Width information ]
        /// <summary>
        /// Show all or selected Product Width information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<ProductWidth> GetProductWidthDetails(int? id)
        {
            return uow.ProductWidthRepo.GetAll().Where(c => (id != null ? c.ProductWidthID == id : c.ProductWidthID == c.ProductWidthID)).OrderBy(o => o.ProductWidthName);
        }
        #endregion

        // POST: api/productWidth
        [HttpPost]
        public IActionResult Post([FromBody] ProductWidth model)
        {
            try
            {
                uow.ProductWidthRepo.Add(model);
                uow.SaveChanges();
                return Created("/api/productWidth", model);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/productWidth/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductWidth model)
        {
            try
            {
                uow.ProductWidthRepo.Update(model);
                uow.SaveChanges();
                return Created("/api/productWidth", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/productWidth/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                uow.ProductWidthRepo.DeleteByID(id);
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