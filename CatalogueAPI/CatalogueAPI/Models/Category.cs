using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogueAPI.Models;

[Table("Categories")]
public class Category
{
    public Category()
    => Products = [];
    
    [Key]
    public int CategoryId { get; set; }
    
    [Required(ErrorMessage = "You must provide a category name!")]
    [StringLength(80, ErrorMessage = "Name is too big.")]
    public string? CategoryName { get; set; }

    [Required(ErrorMessage = "You must provide a category image url!")]
    [StringLength(300, ErrorMessage = "URL provided is too big.")]
    public string? CategoryImageUrl { get; set; }

    public ICollection<Product>? Products { get; set; }
}
