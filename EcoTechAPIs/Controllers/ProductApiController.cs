using BAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


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
                Product p = new Product { ProductID = 1, ProductName = "sold", IsActive = true };
                products.Add(p);
            }
        }

        [HttpGet]
        public List<Product> Get()
        {
            //IList<Student> studentList = new List<Student>() {
            //new Student() { StudentID = 1, StudentName = "John", Age = 18, StandardID = 1 } ,
            //new Student() { StudentID = 2, StudentName = "Steve",  Age = 21, StandardID = 1 } ,
            //new Student() { StudentID = 3, StudentName = "Bill",  Age = 18, StandardID = 2 } ,
            //new Student() { StudentID = 4, StudentName = "Ram" , Age = 20, StandardID = 2 } ,
            //new Student() { StudentID = 5, StudentName = "Ron" , Age = 21 } };

            //IList<Standard> standardList = new List<Standard>() {
            //new Standard(){ StandardID = 1, StandardName="Standard 1"},
            //new Standard(){ StandardID = 2, StandardName="Standard 2"},
            //new Standard(){ StandardID = 3, StandardName="Standard 3"} };

            //var studentsWithStandard = from stad in standardList
            //    join s in studentList
            //    on stad.StandardID equals s.StandardID
            //    into sg
            //    from std_grp in sg
            //    orderby stad.StandardName, std_grp.StudentName
            //    select new
            //    {
            //        StudentName = std_grp.StudentName,
            //        StandardName = stad.StandardName
            //    };

            //foreach (var group in studentsWithStandard)
            //{
            //    Console.WriteLine("{0} is in {1}", group.StudentName, group.StandardName);
            //}

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


    //public class Student
    //{
    //    public int StudentID { get; set; }
    //    public string StudentName { get; set; }
    //    public int Age { get; set; }
    //    public int StandardID { get; set; }
    //}

    //public class Standard
    //{
    //    public int StandardID { get; set; }
    //    public string StandardName { get; set; }
    //}
}
