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
    public class ProductSupplierController : ControllerBase
    {
        IUnitOfWork uow;
        public ProductSupplierController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // GET: api/ProductSupplier
        [HttpGet]
        public IEnumerable<ProductSupplier> Get()
        {
            return GetProductSuppliers(null);
        }

        // GET: api/ProductSupplier/5
        [HttpGet("{id}", Name = "ProductSupplier")]
        public IEnumerable<ProductSupplier> Get(int id)
        {
            return GetProductSuppliers(id);
        }

        #region [ Show all or Selected Product Attributes ]
        /// <summary>
        /// Show all or selected Product Attributes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<ProductSupplier> GetProductSuppliers(int? id)
        {
            return uow.ProductSupplierRepo.GetAll().Where(c => (id != null ? c.ProductSupplierID == id : c.ProductSupplierID == c.ProductSupplierID));
        }
        #endregion

        // GET: api/GetByProductSizeAndPriceID/1
        [Route("{GetByProductSizeAndPriceID}/{ProductSizeAndPriceID}")]
        public IEnumerable<ProductSupplierViewModel> GetByProductSizeAndPriceID(int ProductSizeAndPriceID)
        {
            return uow.ProductSupplierRepo.GetByProductSizeAndPriceID(ProductSizeAndPriceID);
        }

        // POST: api/ProductSupplier
        [HttpPost]
        public IActionResult Post([FromBody] ProductSupplier model)
        {
            try
            {
                uow.ProductSupplierRepo.Add(model);
                uow.SaveChanges();
                return Created("/api/ProductSupplier", model);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/ProductSupplier/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] ProductSupplier model)
        {
            try
            {
                uow.ProductSupplierRepo.Update(model);
                uow.SaveChanges();
                return Created("/api/ProductSupplier", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/ProductSupplier/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                uow.ProductSupplierRepo.DeleteByID(id);
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