using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProductInventoryManagement
{
    // Custom Attribute
    [AttributeUsage(AttributeTargets.Class)]
    class ProductInfoAttribute : Attribute
    {
        public string Developer { get; set; }
        public string Version { get; set; }

        public ProductInfoAttribute(string developer, string version)
        {
            Developer = developer;
            Version = version;
        }
    }
    // Product Class
    [ProductInfo("Sumit Neupane", "1.0")]
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }

        public Product(int id, string name, string category, double price)
        {
            Id = id;
            Name = name;
            Category = category;
            Price = price;
        }
    }

    class Program
    {
        // Async Method
        static async Task LoadProductsAsync()
        {
            Console.WriteLine("\nLoading product data...");
            await Task.Delay(3000);
            Console.WriteLine("Product data loaded successfully.\n");
        }

        static async Task Main(string[] args)
        {
            List<Product> products = new List<Product>()
            {
                new Product(1,"Laptop","Electronics",85000),
                new Product(2,"Mouse","Electronics",1200),
                new Product(3,"Chair","Furniture",4500),
                new Product(4,"Table","Furniture",8000),
                new Product(5,"Keyboard","Electronics",2500)
            };

            Console.WriteLine("Before Loading...");
            await LoadProductsAsync();
            Console.WriteLine("After Loading...\n");

            // LINQ Queries

            Console.WriteLine("All Products");
            foreach (var p in products)
                Console.WriteLine($"{p.Id} {p.Name} {p.Category} Rs.{p.Price}");

            Console.WriteLine("\nProducts Price > 5000");
            var expensive = products.Where(p => p.Price > 5000);

            foreach (var p in expensive)
                Console.WriteLine($"{p.Name} Rs.{p.Price}");

            Console.WriteLine("\nSorted By Name");
            var sortedName = products.OrderBy(p => p.Name);

            foreach (var p in sortedName)
                Console.WriteLine(p.Name);

            Console.WriteLine("\nSelected Fields");
            var selected = products.Select(p => new
            {
                p.Name,
                p.Price
            });

            foreach (var p in selected)
                Console.WriteLine($"{p.Name} - Rs.{p.Price}");

            
            // Lambda Expressions
            

            Console.WriteLine("\nFirst Furniture Product");

            var firstFurniture = products
                .Where(x => x.Category == "Furniture")
                .FirstOrDefault();

            if (firstFurniture != null)
                Console.WriteLine(firstFurniture.Name);

            
            // Exception Handling
           
            try
            {
                Console.WriteLine("\nEnter New Product");

                Console.Write("ID: ");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("Category: ");
                string category = Console.ReadLine();

                Console.Write("Price: ");
                double price = Convert.ToDouble(Console.ReadLine());

                products.Add(new Product(id, name, category, price));

                Console.WriteLine("Product Added Successfully.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Input! Enter correct numeric values.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Input Process Completed.");
            }

            
            // Reflection (Attribute)
           
            Console.WriteLine("\nProduct Class Metadata");

            Type type = typeof(Product);

            ProductInfoAttribute attribute =
                (ProductInfoAttribute)Attribute.GetCustomAttribute(type, typeof(ProductInfoAttribute));

            if (attribute != null)
            {
                Console.WriteLine("Developer : " + attribute.Developer);
                Console.WriteLine("Version   : " + attribute.Version);
            }

            Console.WriteLine("\nFinal Product List");

            foreach (var p in products)
                Console.WriteLine($"{p.Id} {p.Name} {p.Category} Rs.{p.Price}");

            Console.ReadKey();
        }
    }
}