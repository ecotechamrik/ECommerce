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
    public class ProductController : ControllerBase
    {
        IUnitOfWork uow;
        public ProductController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // GET: api/product
        [HttpGet]
        public IList<ProductViewModel> Get()
        {


            uow.ProductRepo.DbInitialize();

            var products = uow.ProductRepo.GetProductsWithCategories();
            return products.ToList();
        }

        //// GET: api/product
        //[HttpGet]
        //public IEnumerable<Product> Get()
        //{
        //    return GetProductDetails(null);
        //}

        // GET: api/product/5
        [HttpGet("{id}", Name = "Product")]
        public IEnumerable<Product> Get(int id)
        {
            return GetProductDetails(id);
        }

        #region [ Show all or Selected Door Type information ]
        /// <summary>
        /// Show all or selected Door Type information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Product> GetProductDetails(int? id)
        {
            return uow.ProductRepo.GetAll().Where(c => (id != null ? c.ProductID == id : c.ProductID == c.ProductID));
        }
        #endregion

        // POST: api/product
        [HttpPost]
        public IActionResult Post([FromBody] Product model)
        {
            try
            {
                uow.ProductRepo.Add(model);
                uow.SaveChanges();
                return Created("/api/product", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // PUT: api/product/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product model)
        {
            try
            {
                uow.ProductRepo.Update(model);
                uow.SaveChanges();
                return Created("/api/product", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/product/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                uow.ProductRepo.DeleteByID(id);
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