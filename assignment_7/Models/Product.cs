using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_7.Models {
    public class Product {
        public int ProductID {
            get; set;
        }
        public string Name {
            get; set;
        }
        public string Category {
            get; set;
        }
        public decimal Price {
            get; set;
        }

        public Product(int productId, string name, string category, decimal price) {
            ProductID = productId;
            Name = name;
            Category = category;
            Price = price;
        }
    }
}
