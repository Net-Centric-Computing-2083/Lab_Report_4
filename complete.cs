using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

[AttributeUsage(AttributeTargets.Class)]
class ProductInfoAttribute : Attribute
{
    public string Author;

    public ProductInfoAttribute(string author)
    {
        Author = author;
    }
}

[ProductInfo("BS.CSIT Student")]
class Product
{
    public int Id;
    public string Name;
    public string Category;
    public double Price;
}

class Program
{
    static async Task LoadData()
    {
        Console.WriteLine("\nLoading Products...");
        await Task.Delay(3000);
        Console.WriteLine("Products Loaded Successfully.");
    }

    static async Task Main(string[] args)
    {
        List<Product> products = new List<Product>()
        {
            new Product{Id=1,Name="Laptop",Category="Electronics",Price=70000},
            new Product{Id=2,Name="Mouse",Category="Electronics",Price=1000},
            new Product{Id=3,Name="Book",Category="Education",Price=500},
            new Product{Id=4,Name="Bag",Category="Accessories",Price=1500}
        };

        Console.WriteLine("All Products");

        foreach (var p in products)
        {
            Console.WriteLine(p.Name + " " + p.Price);
        }

        Console.WriteLine("\nLINQ Query");

        var result = products.Where(p => p.Price > 1000);

        foreach (var p in result)
        {
            Console.WriteLine(p.Name);
        }

        Console.WriteLine("\nSorted");

        foreach (var p in products.OrderBy(p => p.Name))
        {
            Console.WriteLine(p.Name);
        }

        Console.WriteLine("\nFirst Product");

        var first = products.FirstOrDefault(p => p.Price > 1000);

        Console.WriteLine(first.Name);

        Console.WriteLine("\nException Handling");

        try
        {
            Console.Write("Enter Price: ");
            double price = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine(price);
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid Input");
        }
        finally
        {
            Console.WriteLine("Completed");
        }

        Console.WriteLine("\nAttribute");

        Type t = typeof(Product);

        object[] attrs = t.GetCustomAttributes(false);

        foreach (ProductInfoAttribute a in attrs)
        {
            Console.WriteLine("Author: " + a.Author);
        }

        await LoadData();
    }
}