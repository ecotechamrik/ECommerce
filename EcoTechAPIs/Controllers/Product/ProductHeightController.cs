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
    public class ProductHeightController : ControllerBase
    {
        IUnitOfWork uow;
        public ProductHeightController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // GET: api/productHeight
        [HttpGet]
        public IEnumerable<ProductHeight> Get()
        {
            return GetProductHeightDetails(null);
        }

        // GET: api/productHeight/5
        [HttpGet("{id}", Name = "ProductHeight")]
        public IEnumerable<ProductHeight> Get(int id)
        {
            return GetProductHeightDetails(id);
        }

        #region [ Show all or Selected Product Height information ]
        /// <summary>
        /// Show all or selected Product Height information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<ProductHeight> GetProductHeightDetails(int? id)
        {
            return uow.ProductHeightRepo.GetAll().Where(c => (id != null ? c.ProductHeightID == id : c.ProductHeightID == c.ProductHeightID));
        }
        #endregion

        // POST: api/productHeight
        [HttpPost]
        public IActionResult Post([FromBody] ProductHeight model)
        {
            try
            {
                uow.ProductHeightRepo.Add(model);
                uow.SaveChanges();
                return Created("/api/productHeight", model);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/productHeight/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] ProductHeight model)
        {
            try
            {
                uow.ProductHeightRepo.Update(model);
                uow.SaveChanges();
                return Created("/api/productHeight", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/productHeight/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                uow.ProductHeightRepo.DeleteByID(id);
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