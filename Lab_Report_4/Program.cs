using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
namespace ProductInventoryManagement
{
    [AttributeUsage(AttributeTargets.Class)]
    class ProductInfoAttribute : Attribute
    {
        public string Description { get; set; }

        public ProductInfoAttribute(string description)
        {
            Description = description;
        }
    }
    [ProductInfo("Stores product details for inventory management.")]
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            List<Product> products = new List<Product>()
            {
                new Product{Id=1, Name="Laptop", Category="Electronics", Price=80000},
                new Product{Id=2, Name="Mouse", Category="Electronics", Price=1200},
                new Product{Id=3, Name="Chair", Category="Furniture", Price=5000},
                new Product{Id=4, Name="Table", Category="Furniture", Price=9000},
                new Product{Id=5, Name="Keyboard", Category="Electronics", Price=2500}
            };
            Console.WriteLine("All Products");
            foreach (var p in products)
            {
                Console.WriteLine($"{p.Id} {p.Name} {p.Category} Rs.{p.Price}");
            }
            Console.WriteLine("\nProducts Price > 5000");
            var expensive = products.Where(p => p.Price > 5000);
            foreach (var p in expensive)
            {
                Console.WriteLine($"{p.Name} - Rs.{p.Price}");
            }
            Console.WriteLine("\nSorted by Name");
            var sorted = products.OrderBy(p => p.Name);
            foreach (var p in sorted)
            {
                Console.WriteLine($"{p.Name} - Rs.{p.Price}");
            }
            Console.WriteLine("\nSelected Fields");
            var selected = products.Select(p => new
            {
                p.Name,
                p.Price
            });
            foreach (var p in selected)
            {
                Console.WriteLine($"{p.Name} - Rs.{p.Price}");
            }
            Console.WriteLine("\nFirst Product in Electronics");
            var first = products.FirstOrDefault(p => p.Category == "Electronics");
            if (first != null)
            {
                Console.WriteLine($"{first.Name} - Rs.{first.Price}");
            }
            Console.WriteLine("\nEnter Product ID:");
            try
            {
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Product Name:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Product Price:");
                double price = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("\nProduct Entered Successfully");
                Console.WriteLine($"{id} {name} Rs.{price}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input! Please enter correct numeric values.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Input process completed.");
            }
            Console.WriteLine("\nAttribute Information");
            Type type = typeof(Product);
            ProductInfoAttribute attr =
                (ProductInfoAttribute)Attribute.GetCustomAttribute(type, typeof(ProductInfoAttribute));
            if (attr != null)
            {
                Console.WriteLine(attr.Description);
            }
            await LoadProductsAsync();
            Console.ReadKey();
        }
        static async Task LoadProductsAsync()
        {
            Console.WriteLine("\nLoading product data...");
            await Task.Delay(3000); 
            Console.WriteLine("Product data loaded successfully.");
        }
    }
}