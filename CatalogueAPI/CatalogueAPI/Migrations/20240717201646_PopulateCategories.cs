using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogueAPI.Migrations
{
    /// <inheritdoc />
    public partial class PopulateCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Categories(CategoryName, CategoryImageUrl) Values('Bebidas', 'bebidas.jpg')");
            mb.Sql("Insert into Categories(CategoryName, CategoryImageUrl) Values('Lanche', 'lanches.jpg')");
            mb.Sql("Insert into Categories(CategoryName, CategoryImageUrl) Values('Sobremesas', 'sobremesas.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categories");
        }
    }
}
