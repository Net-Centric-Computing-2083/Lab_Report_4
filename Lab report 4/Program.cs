using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProductInventorySystem
{
    // Custom Attribute
    [AttributeUsage(AttributeTargets.Class)]
    public class ProductInfoAttribute : Attribute
    {
        public string Description { get; set; }

        public ProductInfoAttribute(string description)
        {
            Description = description;
        }
    }

    [ProductInfo("This class stores product information.")]
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Category: {Category}, Price: {Price}";
        }
    }

    class Program
    {
        static async Task LoadProductsAsync()
        {

            Console.WriteLine("\nLoading product data...");
            await Task.Delay(3000);
            Console.WriteLine("Product data loaded successfully.");
        }

        static async Task Main(string[] args)
        {
            List<Product> products = new List<Product>()
            {
                new Product{Id=1, Name="Laptop", Category="Electronics", Price=800},
                new Product{Id=2, Name="Mouse", Category="Electronics", Price=20},
                new Product{Id=3, Name="Table", Category="Furniture", Price=150},
                new Product{Id=4, Name="Chair", Category="Furniture", Price=100}
            };

            // LINQ Queries
            Console.WriteLine("This program is compiled by BigyanLuitel-80010922\n");
            Console.WriteLine("All Products:");
            var allProducts = from p in products
                              select p;

            foreach (var p in allProducts)
                Console.WriteLine(p);

            Console.WriteLine("\nProducts Price > 100:");
            var expensiveProducts = products.Where(p => p.Price > 100);

            foreach (var p in expensiveProducts)
                Console.WriteLine(p);

            Console.WriteLine("\nSorted by Name:");
            var sortedProducts = products.OrderBy(p => p.Name);

            foreach (var p in sortedProducts)
                Console.WriteLine(p);

            Console.WriteLine("\nSelected Fields:");
            var selectedFields = products.Select(p => new
            {
                p.Name,
                p.Price
            });

            foreach (var p in selectedFields)
                Console.WriteLine($"{p.Name} - {p.Price}");

            // Lambda Expressions
            Console.WriteLine("\nFirst Furniture Product:");

            var firstFurniture =
                products.FirstOrDefault(p => p.Category == "Furniture");

            Console.WriteLine(firstFurniture);

            // Exception Handling
            try
            {

                Console.Write("\nEnter Product Price: ");
                double price = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Price Entered: " + price);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Input! Please enter numeric value.");
            }
            finally
            {
                Console.WriteLine("Input operation completed.");
            }

            // Reflection and Attribute
            Console.WriteLine("\nAttribute Information:");

            Type type = typeof(Product);

            var attribute = (ProductInfoAttribute)
                Attribute.GetCustomAttribute(
                    type,
                    typeof(ProductInfoAttribute));

            if (attribute != null)
            {
                Console.WriteLine(attribute.Description);
            }

            // Async/Await
            Console.WriteLine("\nStarting Async Operation...");
            await LoadProductsAsync();
            Console.WriteLine("Async Operation Completed.");
        }
    }
}
