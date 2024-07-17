using CatalogueAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogueAPI.Context;

public class CatalogueDbContext(DbContextOptions<CatalogueDbContext> options) : DbContext(options)
{
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Product>? Products { get; set; }
}