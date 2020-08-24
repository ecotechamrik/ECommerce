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
    public class ProductGradeController : ControllerBase
    {
        IUnitOfWork uow;
        public ProductGradeController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // GET: api/productgrade
        [HttpGet]
        public IEnumerable<ProductGrade> Get()
        {
            return GetProductGradeDetails(null);
        }

        // GET: api/productgrade/5
        [HttpGet("{id}", Name = "ProductGrade")]
        public IEnumerable<ProductGrade> Get(int id)
        {
            return GetProductGradeDetails(id);
        }

        #region [ Show all or Selected Product Grade information ]
        /// <summary>
        /// Show all or selected Product Grade information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<ProductGrade> GetProductGradeDetails(int? id)
        {
            return uow.ProductGradeRepo.GetAll().Where(c => (id != null ? c.ProductGradeID == id : c.ProductGradeID == c.ProductGradeID)).OrderBy(o => o.ProductGradeName);
        }
        #endregion

        // POST: api/productgrade
        [HttpPost]
        public IActionResult Post([FromBody] ProductGrade model)
        {
            try
            {
                uow.ProductGradeRepo.Add(model);
                uow.SaveChanges();
                return Created("/api/productgrade", model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/productgrade/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductGrade model)
        {
            try
            {
                uow.ProductGradeRepo.Update(model);
                uow.SaveChanges();
                return Created("/api/productgrade", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/productgrade/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                uow.ProductGradeRepo.DeleteByID(id);
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