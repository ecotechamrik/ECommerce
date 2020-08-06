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
    public class ProductSizeController : ControllerBase
    {
        IUnitOfWork uow;
        public ProductSizeController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // GET: api/productSize
        [HttpGet]
        public IEnumerable<ProductSize> Get()
        {
            return GetProductSizeDetails(null);
        }

        // GET: api/productSize/5
        [HttpGet("{id}", Name = "ProductSize")]
        public IEnumerable<ProductSize> Get(int id)
        {
            return GetProductSizeDetails(id);
        }

        #region [ Show all or Selected Product Size information ]
        /// <summary>
        /// Show all or selected Product Size information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<ProductSize> GetProductSizeDetails(int? id)
        {
            return uow.ProductSizeRepo.GetAll().Where(c => (id != null ? c.ProductSizeID == id : c.ProductSizeID == c.ProductSizeID));
        }
        #endregion

        // POST: api/productSize
        [HttpPost]
        public IActionResult Post([FromBody] ProductSize model)
        {
            try
            {
                uow.ProductSizeRepo.Add(model);
                uow.SaveChanges();
                return Created("/api/productSize", model);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/productSize/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductSize model)
        {
            try
            {
                uow.ProductSizeRepo.Update(model);
                uow.SaveChanges();
                return Created("/api/productSize", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/productSize/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                uow.ProductSizeRepo.DeleteByID(id);
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