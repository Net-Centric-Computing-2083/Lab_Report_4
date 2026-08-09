using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

// Custom attribute
[AttributeUsage(AttributeTargets.Class)]
public class ProductInfoAttribute : Attribute
{
    public string Description { get; }
    public string Version { get; }

    public ProductInfoAttribute(string description, string version)
    {
        Description = description;
        Version = version;
    }
}

// Applying the custom attribute to the Product class
[ProductInfo(
    "Represents a product in the inventory management system.",
    "1.0"
)]
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }

    public Product(int id, string name, decimal price, string category)
    {
        Id = id;
        Name = name;
        Price = price;
        Category = category;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Price: Rs. {Price}, Category: {Category}";
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        // Product collection
        List<Product> products = new List<Product>
        {
            new Product(101, "Laptop", 85000, "Electronics"),
            new Product(102, "Smartphone", 45000, "Electronics"),
            new Product(103, "Office Chair", 12000, "Furniture"),
            new Product(104, "Notebook", 250, "Stationery"),
            new Product(105, "Printer", 25000, "Electronics"),
            new Product(106, "Study Table", 18000, "Furniture")
        };

        // Task 1: LINQ Queries
        Console.WriteLine("TASK 1: LINQ QUERIES");
        Console.WriteLine("====================");

        Console.WriteLine("\nAll Products:");

        IEnumerable<Product> allProducts =
            from product in products
            select product;

        foreach (Product product in allProducts)
        {
            Console.WriteLine(product);
        }

        Console.WriteLine("\nProducts with price greater than Rs. 20,000:");

        IEnumerable<Product> expensiveProducts =
            from product in products
            where product.Price > 20000
            select product;

        foreach (Product product in expensiveProducts)
        {
            Console.WriteLine(product);
        }

        Console.WriteLine("\nProducts in Electronics category:");

        IEnumerable<Product> electronicProducts =
            from product in products
            where product.Category == "Electronics"
            select product;

        foreach (Product product in electronicProducts)
        {
            Console.WriteLine(product);
        }

        Console.WriteLine("\nProducts sorted by name:");

        IEnumerable<Product> productsSortedByName =
            from product in products
            orderby product.Name
            select product;

        foreach (Product product in productsSortedByName)
        {
            Console.WriteLine(product);
        }

        Console.WriteLine("\nProducts sorted by price:");

        IEnumerable<Product> productsSortedByPrice =
            from product in products
            orderby product.Price
            select product;

        foreach (Product product in productsSortedByPrice)
        {
            Console.WriteLine(product);
        }

        Console.WriteLine("\nSelected Product Fields:");

        var selectedFields =
            from product in products
            select new
            {
                product.Name,
                product.Price
            };

        foreach (var product in selectedFields)
        {
            Console.WriteLine(
                $"Name: {product.Name}, Price: Rs. {product.Price}"
            );
        }

        // Task 2: Lambda Expressions
        Console.WriteLine("\n\nTASK 2: LAMBDA EXPRESSIONS");
        Console.WriteLine("==========================");

        Console.WriteLine("\nFurniture products using Where():");

        List<Product> furnitureProducts = products
            .Where(product => product.Category == "Furniture")
            .ToList();

        foreach (Product product in furnitureProducts)
        {
            Console.WriteLine(product);
        }

        Console.WriteLine("\nProduct names using Select():");

        List<string> productNames = products
            .Select(product => product.Name)
            .ToList();

        foreach (string name in productNames)
        {
            Console.WriteLine(name);
        }

        Console.WriteLine("\nProducts ordered by price using OrderBy():");

        List<Product> orderedProducts = products
            .OrderBy(product => product.Price)
            .ToList();

        foreach (Product product in orderedProducts)
        {
            Console.WriteLine(product);
        }

        Console.WriteLine("\nSearching for product with ID 103:");

        Product searchedProduct = products
            .FirstOrDefault(product => product.Id == 103);

        if (searchedProduct != null)
        {
            Console.WriteLine(searchedProduct);
        }
        else
        {
            Console.WriteLine("Product was not found.");
        }

        // Task 3: Exception Handling
        Console.WriteLine("\n\nTASK 3: EXCEPTION HANDLING");
        Console.WriteLine("==========================");

        try
        {
            Console.Write("Enter Product ID: ");
            int id = int.Parse(Console.ReadLine() ?? "");

            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter Product Price: ");
            decimal price = decimal.Parse(Console.ReadLine() ?? "");

            Console.Write("Enter Product Category: ");
            string category = Console.ReadLine() ?? "";

            if (id <= 0)
            {
                throw new ArgumentException(
                    "Product ID must be greater than zero."
                );
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(
                    "Product name cannot be empty."
                );
            }

            if (price < 0)
            {
                throw new ArgumentException(
                    "Product price cannot be negative."
                );
            }

            if (string.IsNullOrWhiteSpace(category))
            {
                throw new ArgumentException(
                    "Product category cannot be empty."
                );
            }

            bool idAlreadyExists = products.Any(
                product => product.Id == id
            );

            if (idAlreadyExists)
            {
                throw new ArgumentException(
                    "A product with this ID already exists."
                );
            }

            Product newProduct = new Product(
                id,
                name,
                price,
                category
            );

            products.Add(newProduct);

            Console.WriteLine("\nProduct added successfully:");
            Console.WriteLine(newProduct);
        }
        catch (FormatException)
        {
            Console.WriteLine(
                "Invalid input. Product ID and price must be numeric values."
            );
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine($"Input error: {exception.Message}");
        }
        catch (Exception exception)
        {
            Console.WriteLine(
                $"An unexpected error occurred: {exception.Message}"
            );
        }
        finally
        {
            Console.WriteLine(
                "Product input operation has been completed."
            );
        }

        // Task 4: Attributes
        Console.WriteLine("\n\nTASK 4: CUSTOM ATTRIBUTES");
        Console.WriteLine("=========================");

        Type productType = typeof(Product);

        ProductInfoAttribute attribute =
            productType.GetCustomAttribute<ProductInfoAttribute>();

        if (attribute != null)
        {
            Console.WriteLine($"Class Name: {productType.Name}");
            Console.WriteLine(
                $"Description: {attribute.Description}"
            );
            Console.WriteLine($"Version: {attribute.Version}");
        }
        else
        {
            Console.WriteLine(
                "No ProductInfoAttribute was found."
            );
        }

        // Task 5: Async/Await Programming
        Console.WriteLine("\n\nTASK 5: ASYNC/AWAIT PROGRAMMING");
        Console.WriteLine("===============================");

        Console.WriteLine(
            "Before asynchronous operation: Preparing to load products."
        );

        List<Product> loadedProducts =
            await LoadProductsAsync(products);

        Console.WriteLine(
            "After asynchronous operation: Products loaded successfully."
        );

        Console.WriteLine("\nLoaded Products:");

        foreach (Product product in loadedProducts)
        {
            Console.WriteLine(product);
        }

        Console.WriteLine(
            "\nAll Lab Report 4 tasks completed successfully."
        );
    }

    // Asynchronous method
    public static async Task<List<Product>> LoadProductsAsync(
        List<Product> products
    )
    {
        Console.WriteLine(
            "During asynchronous operation: Loading product data..."
        );

        // Simulates a time-consuming operation
        await Task.Delay(2000);

        return products;
    }
}