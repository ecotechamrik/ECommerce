using BAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using BAL.ViewModels.Product;

namespace EcoTechAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAttributeController : ControllerBase
    {
        IUnitOfWork uow;
        public ProductAttributeController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // GET: api/ProductAttribute
        [HttpGet]
        public IEnumerable<ProductAttribute> Get()
        {
            return GetProductAttributeDetails(null);
        }

        // GET: api/ProductAttribute/5
        [HttpGet("{id}", Name = "ProductAttribute")]
        public IEnumerable<ProductAttribute> Get(int id)
        {
            return GetProductAttributeDetails(id);
        }

        // GET: api/GetByProductID/5
        [Route("GetByProductID/{id}")]
        public IEnumerable<ProductAttributeViewModel> GetByProductID(int id)
        {
            return uow.ProductAttributeRepo.GetByProductID(id);
        }

        // GET: api/GetProductAttrWithDoorName/5
        [Route("GetProductAttrWithDoorName/{id}")]
        public ProductAttributeViewModel GetProductAttrWithDoorName(int id)
        {
            return uow.ProductAttributeRepo.GetProductAttrWithDoorName(id);
        }

        #region [ Show all or Selected Product Attribute information ]
        /// <summary>
        /// Show all or selected Product Attribute information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<ProductAttribute> GetProductAttributeDetails(int? id)
        {
            return uow.ProductAttributeRepo.GetAll().Where(c => (id != null ? c.ProductAttributeID == id : c.ProductAttributeID == c.ProductAttributeID));
        }
        #endregion

        // POST: api/ProductAttribute
        [HttpPost]
        public ProductAttribute Post([FromBody] ProductAttribute model)
        {
            try
            {
                uow.ProductAttributeRepo.Add(model);
                uow.SaveChanges();
                return model;
            }
            catch
            {
                return null;
            }
        }

        // PUT: api/ProductAttribute/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductAttribute model)
        {
            try
            {
                uow.ProductAttributeRepo.Update(model);
                uow.SaveChanges();
                return Created("/api/ProductAttribute", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/ProductAttribute/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                uow.ProductAttributeRepo.DeleteByID(id);
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