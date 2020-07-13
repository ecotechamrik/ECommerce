using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace EcoTechAPIs.Models
{
    public class Product
    {
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public string Color { get; set; }
    }
}
