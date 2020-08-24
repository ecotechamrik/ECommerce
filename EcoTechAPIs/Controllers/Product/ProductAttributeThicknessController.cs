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
    public class ProductAttributeThicknessController : ControllerBase
    {
        IUnitOfWork uow;
        public ProductAttributeThicknessController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // GET: api/ProductAttributeThickness
        [HttpGet]
        public IEnumerable<ProductAttributeThickness> Get()
        {
            return uow.ProductAttributeThicknessRepo.GetAll();
        }

        // GET: api/ProductAttributeThickness/5
        [HttpGet("{id}", Name = "ProductAttributeThickness")]
        public ProductAttributeThickness Get(int id)
        {
            return uow.ProductAttributeThicknessRepo.GetByID(id);
        }

        // POST: api/ProductAttributeThickness
        [HttpPost]
        public ProductAttributeThickness Post([FromBody] ProductAttributeThickness model)
        {
            try
            {
                uow.ProductAttributeThicknessRepo.Add(model);
                uow.SaveChanges();
                return model;
            }
            catch
            {
                return null;
            }
        }

        // PUT: api/ProductAttributeThickness/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductAttributeThickness model)
        {
            try
            {
                uow.ProductAttributeThicknessRepo.Update(model);
                uow.SaveChanges();
                return Created("/api/ProductAttributeThickness", model);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/ProductAttributeThickness/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                uow.ProductAttributeThicknessRepo.DeleteByID(id);
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