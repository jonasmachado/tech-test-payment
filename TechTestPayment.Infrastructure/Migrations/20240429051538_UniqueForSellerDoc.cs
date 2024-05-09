using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechTestPayment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UniqueForSellerDoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "cpf",
                schema: "tech_test_payment",
                table: "seller",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_seller_cpf",
                schema: "tech_test_payment",
                table: "seller",
                column: "cpf",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_seller_cpf",
                schema: "tech_test_payment",
                table: "seller");

            migrationBuilder.AlterColumn<string>(
                name: "cpf",
                schema: "tech_test_payment",
                table: "seller",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
