using System;
using System.Collections.Generic;

class Address
{
    private string Street;
    private string City;
    private string State;
    private string Country;

    public Address(string street, string city, string state, string country)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
    }

    public bool IsInUSA()
    {
        return Country.Equals("USA", StringComparison.OrdinalIgnoreCase);
    }

    public string GetFullAddress()
    {
        return $"{Street}, {City}, {State}, {Country}";
    }
}

class Customer
{
    public string Name { get; private set; }
    private Address Address;

    public Customer(string name, Address address)
    {
        Name = name;
        Address = address;
    }

    public bool IsInUSA()
    {
        return Address.IsInUSA();
    }

    public string GetFullAddress()
    {
        return Address.GetFullAddress();
    }
    
    public string GetCustomerName()
    {
        return Name;
    }
}

class Product
{
    public string Name { get; private set; }
    public string ProductId { get; private set; }
    private decimal PricePerUnit;
    private int Quantity;

    public Product(string name, string productId, decimal pricePerUnit, int quantity)
    {
        Name = name;
        ProductId = productId;
        PricePerUnit = pricePerUnit;
        Quantity = quantity;
    }

    public decimal GetTotalCost()
    {
        return PricePerUnit * Quantity;
    }
}

class Order
{
    private List<Product> Products;
    private Customer Customer;

    public Order(List<Product> products, Customer customer)
    {
        Products = products;
        Customer = customer;
    }

    public decimal GetTotalCost()
    {
        decimal totalCost = 0;
        foreach (var product in Products)
        {
            totalCost += product.GetTotalCost();
        }

        // Add one-time shipping cost based on customer location
        totalCost += Customer.IsInUSA() ? 5 : 35;

        return totalCost;
    }

    public string GetPackingLabel()
    {
        string packingLabel = "Packing Label:\n";
        foreach (var product in Products)
        {
            packingLabel += $"{product.Name} (Product ID: {product.ProductId})\n";
        }
        return packingLabel;
    }

   public string GetShippingLabel()
{
    return $"Shipping Label:\nCustomer Name: {Customer.Name}\nCustomer Address: {Customer.GetFullAddress()}";
}
}

class Program
{
    static void Main()
    {
        // Create addresses
        Address usaAddress = new Address("591 Mission Boulevard", "San Diego", "CA", "USA");
        Address nonUsaAddress = new Address("409 Ludlow St", "Saskatoon", "Saskatchewan", "Canada");

        // Create customers
        Customer usaCustomer = new Customer("Donald Duck", usaAddress);
        Customer nonUsaCustomer = new Customer("Edison Garcia", nonUsaAddress);

        // Create products
        Product product1 = new Product("Laptop", "LPT001", 800, 2);
        Product product2 = new Product("Mouse", "MS001", 20, 5);
        Product product3 = new Product("Monitor", "MNT001", 300, 1);
         Product product4 = new Product("Keyboard", "KBD001", 45, 3);


        // Create orders
        Order usaOrder = new Order(new List<Product> { product1, product2 }, usaCustomer);
        Order nonUsaOrder = new Order(new List<Product> { product2, product3 }, nonUsaCustomer);

        // Display order information
        Console.WriteLine("Order 1 (USA):");
        Console.WriteLine(usaOrder.GetPackingLabel());
        Console.WriteLine(usaOrder.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${usaOrder.GetTotalCost():F2}\n");

        Console.WriteLine("Order 2 (Non-USA):");
        Console.WriteLine(nonUsaOrder.GetPackingLabel());
        Console.WriteLine(nonUsaOrder.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${nonUsaOrder.GetTotalCost():F2}");
    }
}