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
    public class SubCatGalleryController : ControllerBase
    {
        static IUnitOfWork uow;
        public SubCatGalleryController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // GET: api/subcatgallery
        [HttpGet]
        public IEnumerable<SubCatGalleryViewModel> Get()
        {
            return GetSubCatGalleryDetails(null);
        }

        // GET: api/subcatgallery/5
        [HttpGet("{id}", Name = "GetBySubCatGalleryID")]
        public IEnumerable<SubCatGalleryViewModel> Get(int id)
        {
            return GetSubCatGalleryDetails(id);
        }

        // GET: api/subcatgallery/5
        [Route("{getbysubcategoryid}/{id}")]
        public IEnumerable<SubCatGalleryViewModel> GetBySubCategoryID(int id)
        {
            return uow.SubCatGalleryRepo.GetSubCatGallery().Where(s => s.SubCategoryID == id);
        }

        #region [ Show all or Selected Sub Category Gallery Information ]
        /// <summary>
        /// Show all or Selected Sub Category Gallery Information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        static IEnumerable<SubCatGalleryViewModel> GetSubCatGalleryDetails(int? id)
        {
            return uow.SubCatGalleryRepo.GetSubCatGallery().Where(s => (id != null ? s.SubCatGalleryID == id : s.SubCatGalleryID == s.SubCatGalleryID));
        }
        #endregion

        // POST: api/subcatgallery
        [HttpPost]
        public IActionResult Post([FromBody] SubCatGallery model)
        {
            try
            {
                uow.SubCatGalleryRepo.Add(model);
                uow.SaveChanges();
                return Created("/api/subcatgallery", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // PUT: api/subcatgallery/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SubCatGallery model)
        {
            try
            {
                uow.SubCatGalleryRepo.Update(model);
                uow.SaveChanges();
                return Created("/api/subcatgallery", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/subcatgallery/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                uow.SubCatGalleryRepo.DeleteByID(id);
                uow.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        // DELETE: api/DeleteBySubCategoryID/5
        [HttpDelete("{deletebysubcategoryid}/{subcategoryid}")]        
        public IActionResult DeleteBySubCategoryID(int SubCategoryID)
        {
            try
            {
                uow.SubCatGalleryRepo.DeleteBySubCategoryID(SubCategoryID);
                uow.SaveChanges();
                return Ok("IActionResult DeleteBySubCategoryID(int SubCategoryID)");
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
    }
}