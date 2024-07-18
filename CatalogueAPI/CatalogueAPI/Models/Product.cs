using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogueAPI.Models;

[Table("Products")]
public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required(ErrorMessage="You must provide a product name!")]
    [StringLength(80, ErrorMessage ="Product name is too big.")]
    public string? ProductName { get; set; }

    [Required(ErrorMessage = "You must provide a product description!")]
    [StringLength(300, ErrorMessage = "Product description is too big.")]
    public string? ProductDescription { get; set; }

    [Required(ErrorMessage = "You must provide a product price!")]
    [Column(TypeName = "decimal(10,2)")]
    public decimal ProductPrice { get; set; }

    [Required(ErrorMessage = "You must provide a product image url!")]
    [StringLength(300, ErrorMessage = "Product image url is too big.")]
    public string? ProductImageUrl { get; set; }

    public float InStock { get; set; }

    public DateTime DateRegister { get; set; }

    public int CategoryID { get; set; }

    public Category? Category { get; set; }
}
