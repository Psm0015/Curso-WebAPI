using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO `catalogodb`.`categorias`(`Nome`,`ImageUrl`)VALUES('Bebidas','bebidas.jpg');");
            mb.Sql("INSERT INTO `catalogodb`.`categorias`(`Nome`,`ImageUrl`)VALUES('Lanches','lanches.jpg');");
            mb.Sql("INSERT INTO `catalogodb`.`categorias`(`Nome`,`ImageUrl`)VALUES('Sobremesas','sobremesas.jpg');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categorias");
        }
    }
}
