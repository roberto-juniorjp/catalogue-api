using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogueAPI.Migrations
{
    /// <inheritdoc />
    public partial class PopulateProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Products(ProductName,ProductDescription,ProductPrice,ProductImageUrl,InStock,DateRegister,CategoryId) " +
                "Values('Diet Coke','Coke Soda 350ml',5.45,'coke.jpg',50,now(),1)");
            mb.Sql("Insert into Products(ProductName,ProductDescription,ProductPrice,ProductImageUrl,InStock,DateRegister,CategoryId) " +
                "Values('Tuna','Tuna dish with mayonnaise',8.50,'tuna.jpg',10,now(),2)");
            mb.Sql("Insert into Products(ProductName,ProductDescription,ProductPrice,ProductImageUrl,InStock,DateRegister,CategoryId) " +
                "Values('Pudding','Pudding made with milk 100g',6.75,'pudding.jpg',20,now(),3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Products");
        }
    }
}
