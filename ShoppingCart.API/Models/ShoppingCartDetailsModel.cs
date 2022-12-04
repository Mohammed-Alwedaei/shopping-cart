using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.API.Models;

[Table("ShoppingCartDetails")]
public class ShoppingCartDetailsModel
{
    [Key]
    public string Id { get; set; }

    public Guid ShoppingCartHeaderId { get; set; }

    public string? ProductId { get; set; }

    [ForeignKey("ProductId")]
    public List<ProductModel> Products { get; set; } = new ();

    public DateTime CreatedDate { get; set; }
}