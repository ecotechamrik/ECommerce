using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace EcoTechAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        static List<Product> products = new List<Product>();


        public ProductApiController()
        {
            if (products.Count == 0)
            {
                Product p = new Product { ProductID = 1, ProductName = "sold", ProductCode = "P001", LivePrice = "200", IsActive = true };
                products.Add(p);
            }
        }

        [HttpGet]
        public List<Product> Get()
        {
            var ps = (from p in products
                      select p).ToList();

            return ps;
        }

        [HttpPost]
        public bool InsertProduct(Product p)
        {
            products.Add(p);
            return true;
        }

        [HttpPut]
        public bool UpdateProduct(Product p)
        {
            // products.Add(p);
            return true;
        }

        [HttpDelete]
        public bool DeleteProduct(int productid)
        {
            var prod = GetProductById(productid);
            products.Remove(prod);
            return true;
        }

        // GET: api/Home/5
        [HttpGet("{productid}", Name = "Product1")]
        public Product GetProductById(int productid)
        {
            var prod = (from p in products
                        where p.ProductID == productid
                        select p).FirstOrDefault();
            return prod;
        }

        [HttpGet("{productid}/{name}", Name = "Product2")]
        public Product GetProductById(int productid, string name)
        {
            var prod = (from p in products
                        where p.ProductID == productid
                        select p).FirstOrDefault();
            return prod;
        }
    }
}
