using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Lab4_LINQ_ModernCSharp
{
    //==========================
    // Custom Attribute
    //==========================
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

    //==========================
    // Product Class
    //==========================
    [ProductInfo("Saurav Basnet", "1.0")]
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
        //==========================
        // Async Method
        //==========================
        static async Task LoadProductsAsync()
        {
            Console.WriteLine("Loading products...");
            await Task.Delay(3000);
            Console.WriteLine("Products loaded successfully.\n");
        }

        static async Task Main(string[] args)
        {
            //==========================
            // Async/Await
            //==========================
            Console.WriteLine("Application Started.\n");

            await LoadProductsAsync();

            //==========================
            // Product Collection
            //==========================
            List<Product> products = new List<Product>()
            {
                new Product(1,"Laptop","Electronics",85000),
                new Product(2,"Mouse","Electronics",1200),
                new Product(3,"Keyboard","Electronics",2500),
                new Product(4,"Chair","Furniture",5500),
                new Product(5,"Table","Furniture",9000)
            };

            //==========================
            // Task 1 : LINQ Queries
            //==========================
            Console.WriteLine("===== ALL PRODUCTS =====");

            foreach (var p in products)
            {
                Console.WriteLine($"{p.Id}  {p.Name}  {p.Category}  Rs.{p.Price}");
            }

            Console.WriteLine("\n===== PRODUCTS PRICE > 5000 =====");

            var expensiveProducts =
                from p in products
                where p.Price > 5000
                select p;

            foreach (var p in expensiveProducts)
            {
                Console.WriteLine($"{p.Name} - Rs.{p.Price}");
            }

            Console.WriteLine("\n===== SORTED BY NAME =====");

            var sortedProducts =
                from p in products
                orderby p.Name
                select p;

            foreach (var p in sortedProducts)
            {
                Console.WriteLine($"{p.Name} - Rs.{p.Price}");
            }

            Console.WriteLine("\n===== SELECT NAME AND PRICE =====");

            var selectedFields =
                from p in products
                select new
                {
                    p.Name,
                    p.Price
                };

            foreach (var item in selectedFields)
            {
                Console.WriteLine($"{item.Name} - Rs.{item.Price}");
            }

            //==========================
            // Task 2 : Lambda Expressions
            //==========================
            Console.WriteLine("\n===== LAMBDA EXPRESSIONS =====");

            var electronics = products
                                .Where(p => p.Category == "Electronics");

            Console.WriteLine("\nElectronics Products:");

            foreach (var p in electronics)
            {
                Console.WriteLine(p.Name);
            }

            var names = products
                        .Select(p => p.Name);

            Console.WriteLine("\nOnly Product Names:");

            foreach (var name in names)
            {
                Console.WriteLine(name);
            }

            var sortedByPrice = products
                                .OrderBy(p => p.Price);

            Console.WriteLine("\nSorted by Price:");

            foreach (var p in sortedByPrice)
            {
                Console.WriteLine($"{p.Name} - Rs.{p.Price}");
            }

            var firstProduct =
                products.FirstOrDefault(p => p.Price > 80000);

            Console.WriteLine("\nFirst Product Price > 80000");

            if (firstProduct != null)
                Console.WriteLine(firstProduct.Name);

            //==========================
            // Task 3 : Exception Handling
            //==========================
            Console.WriteLine("\n===== ADD NEW PRODUCT =====");

            try
            {
                Console.Write("Enter Product ID: ");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Product Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter Category: ");
                string category = Console.ReadLine();

                Console.Write("Enter Price: ");
                double price = Convert.ToDouble(Console.ReadLine());

                products.Add(new Product(id, name, category, price));

                Console.WriteLine("\nProduct Added Successfully.");
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
                Console.WriteLine("Input operation completed.");
            }

            //==========================
            // Task 4 : Attributes
            //==========================
            Console.WriteLine("\n===== PRODUCT ATTRIBUTE =====");

            Type type = typeof(Product);

            ProductInfoAttribute attribute =
                (ProductInfoAttribute)Attribute.GetCustomAttribute(
                    type,
                    typeof(ProductInfoAttribute));

            if (attribute != null)
            {
                Console.WriteLine($"Developer : {attribute.Developer}");
                Console.WriteLine($"Version   : {attribute.Version}");
            }

            Console.WriteLine("\n===== UPDATED PRODUCT LIST =====");

            foreach (var p in products)
            {
                Console.WriteLine($"{p.Id} {p.Name} {p.Category} Rs.{p.Price}");
            }

            Console.WriteLine("\nProgram Finished.");
        }
    }
}