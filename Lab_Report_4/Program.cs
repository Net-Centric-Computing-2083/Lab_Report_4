using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

// Task 4: Custom Attribute


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
    
    // Task 5: Async/Await
    

    static async Task LoadProductsAsync()
    {
        Console.WriteLine("\nLoading product data...");
        await Task.Delay(3000); // Simulate loading time
        Console.WriteLine("Product data loaded successfully.");
    }

    static async Task Main(string[] args)
    {
        Console.WriteLine("===== PRODUCT INVENTORY MANAGEMENT SYSTEM =====");

        
        // Task 1: Store Products in Collection
        

        List<Product> products = new List<Product>()
        {
            new Product(101,"Laptop","Electronics",90000),
            new Product(102,"Mobile","Electronics",35000),
            new Product(103,"Chair","Furniture",5000),
            new Product(104,"Table","Furniture",8000),
            new Product(105,"Mouse","Electronics",1500)
        };

        
        // Task 1: LINQ - Display All Products
        

        Console.WriteLine("\nAll Products:");

        foreach (var p in products)
        {
            Console.WriteLine($"{p.Id} {p.Name} {p.Category} Rs.{p.Price}");
        }

        
        // Task 1: Filter by Price
        

        Console.WriteLine("\nProducts with Price > 10000:");

        var expensiveProducts =
            from p in products
            where p.Price > 10000
            select p;

        foreach (var p in expensiveProducts)
        {
            Console.WriteLine($"{p.Name} - Rs.{p.Price}");
        }

        
        // Task 1: Filter by Category
        

        Console.WriteLine("\nElectronics Products:");

        var electronics =
            from p in products
            where p.Category == "Electronics"
            select p;

        foreach (var p in electronics)
        {
            Console.WriteLine(p.Name);
        }

        
        // Task 1: Sort by Name
        

        Console.WriteLine("\nProducts Sorted by Name:");

        var sortByName =
            from p in products
            orderby p.Name
            select p;

        foreach (var p in sortByName)
        {
            Console.WriteLine(p.Name);
        }

        
        // Task 1: Select Specific Fields
        

        Console.WriteLine("\nProduct Name and Price:");

        var selected =
            from p in products
            select new
            {
                p.Name,
                p.Price
            };

        foreach (var p in selected)
        {
            Console.WriteLine($"{p.Name} - Rs.{p.Price}");
        }

        
        // Task 2: Lambda Expressions
        

        Console.WriteLine("\nLambda Expression :");

        // Where()
        var priceFilter = products.Where(p => p.Price > 5000);

        Console.WriteLine("\nWhere():");

        foreach (var p in priceFilter)
        {
            Console.WriteLine(p.Name);
        }

        // Select()
        var names = products.Select(p => p.Name);

        Console.WriteLine("\nSelect():");

        foreach (var name in names)
        {
            Console.WriteLine(name);
        }

        // OrderBy()
        var ordered = products.OrderBy(p => p.Price);

        Console.WriteLine("\nOrderBy():");

        foreach (var p in ordered)
        {
            Console.WriteLine($"{p.Name} - {p.Price}");
        }

        // FirstOrDefault()
        var first = products.FirstOrDefault(p => p.Category == "Furniture");

        Console.WriteLine("\nFirstOrDefault():");

        if (first != null)
        {
            Console.WriteLine(first.Name);
        }

        
        // Task 3: Exception Handling
        

        Console.WriteLine("\nEnter New Product Details");

        try
        {
            Console.Write("Product ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Product Name: ");
            string name = Console.ReadLine();

            Console.Write("Category: ");
            string category = Console.ReadLine();

            Console.Write("Price: ");
            double price = Convert.ToDouble(Console.ReadLine());

            Product newProduct = new Product(id, name, category, price);

            products.Add(newProduct);

            Console.WriteLine("Product Added Successfully.");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid Input! Please enter correct data.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.WriteLine("Input Process Completed.");
        }

        
        // Task 4: Reflection
        

        Console.WriteLine("\nAttribute Information:");

        Type type = typeof(Product);

        ProductInfoAttribute attribute =
            (ProductInfoAttribute)Attribute.GetCustomAttribute(
                type,
                typeof(ProductInfoAttribute));

        if (attribute != null)
        {

            Console.WriteLine(attribute.Description);
        }

        
        // Task 5: Async/Await
        

        Console.WriteLine("\nBefore Loading...");
await LoadProductsAsync();
Console.WriteLine("After Loading.");

Console.WriteLine("\nProgram Executed Successfully.");

Console.ReadKey();
    }
}