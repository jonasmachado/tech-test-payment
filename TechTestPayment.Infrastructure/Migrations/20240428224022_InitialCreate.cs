using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechTestPayment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tech_test_payment");

            migrationBuilder.CreateTable(
                name: "product",
                schema: "tech_test_payment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    remaining = table.Column<double>(type: "float", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "seller",
                schema: "tech_test_payment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seller", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order",
                schema: "tech_test_payment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    seller_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_seller_seller_id",
                        column: x => x.seller_id,
                        principalSchema: "tech_test_payment",
                        principalTable: "seller",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order_product",
                schema: "tech_test_payment",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_product", x => new { x.order_id, x.product_id });
                    table.ForeignKey(
                        name: "FK_order_product_order_order_id",
                        column: x => x.order_id,
                        principalSchema: "tech_test_payment",
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_product_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "tech_test_payment",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_seller_id",
                schema: "tech_test_payment",
                table: "order",
                column: "seller_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_product_product_id",
                schema: "tech_test_payment",
                table: "order_product",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_product",
                schema: "tech_test_payment");

            migrationBuilder.DropTable(
                name: "order",
                schema: "tech_test_payment");

            migrationBuilder.DropTable(
                name: "product",
                schema: "tech_test_payment");

            migrationBuilder.DropTable(
                name: "seller",
                schema: "tech_test_payment");
        }
    }
}
