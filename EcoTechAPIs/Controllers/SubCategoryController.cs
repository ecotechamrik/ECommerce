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
    public class SubCategoryController : ControllerBase
    {
        IUnitOfWork uow;
        public SubCategoryController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // GET: api/subcategory
        [HttpGet]
        public IEnumerable<SubCategoryViewModel> Get()
        {
            return GetSubCategoryDetails(null);
        }

        // GET: api/subcategory/5
        [HttpGet("{id}", Name = "GetByID")]
        public IEnumerable<SubCategoryViewModel> Get(int id)
        {
            return GetSubCategoryDetails(id);
        }

        // GET: api/getbycategoryid/5
        [Route("{getbycategoryid}/{id}")]
        public IEnumerable<SubCategoryViewModel> GetByCategoryID(int id)
        {
            return uow.SubCategoryRepo.GetSubCategoryWithCategories().Where(c => c.CategoryID == id).OrderBy(o => o.SubCategoryName);
        }

        #region [ Show all or Selected Sub Category Information ]
        /// <summary>
        /// Show all or Selected Sub Category Information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<SubCategoryViewModel> GetSubCategoryDetails(int? id)
        {
            return uow.SubCategoryRepo.GetSubCategoryWithCategories().Where(c => (id != null ? c.SubCategoryID == id : c.SubCategoryID == c.SubCategoryID)).OrderBy(o => o.SubCategoryName);
        }
        #endregion

        // POST: api/subcategory
        [HttpPost]
        public IActionResult Post([FromBody] SubCategory model)
        {
            try
            {
                uow.SubCategoryRepo.Add(model);
                uow.SaveChanges();
                return Created("/api/subcategory", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // PUT: api/subcategory/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SubCategory model)
        {
            try
            {
                uow.SubCategoryRepo.Update(model);
                uow.SaveChanges();
                return Created("/api/subcategory", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/subcategory/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                uow.SubCategoryRepo.DeleteByID(id);
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