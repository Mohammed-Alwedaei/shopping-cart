using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.API.Models;

[Table("ShoppingCartHeaders")]
public class ShoppingCartHeaderModel
{
    [Key]
    public Guid Id { get; set; }

    public string ShoppingCartDetailsId { get; set; }

    [NotMapped]
    public List<ShoppingCartDetailsModel> ShoppingCartDetails { get; set; }

    public string UserId { get; set; }

    public decimal TotalPrice { get; set; }

    public string ShippingAddress { get; set; }

    public DateTime CreatedDate { get; set; }
}