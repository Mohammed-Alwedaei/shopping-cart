using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.API.Models;

[Table("Products")]
public class ProductModel
{
    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set;}
}