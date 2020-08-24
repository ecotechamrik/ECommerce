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
    public class CategoryController : ControllerBase
    {
        IUnitOfWork uow;
        public CategoryController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // GET: api/category
        [HttpGet]
        public IEnumerable<CategoryViewModel> Get()
        {
            return GetCategoryDetails(null);
        }

        // GET: api/category/5
        [HttpGet("{id}", Name = "Category")]
        public IEnumerable<CategoryViewModel> Get(int id)
        {
            return GetCategoryDetails(id);
        }

        // GET: api/getbysectionid/5
        [Route("{getbysectionid}/{id}")]
        public IEnumerable<CategoryViewModel> GetBySectionID(int id)
        {
            return uow.CategoryRepo.GetCategoryWithSections().Where(c => c.SectionID == id).OrderBy(d => d.CategoryName);
        }

        #region [ Show all or Selected Category information ]
        /// <summary>
        /// Show all or selected category information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<CategoryViewModel> GetCategoryDetails(int? id)
        {
            return uow.CategoryRepo.GetCategoryWithSections().Where(c => (id != null ? c.CategoryID == id : c.CategoryID == c.CategoryID)).OrderBy(d => d.CategoryName);
        }
        #endregion

        // POST: api/category
        [HttpPost]
        public IActionResult Post([FromBody] Category model)
        {
            try
            {
                uow.CategoryRepo.Add(model);
                uow.SaveChanges();
                return Created("/api/category", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // PUT: api/category/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category model)
        {
            try
            {
                uow.CategoryRepo.Update(model);
                uow.SaveChanges();
                return Created("/api/category", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/category/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                uow.CategoryRepo.DeleteByID(id);
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