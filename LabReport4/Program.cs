using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProductInventoryManagementSystem
{
    // Task 4: Custom Attribute

    [AttributeUsage(AttributeTargets.Class)]
    class ProductInfoAttribute : Attribute
    {
        public string Developer { get; }
        public string Version { get; }

        public ProductInfoAttribute(string developer, string version)
        {
            Developer = developer;
            Version = version;
        }
    }

    // Product Class

    [ProductInfo("Binit Kumar Singh", "1.0")]
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Category: {Category}, Price: ${Price}";
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=========================================");
            Console.WriteLine(" PRODUCT INVENTORY MANAGEMENT SYSTEM");
            Console.WriteLine("=========================================");

            // Product Collection

            List<Product> products = new List<Product>
            {
                new Product{Id=1,Name="Laptop",Category="Electronics",Price=850},
                new Product{Id=2,Name="Mouse",Category="Electronics",Price=25},
                new Product{Id=3,Name="Chair",Category="Furniture",Price=120},
                new Product{Id=4,Name="Table",Category="Furniture",Price=250},
                new Product{Id=5,Name="Keyboard",Category="Electronics",Price=45}
            };

            // Task 1 : LINQ Queries

            Console.WriteLine("\n========== TASK 1 : LINQ ==========");

            Console.WriteLine("\nAll Products:");
            foreach (var p in products)
            {
                Console.WriteLine(p);
            }

            Console.WriteLine("\nProducts with Price > 100:");
            var expensiveProducts =
                from p in products
                where p.Price > 100
                select p;

            foreach (var p in expensiveProducts)
            {
                Console.WriteLine(p);
            }

            Console.WriteLine("\nProducts Sorted by Name:");

            var sortedProducts =
                from p in products
                orderby p.Name
                select p;

            foreach (var p in sortedProducts)
            {
                Console.WriteLine(p);
            }

            Console.WriteLine("\nSelected Fields:");

            var selected =
                from p in products
                select new
                {
                    p.Name,
                    p.Price
                };

            foreach (var item in selected)
            {
                Console.WriteLine(item.Name + " - $" + item.Price);
            }

            // Task 2 : Lambda Expressions

            Console.WriteLine("\n========== TASK 2 : Lambda ==========");

            var electronics =
                products.Where(p => p.Category == "Electronics");

            Console.WriteLine("\nElectronics:");

            foreach (var p in electronics)
            {
                Console.WriteLine(p);
            }

            Console.WriteLine("\nOnly Product Names:");

            var names =
                products.Select(p => p.Name);

            foreach (var n in names)
            {
                Console.WriteLine(n);
            }

            Console.WriteLine("\nOrdered by Price:");

            var ordered =
                products.OrderBy(p => p.Price);

            foreach (var p in ordered)
            {
                Console.WriteLine(p);
            }

            Console.WriteLine("\nFirst Product Over $100:");

            var first =
                products.FirstOrDefault(p => p.Price > 100);

            if (first != null)
                Console.WriteLine(first);

            // Task 3 : Exception Handling

            Console.WriteLine("\n========== TASK 3 : Exception Handling ==========");

            try
            {
                Console.Write("Enter Product ID: ");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Product Price: ");
                double price = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("\nYou Entered:");
                Console.WriteLine("Product ID: " + id);
                Console.WriteLine("Price: $" + price);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input! Please enter numeric values.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Input process completed.");
            }

            // Task 4 : Attributes

            Console.WriteLine("\n========== TASK 4 : Attributes ==========");

            Type type = typeof(Product);

            ProductInfoAttribute attribute =
                (ProductInfoAttribute)Attribute.GetCustomAttribute(
                    type,
                    typeof(ProductInfoAttribute));

            if (attribute != null)
            {
                Console.WriteLine("Developer : " + attribute.Developer);
                Console.WriteLine("Version   : " + attribute.Version);
            }

            // Task 5 : Async/Await

            Console.WriteLine("\n========== TASK 5 : Async/Await ==========");

            Console.WriteLine("Loading products...");

            await LoadProductsAsync();

            Console.WriteLine("Products loaded successfully.");

            Console.WriteLine("\n=========================================");
            Console.WriteLine("Program Completed Successfully.");
            Console.WriteLine("=========================================");

            Console.ReadKey();
        }

        static async Task LoadProductsAsync()
        {
            Console.WriteLine("Please wait...");
            await Task.Delay(3000);
            Console.WriteLine("Loading completed.");
        }
    }
}