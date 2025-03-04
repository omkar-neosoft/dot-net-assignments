using assignment_7.Extensions;
using assignment_7.Models;

namespace assignment_7 {
    internal class Program {
        static void Main(string[] args) {
            #region Question 1
            /**
             * 1. You are developing an Employee Management System in C#. The system
                    has an Employee class that stores employee details, but it does not have a
                    method to calculate the years of experience based on the joining date.
                    (Use DateTime struct properties to extract year from date) write a c#
                    program for the same 
             */

            Console.WriteLine("\n\n-------------- Question 1 ------------\n\n");

            Employee emp1 = new Employee("John Doe", new DateTime(2015, 6, 15));
            Console.WriteLine($"{emp1.Name} has {emp1.GetYearsOfExperience()} years of experience.");

            #endregion

            #region Question 2
            /**
             * 2. You are working on an e-commerce application that has a list of products.
                    Each product has properties: ProductID, Name, Category, and Price. use
                    LINQ to find all products in the "Electronics" category that cost more
                    than Rs1000? 
             */

            Console.WriteLine("\n\n-------------- Question 2 ------------\n\n");
            List<Product> products = new List<Product>
            {
                new Product(1, "Smartphone", "Electronics", 15000),
                new Product(2, "Laptop", "Electronics", 55000),
                new Product(3, "Table", "Furniture", 3000),
                new Product(4, "Headphones", "Electronics", 1200),
                new Product(5, "Mouse", "Electronics", 800),
                new Product(6, "T-Shirt", "Clothing", 500)
            };

            // LINQ Query to filter products
            var filteredProducts = products
                .Where(p => p.Category == "Electronics" && p.Price > 1000)
                .ToList();

            // Display the filtered products
            Console.WriteLine("Electronics products costing more than Rs.1000:");
            foreach (var product in filteredProducts) {
                Console.WriteLine($"ID: {product.ProductID}, Name: {product.Name}, Price: Rs.{product.Price}");
            }

            #endregion

            #region Question 3
            /**
             * 3. How would you use LINQ to find the most expensive product in the list?
             */

            Console.WriteLine("\n\n-------------- Question 3 ------------\n\n");

            var mostExpensiveProduct = products
                .OrderByDescending(p => p.Price)
                .FirstOrDefault();

            if (mostExpensiveProduct != null) {
                Console.WriteLine($"Most Expensive Product: {mostExpensiveProduct.Name}, Price: Rs.{mostExpensiveProduct.Price}");
            }

            #endregion
        }
    }
}
