using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

[AttributeUsage(AttributeTargets.Class)]
class ProductInfoAttribute : Attribute
{
    public string Info;
    public ProductInfoAttribute(string info) => Info = info;
}

[ProductInfo("Product Inventory Class")]
class Product
{
    public int Id;
    public string Name = "";
    public string Category = "";
    public double Price;
}

class Program
{
    static async Task Main()
    {
        List<Product> products = new()
        {
            new Product{Id=1, Name="Laptop", Category="Electronics", Price=700},
            new Product{Id=2, Name="Book", Category="Education", Price=20},
            new Product{Id=3, Name="Phone", Category="Electronics", Price=500}
        };

        // LINQ Queries
        Console.WriteLine("All Products:");
        foreach (var p in products)
            Console.WriteLine($"{p.Name} - {p.Price}");

        Console.WriteLine("\nPrice > 100:");
        products.Where(p => p.Price > 100)
                .ToList()
                .ForEach(p => Console.WriteLine(p.Name));

        Console.WriteLine("\nSorted:");
        products.OrderBy(p => p.Name)
                .ToList()
                .ForEach(p => Console.WriteLine(p.Name));

        // Exception Handling
        try
        {
            Console.Write("\nEnter Product Price: ");
            int price = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Price: " + price);
        }
        catch
        {
            Console.WriteLine("Invalid input!");
        }
        finally
        {
            Console.WriteLine("Done.");
        }

        // Attribute
        var attr = (ProductInfoAttribute)Attribute.GetCustomAttribute(
            typeof(Product), typeof(ProductInfoAttribute))!;
        Console.WriteLine("\nAttribute: " + attr.Info);

        // Async/Await
        Console.WriteLine("\nLoading products...");
        await LoadData();
        Console.WriteLine("Finished!");
    }

    static async Task LoadData()
    {
        await Task.Delay(2000);
        Console.WriteLine("Products Loaded.");
    }
}