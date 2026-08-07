using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

[AttributeUsage(AttributeTargets.Class)]
class ProductInfoAttribute : Attribute
{
    public string Author { get; }

    public ProductInfoAttribute(string author)
    {
        Author = author;
    }
}

[ProductInfo("BS.CSIT Student")]
class Product
{
    public int Id;
    public string Name = "";
    public string Category = "";
    public double Price;
}

class Program
{
    static async Task LoadData()
    {
        Console.WriteLine("Loading product data...");
        await Task.Delay(3000);
        Console.WriteLine("Products loaded successfully.");
    }

    static async Task Main(string[] args)
    {
        List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 70000 },
            new Product { Id = 2, Name = "Mouse", Category = "Electronics", Price = 1000 },
            new Product { Id = 3, Name = "Book", Category = "Education", Price = 500 },
            new Product { Id = 4, Name = "Bag", Category = "Accessories", Price = 1500 },
            new Product { Id = 5, Name = "Keyboard", Category = "Electronics", Price = 2500 }
        };

        // ==========================
        // Task 1: LINQ Queries
        // ==========================

        Console.WriteLine("===== All Products =====");

        foreach (Product p in products)
        {
            Console.WriteLine($"{p.Id}\t{p.Name}\t{p.Category}\tRs.{p.Price}");
        }

        Console.WriteLine("\n===== Filter: Price > 1000 =====");

        var expensiveProducts = products.Where(p => p.Price > 1000);

        foreach (var p in expensiveProducts)
        {
            Console.WriteLine($"{p.Name} - Rs.{p.Price}");
        }

        Console.WriteLine("\n===== Filter: Electronics =====");

        var electronics = products.Where(p => p.Category == "Electronics");

        foreach (var p in electronics)
        {
            Console.WriteLine($"{p.Name}");
        }

        Console.WriteLine("\n===== Sorted By Name =====");

        var sorted = products.OrderBy(p => p.Name);

        foreach (var p in sorted)
        {
            Console.WriteLine($"{p.Name} - Rs.{p.Price}");
        }

        Console.WriteLine("\n===== Select Specific Fields =====");

        var selected = products.Select(p => new
        {
            p.Name,
            p.Price
        });

        foreach (var p in selected)
        {
            Console.WriteLine($"{p.Name} - Rs.{p.Price}");
        }

        // ==========================
        // Task 2: Lambda Expressions
        // ==========================

        Console.WriteLine("\n===== First Product Price > 1000 =====");

        var first = products.FirstOrDefault(p => p.Price > 1000);

        if (first != null)
        {
            Console.WriteLine($"{first.Name} - Rs.{first.Price}");
        }

        // ==========================
        // Task 3: Exception Handling
        // ==========================

        Console.WriteLine("\n===== Exception Handling =====");

        try
        {
            Console.Write("Enter Product ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter Category: ");
            string category = Console.ReadLine() ?? "";

            Console.Write("Enter Price: ");
            double price = Convert.ToDouble(Console.ReadLine());

            Product newProduct = new Product
            {
                Id = id,
                Name = name,
                Category = category,
                Price = price
            };

            Console.WriteLine("\nProduct Entered Successfully");
            Console.WriteLine($"{newProduct.Id} {newProduct.Name} {newProduct.Category} Rs.{newProduct.Price}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input! Please enter numeric values for ID and Price.");
        }
        finally
        {
            Console.WriteLine("Exception Handling Completed.");
        }

        // ==========================
        // Task 4: Attributes
        // ==========================

        Console.WriteLine("\n===== Product Attribute =====");

        Type type = typeof(Product);

        ProductInfoAttribute? attribute =
            (ProductInfoAttribute?)Attribute.GetCustomAttribute(
                type,
                typeof(ProductInfoAttribute));

        if (attribute != null)
        {
            Console.WriteLine("Author: " + attribute.Author);
        }

        // ==========================
        // Task 5: Async/Await
        // ==========================

        Console.WriteLine("\nBefore Loading");

        await LoadData();

        Console.WriteLine("After Loading");
    }
}