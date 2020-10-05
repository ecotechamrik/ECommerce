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
    public class ProductDesignController : ControllerBase
    {
        IUnitOfWork uow;
        public ProductDesignController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // GET: api/productdesign
        [HttpGet]
        public IEnumerable<ProductDesign> Get()
        {
            return GetProductDesignDetails(null);
        }

        // GET: api/productdesign/5
        [HttpGet("{id}", Name = "ProductDesign")]
        public IEnumerable<ProductDesign> Get(int id)
        {
            return GetProductDesignDetails(id);
        }

        #region [ Show all or Selected Product Design information ]
        /// <summary>
        /// Show all or selected Product Design information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<ProductDesign> GetProductDesignDetails(int? id)
        {
            return uow.ProductDesignRepo.GetAll().Where(c => (id != null ? c.ProductDesignID == id : c.ProductDesignID == c.ProductDesignID)).OrderBy(o => o.ProductDesignName);
        }
        #endregion

        // POST: api/productdesign
        [HttpPost]
        public IActionResult Post([FromBody] ProductDesign model)
        {
            try
            {
                uow.ProductDesignRepo.Add(model);
                uow.SaveChanges();
                return Created("/api/productdesign", model);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/productdesign/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] ProductDesign model)
        {
            try
            {
                uow.ProductDesignRepo.Update(model);
                uow.SaveChanges();
                return Created("/api/productdesign", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/productdesign/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                uow.ProductDesignRepo.DeleteByID(id);
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