using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

[AttributeUsage(AttributeTargets.Class)]
class ProductInfoAttribute : Attribute
{
    public string Description { get; }

    public ProductInfoAttribute(string description)
    {
        Description = description;
    }
}

[ProductInfo("Inventory Product Class")]
class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Category { get; set; } = "";
    public double Price { get; set; }
}

class Program
{
    static async Task Main(string[] args)
    {
        // Task 1: Product Collection
        List<Product> products = new List<Product>()
        {
            new Product{Id=1, Name="Laptop", Category="Electronics", Price=850},
            new Product{Id=2, Name="Mouse", Category="Electronics", Price=25},
            new Product{Id=3, Name="Notebook", Category="Stationery", Price=5},
            new Product{Id=4, Name="Printer", Category="Electronics", Price=150},
            new Product{Id=5, Name="Pen", Category="Stationery", Price=2}
        };

        Console.WriteLine("All Products:");
        foreach (var p in products)
        {
            Console.WriteLine($"{p.Id} {p.Name} {p.Category} ${p.Price}");
        }

        // LINQ Query
        Console.WriteLine("\nProducts Price > 100:");
        var expensiveProducts =
            from p in products
            where p.Price > 100
            select p;

        foreach (var p in expensiveProducts)
        {
            Console.WriteLine($"{p.Name} - ${p.Price}");
        }

        Console.WriteLine("\nSorted By Name:");
        var sortedProducts =
            from p in products
            orderby p.Name
            select p;

        foreach (var p in sortedProducts)
        {
            Console.WriteLine(p.Name);
        }

        Console.WriteLine("\nSelected Fields:");
        var names =
            from p in products
            select new { p.Name, p.Price };

        foreach (var item in names)
        {
            Console.WriteLine($"{item.Name} - ${item.Price}");
        }

        // Task 2: Lambda Expressions
        Console.WriteLine("\nLambda Expressions:");

        var electronics = products.Where(p => p.Category == "Electronics");

        foreach (var p in electronics)
        {
            Console.WriteLine(p.Name);
        }

        var productNames = products.Select(p => p.Name);

        Console.WriteLine("\nProduct Names:");
        foreach (var name in productNames)
        {
            Console.WriteLine(name);
        }

        var orderByPrice = products.OrderBy(p => p.Price);

        Console.WriteLine("\nOrder By Price:");
        foreach (var p in orderByPrice)
        {
            Console.WriteLine($"{p.Name} - ${p.Price}");
        }

        var first = products.FirstOrDefault(p => p.Price > 100);

        Console.WriteLine("\nFirst Product Price >100:");
        if (first != null)
            Console.WriteLine(first.Name);

        // Task 3: Exception Handling
        Console.WriteLine("\nException Handling:");

        try
        {
            Console.Write("Enter Product ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Product Price: ");
            double price = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Product Added Successfully.");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter numeric values.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.WriteLine("Input operation completed.");
        }

        // Task 4: Attributes
        Console.WriteLine("\nAttribute Information:");

        Type type = typeof(Product);

        ProductInfoAttribute? attribute =
            (ProductInfoAttribute?)Attribute.GetCustomAttribute(type, typeof(ProductInfoAttribute));

        if (attribute != null)
        {
            Console.WriteLine(attribute.Description);
        }

        // Task 5: Async/Await
        Console.WriteLine("\nBefore Loading Products...");
        await LoadProductsAsync();
        Console.WriteLine("After Loading Products...");
    }

    static async Task LoadProductsAsync()
    {
        Console.WriteLine("Loading products...");
        await Task.Delay(3000);
        Console.WriteLine("Products Loaded Successfully.");
    }
}
