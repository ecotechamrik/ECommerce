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
    public class ProductSizeAndPriceController : ControllerBase
    {
        IUnitOfWork uow;
        public ProductSizeAndPriceController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // GET: api/ProductSizeAndPrice
        [HttpGet]
        public IEnumerable<ProductSizeAndPrice> Get()
        {
            return GetProductSizeAndPrices(null);
        }

        // GET: api/ProductSizeAndPrice/5
        [HttpGet("{id}", Name = "ProductSizeAndPrice")]
        public IEnumerable<ProductSizeAndPrice> Get(int id)
        {
            return GetProductSizeAndPrices(id);
        }

        #region [ Show all or Selected Product Attributes ]
        /// <summary>
        /// Show all or selected Product Attributes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<ProductSizeAndPrice> GetProductSizeAndPrices(int? id)
        {
            return uow.ProductSizeAndPriceRepo.GetAll().Where(c => (id != null ? c.ProductSizeAndPriceID == id : c.ProductSizeAndPriceID == c.ProductSizeAndPriceID));
        }
        #endregion

        // GET: api/GetByProductAttributeID/1
        [Route("{GetByProductAttributeID}/{pid}")]
        public IEnumerable<ProductSizeAndPriceViewModel> GetByProductAttributeID(int? pid)
        {
            return uow.ProductSizeAndPriceRepo.GetByProductAttributeID(pid);
        }

        // GET: api/GetByProductAttributeID/1
        [Route("{GetByProductWidthID}/{ProductAttributeThicknessID}/{ProductWidthID}")]
        public IEnumerable<ProductSizeAndPriceViewModel> GetByProductWidthID(int ProductAttributeThicknessID, int ProductWidthID)
        {
            return uow.ProductSizeAndPriceRepo.GetByProductWidthID(ProductAttributeThicknessID, ProductWidthID);
        }

        // POST: api/ProductSizeAndPrice
        [HttpPost]
        public IActionResult Post([FromBody] ProductSizeAndPrice model)
        {
            try
            {
                uow.ProductSizeAndPriceRepo.Add(model);
                uow.SaveChanges();
                return Created("/api/ProductSizeAndPrice", model);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/ProductSizeAndPrice/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductSizeAndPrice model)
        {
            try
            {
                uow.ProductSizeAndPriceRepo.Update(model);
                uow.SaveChanges();
                return Created("/api/ProductSizeAndPrice", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/ProductSizeAndPrice/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                uow.ProductSizeAndPriceRepo.DeleteByID(id);
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