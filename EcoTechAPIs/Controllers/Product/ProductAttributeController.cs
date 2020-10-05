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

        // GET: api/GetAttributesByID/5
        [Route("GetAttributesByID/{id}")]
        public IEnumerable<ProductAttributeViewModel> GetAttributesByID(int id)
        {
            return uow.ProductAttributeRepo.GetAttributesByID(id);
        }

        // GET: api/GetAttributesByProductID/5
        [Route("GetAttributesByProductID/{id}")]
        public IEnumerable<ProductAttributeViewModel> GetAttributesByProductID(int id)
        {
            return uow.ProductAttributeRepo.GetAttributesByProductID(id);
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
            catch (Exception ex)
            {
                return null;
            }
        }

        // PUT: api/ProductAttribute/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] ProductAttribute model)
        {
            try
            {
                uow.ProductAttributeRepo.UpdateProductAttribute(model);
                uow.SaveChanges();
                return Created("/api/ProductAttribute", model);
            }
            catch(Exception ex)
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
            catch (Exception ex)
            {

                return StatusCode(500);
            }
        }
    }
}