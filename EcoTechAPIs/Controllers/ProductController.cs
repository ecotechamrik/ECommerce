using BAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Collections.Generic;
using System.Linq;

namespace EcoTechAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IUnitOfWork db;
        public ProductController(IUnitOfWork _db)
        {
            db = _db;
        }

        [HttpGet]
        public IList<ProductViewModel> Get()
        {
            

            db.ProductRepo.DbInitialize();

            var products = db.ProductRepo.GetProductsWithCategories();
            return products.ToList();
        }
    }
}