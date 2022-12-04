namespace Products.API.Models;

public class ProductsStoreDatabaseConfig
{
    public string? ConnectionString { get; set; }

    public string? DatabaseName { get; set; }

    public string? CollectionName { get; set; }
}