using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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

[ProductInfo("Rusha Pokharel", "80010.943")]
class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public double Price { get; set; }
}

class Program
{
    static async Task Main(string[] args)
    {
        List<Product> products = new List<Product>()
        {
            new Product{Id=1,Name="Laptop",Category="Electronics",Price=900},
            new Product{Id=2,Name="Phone",Category="Electronics",Price=600},
            new Product{Id=3,Name="Chair",Category="Furniture",Price=120},
            new Product{Id=4,Name="Table",Category="Furniture",Price=250},
            new Product{Id=5,Name="Mouse",Category="Electronics",Price=35}
        };

        Console.WriteLine("===== All Products =====");

        foreach (var p in products)
        {
            Console.WriteLine($"{p.Id} {p.Name} {p.Category} ${p.Price}");
        }

        Console.WriteLine("\n===== Products Price > 200 =====");

        var expensive = from p in products
                        where p.Price > 200
                        select p;

        foreach (var p in expensive)
            Console.WriteLine($"{p.Name} ${p.Price}");

        Console.WriteLine("\n===== Sorted By Name =====");

        var sorted = products.OrderBy(p => p.Name);

        foreach (var p in sorted)
            Console.WriteLine(p.Name);

        Console.WriteLine("\n===== Product Names Only =====");

        var names = products.Select(p => p.Name);

        foreach (var n in names)
            Console.WriteLine(n);

        Console.WriteLine("\n===== First Electronics Product =====");

        var first = products.FirstOrDefault(p => p.Category == "Electronics");

        if (first != null)
            Console.WriteLine(first.Name);

        Console.WriteLine("\n===== Exception Handling =====");

        try
        {
            Console.Write("Enter Product Price: ");
            double price = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Price Entered: " + price);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Invalid Input!");
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.WriteLine("Input operation completed.");
        }

        Console.WriteLine("\n===== Attribute Information =====");

        Type t = typeof(Product);

        var attribute = (ProductInfoAttribute)Attribute.GetCustomAttribute(
            t, typeof(ProductInfoAttribute));

        Console.WriteLine("Developer : " + attribute.Developer);
        Console.WriteLine("Version   : " + attribute.Version);

        Console.WriteLine("\n===== Async/Await =====");

        await LoadProductsAsync();

        Console.WriteLine("\nProgram Finished.");
    }

    static async Task LoadProductsAsync()
    {
        Console.WriteLine("Loading products...");
        await Task.Delay(13000);
        Console.WriteLine("Products Loaded Successfully.");
    }
}