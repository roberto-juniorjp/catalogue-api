namespace CatalogueAPI.Models;

public class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? ProductDescription { get; set; }

    public decimal ProductPrice { get; set; }

    public string? ProductImageUrl { get; set; }

    public float InStock { get; set; }

    public DateTime DateRegister { get; set; }

    public int CategoryID { get; set; }

    public Category? Category { get; set; }
}
