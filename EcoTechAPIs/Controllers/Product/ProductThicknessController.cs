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
    public class ProductThicknessController : ControllerBase
    {
        IUnitOfWork uow;
        public ProductThicknessController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // GET: api/productThickness
        [HttpGet]
        public IEnumerable<ProductThickness> Get()
        {
            return GetProductThicknessDetails(null);
        }

        // GET: api/productThickness/5
        [HttpGet("{id}", Name = "ProductThickness")]
        public IEnumerable<ProductThickness> Get(int id)
        {
            return GetProductThicknessDetails(id);
        }

        #region [ Show all or Selected Product Thickness information ]
        /// <summary>
        /// Show all or selected Product Thickness information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<ProductThickness> GetProductThicknessDetails(int? id)
        {
            return uow.ProductThicknessRepo.GetAll().Where(c => (id != null ? c.ProductThicknessID == id : c.ProductThicknessID == c.ProductThicknessID)).OrderBy(o => o.ProductThicknessName);
        }
        #endregion

        // GET: api/GetWithAttributeThicknessID/1
        [Route("{GetWithAttributeThicknessID}/{ProductAttributeID}")]
        public IEnumerable<ProductThicknessViewModel> GetWithAttributeThicknessID(int? ProductAttributeID)
        {
            return uow.ProductThicknessRepo.GetWithAttributeThicknessID(ProductAttributeID);
        }

        // POST: api/productThickness
        [HttpPost]
        public IActionResult Post([FromBody] ProductThickness model)
        {
            try
            {
                uow.ProductThicknessRepo.Add(model);
                uow.SaveChanges();
                return Created("/api/productThickness", model);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/productThickness/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] ProductThickness model)
        {
            try
            {
                uow.ProductThicknessRepo.Update(model);
                uow.SaveChanges();
                return Created("/api/productThickness", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/productThickness/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                uow.ProductThicknessRepo.DeleteByID(id);
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