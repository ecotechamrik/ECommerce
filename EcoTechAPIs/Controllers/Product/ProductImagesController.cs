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
    public class ProductImagesController : ControllerBase
    {
        IUnitOfWork uow;
        public ProductImagesController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // GET: api/productimages
        [HttpGet]
        public IEnumerable<ProductImageViewModel> Get()
        {
            return GetProductImagesDetails(null);
        }

        // GET: api/productimages/5
        [HttpGet("{id}", Name = "GetByProductImageID")]
        public IEnumerable<ProductImageViewModel> Get(int id)
        {
            return GetProductImagesDetails(id);
        }

        // GET: api/getimagesbyproductid/5
        [Route("{getimagesbyproductid}/{id}")]
        public IEnumerable<ProductImageViewModel> GetImagesByProductID(int id)
        {
            return uow.ProductImagesRepo.GetProductImages().Where(p => p.ProductID == id);
        }

        #region [ Show all or Selected Product Images Information ]
        /// <summary>
        /// Show all or Selected Product Images Information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<ProductImageViewModel> GetProductImagesDetails(int? id)
        {
            return uow.ProductImagesRepo.GetProductImages().Where(s => (id != null ? s.ProductImageID == id : s.ProductImageID == s.ProductImageID));
        }
        #endregion

        // POST: api/productimages
        [HttpPost]
        public IActionResult Post([FromBody] ProductImage model)
        {
            try
            {
                uow.ProductImagesRepo.Add(model);
                uow.SaveChanges();
                return Created("/api/productimages", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // PUT: api/productimages/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] ProductImage model)
        {
            try
            {
                uow.ProductImagesRepo.Update(model);
                uow.SaveChanges();
                return Created("/api/productimages", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // PUT: api/productimages/5
        [Route("SetDefaultImage/{ProductImageID}/{ProductID}")]
        public IEnumerable<ProductImageViewModel> SetDefaultImage(int ProductImageID, int ProductID)
        {
            try
            {
                IEnumerable<ProductImageViewModel> model = uow.ProductImagesRepo.SetDefaultImage(ProductImageID, ProductID);                
                return model;
            }
            catch // (Exception ex)
            {
                return null;
            }
        }

        // DELETE: api/productimages/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                uow.ProductImagesRepo.DeleteByID(id);
                uow.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        // DELETE: api/productimages/DeleteImagesByProductID/5
        [HttpDelete("{DeleteImagesByProductID}/{productid}")]
        public IActionResult DeleteImagesByProductID(int ProductID)
        {
            try
            {
                uow.ProductImagesRepo.DeleteImagesByProductID(ProductID);
                uow.SaveChanges();
                return Ok("IActionResult DeleteImagesByProductID(int ProductID)");
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        // UPDATE ORDER: api/productimages/DeleteImagesByProductID/5
        [Route("UpdateOrder/{id}/{orderno}")]
        public IActionResult UpdateOrder(int id, int orderNo)
        {
            try
            {
                uow.ProductImagesRepo.UpdateOrder(id, orderNo);
                uow.SaveChanges();
                return Ok("UpdateOrder(int id, int orderNo)");
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
    }
}