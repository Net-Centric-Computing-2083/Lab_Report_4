using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

//======================
// Custom Attribute
//======================
[AttributeUsage(AttributeTargets.Class)]
class ProductInfoAttribute : Attribute
{
    public string Author { get; }
    public string Version { get; }

    public ProductInfoAttribute(string author, string version)
    {
        Author = author;
        Version = version;
    }
}

//======================
// Product Class
//======================
[ProductInfo("Sujita Dahal", "1.0")]
class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }

    public override string ToString()
    {
        return $"{Id}\t{Name}\t{Category}\tRs. {Price}";
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        //======================
        // Product Collection
        //======================
        List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 85000 },
            new Product { Id = 2, Name = "Mouse", Category = "Electronics", Price = 1200 },
            new Product { Id = 3, Name = "Chair", Category = "Furniture", Price = 5500 },
            new Product { Id = 4, Name = "Table", Category = "Furniture", Price = 9000 },
            new Product { Id = 5, Name = "Keyboard", Category = "Electronics", Price = 2500 }
        };

        //======================
        // Task 1 : LINQ Queries
        //======================
        Console.WriteLine("===== ALL PRODUCTS =====");
        foreach (var product in products)
        {
            Console.WriteLine(product);
        }

        Console.WriteLine("\n===== PRODUCTS PRICE > 5000 =====");
        var expensiveProducts =
            from p in products
            where p.Price > 5000
            select p;

        foreach (var product in expensiveProducts)
        {
            Console.WriteLine(product);
        }

        Console.WriteLine("\n===== SORT BY NAME =====");
        var sortedProducts =
            from p in products
            orderby p.Name
            select p;

        foreach (var product in sortedProducts)
        {
            Console.WriteLine(product);
        }

        Console.WriteLine("\n===== SELECT NAME AND PRICE =====");
        var selected =
            from p in products
            select new
            {
                p.Name,
                p.Price
            };

        foreach (var item in selected)
        {
            Console.WriteLine($"{item.Name} - Rs. {item.Price}");
        }

        //======================
        // Task 2 : Lambda Expressions
        //======================
        Console.WriteLine("\n===== LAMBDA EXPRESSIONS =====");

        var electronics = products.Where(p => p.Category == "Electronics");

        Console.WriteLine("\nElectronics Products:");
        foreach (var product in electronics)
        {
            Console.WriteLine(product);
        }

        var names = products.Select(p => p.Name);

        Console.WriteLine("\nProduct Names:");
        foreach (var name in names)
        {
            Console.WriteLine(name);
        }

        var orderByPrice = products.OrderBy(p => p.Price);

        Console.WriteLine("\nSorted By Price:");
        foreach (var product in orderByPrice)
        {
            Console.WriteLine(product);
        }

        var firstExpensive = products.FirstOrDefault(p => p.Price > 50000);

        Console.WriteLine("\nFirst Product Price > 50000:");
        if (firstExpensive != null)
            Console.WriteLine(firstExpensive);
        else
            Console.WriteLine("No Product Found.");

        //======================
        // Task 3 : Exception Handling
        //======================
        Console.WriteLine("\n===== ADD NEW PRODUCT =====");

        try
        {
            Product newProduct = new Product();

            Console.Write("Enter Product ID: ");
            newProduct.Id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Product Name: ");
            newProduct.Name = Console.ReadLine();

            Console.Write("Enter Category: ");
            newProduct.Category = Console.ReadLine();

            Console.Write("Enter Price: ");
            newProduct.Price = Convert.ToDouble(Console.ReadLine());

            products.Add(newProduct);

            Console.WriteLine("\nProduct Added Successfully.");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid Input! Please enter correct numeric values.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Input Operation Completed.");
        }

        //======================
        // Task 4 : Attributes
        //======================
        Console.WriteLine("\n===== PRODUCT ATTRIBUTE INFORMATION =====");

        Type type = typeof(Product);

        ProductInfoAttribute attribute =
            (ProductInfoAttribute)Attribute.GetCustomAttribute(type, typeof(ProductInfoAttribute));

        if (attribute != null)
        {
            Console.WriteLine($"Author : {attribute.Author}");
            Console.WriteLine($"Version: {attribute.Version}");
        }

        //======================
        // Task 5 : Async/Await
        //======================
        Console.WriteLine("\n===== ASYNC/AWAIT DEMO =====");

        Console.WriteLine("Before Loading Products...");
        await LoadProductsAsync();
        Console.WriteLine("After Loading Products...");

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    static async Task LoadProductsAsync()
    {
        Console.WriteLine("Loading Product Data...");
        await Task.Delay(3000);
        Console.WriteLine("Product Data Loaded Successfully.");
    }
}
