using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

// Custom Attribute
[AttributeUsage(AttributeTargets.Class)]
class ProductInfoAttribute : Attribute
{
    public string Description { get; set; }

    public ProductInfoAttribute(string description)
    {
        Description = description;
    }
}

// Product Class
[ProductInfo("This class stores product information.")]
class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }

    public override string ToString()
    {
        return $"{Id} - {Name} - {Category} - Rs.{Price}";
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        // Product Collection
        List<Product> products = new List<Product>()
        {
            new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 70000 },
            new Product { Id = 2, Name = "Mouse", Category = "Electronics", Price = 1200 },
            new Product { Id = 3, Name = "Chair", Category = "Furniture", Price = 5000 },
            new Product { Id = 4, Name = "Desk", Category = "Furniture", Price = 9000 },
            new Product { Id = 5, Name = "Keyboard", Category = "Electronics", Price = 2500 }
        };

        // =========================
        // Task 1: LINQ Queries
        // =========================

        Console.WriteLine("===== All Products =====");
        var allProducts = from p in products
                          select p;

        foreach (var p in allProducts)
            Console.WriteLine(p);

        Console.WriteLine("\n===== Products Price > 5000 =====");
        var expensiveProducts = from p in products
                                where p.Price > 5000
                                select p;

        foreach (var p in expensiveProducts)
            Console.WriteLine(p);

        Console.WriteLine("\n===== Sorted By Name =====");
        var sortedProducts = from p in products
                             orderby p.Name
                             select p;

        foreach (var p in sortedProducts)
            Console.WriteLine(p);

        Console.WriteLine("\n===== Product Names Only =====");
        var productNames = from p in products
                           select p.Name;

        foreach (var name in productNames)
            Console.WriteLine(name);

        // =========================
        // Task 2: Lambda Expressions
        // =========================

        Console.WriteLine("\n===== Lambda: Electronics Products =====");

        var electronics = products.Where(p => p.Category == "Electronics");

        foreach (var p in electronics)
            Console.WriteLine(p);

        Console.WriteLine("\n===== Lambda: Product Names =====");

        products.Select(p => p.Name)
                .ToList()
                .ForEach(Console.WriteLine);

        Console.WriteLine("\n===== Lambda: Order By Price =====");

        var ordered = products.OrderBy(p => p.Price);

        foreach (var p in ordered)
            Console.WriteLine(p);

        Console.WriteLine("\n===== Lambda: First Product =====");

        var first = products.FirstOrDefault();

        if (first != null)
            Console.WriteLine(first);

        // =========================
        // Task 3: Exception Handling
        // =========================

        try
        {
            Console.Write("\nEnter Product ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Price: ");
            double price = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("\nProduct Entered Successfully");
            Console.WriteLine($"{id} - {name} - Rs.{price}");
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
            Console.WriteLine("Input operation completed.");
        }

        // =========================
        // Task 4: Attributes
        // =========================

        Console.WriteLine("\n===== Attribute Information =====");

        Type type = typeof(Product);

        object[] attributes = type.GetCustomAttributes(false);

        foreach (ProductInfoAttribute attr in attributes)
        {
            Console.WriteLine("Description: " + attr.Description);
        }

        // =========================
        // Task 5: Async/Await
        // =========================

        await LoadProductsAsync();

        Console.WriteLine("\nProgram Finished.");

        Console.ReadKey();
    }

    static async Task LoadProductsAsync()
    {
        Console.WriteLine("\nLoading products...");
        await Task.Delay(3000); // Simulate delay
        Console.WriteLine("Products loaded successfully!");
    }
}