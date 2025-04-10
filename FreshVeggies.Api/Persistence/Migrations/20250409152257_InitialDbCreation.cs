using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FreshVeggies.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Cellphone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FullAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AddressName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UserAddressId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ImageUrl", "Name", "Price", "Unit" },
                values: new object[,]
                {
                    { 1, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/avocado.png", "Avocado", 5.00m, "each" },
                    { 2, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/beet.png", "Beet", 10.00m, "each" },
                    { 3, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/bell_pepper.png", "Bell Pepper", 10.00m, "each" },
                    { 4, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/broccoli.png", "Broccoli", 9.99m, "each" },
                    { 5, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/cabbage.png", "Cabbage", 15.50m, "each" },
                    { 6, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/capsicum.png", "Capsicum", 21.80m, "each" },
                    { 7, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/carrot.png", "Carrot", 10.80m, "kg" },
                    { 8, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/cauliflower.png", "Cauliflower", 20.50m, "each" },
                    { 9, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/coriander.png", "Coriander", 10.70m, "bunch" },
                    { 10, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/corn.png", "Corn", 15.00m, "each" },
                    { 11, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/cucumber.png", "Cucumber", 10.90m, "each" },
                    { 12, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/eggplant.png", "Eggplant", 15.75m, "each" },
                    { 13, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/farmer.png", "Farmer", 15.00m, "each" },
                    { 14, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/ginger.png", "Ginger", 22.20m, "kg" },
                    { 15, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/green_beans.png", "Green Beans", 31.60m, "kg" },
                    { 16, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/ladyfinger.png", "Ladyfinger", 21.10m, "kg" },
                    { 17, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/lettuce.png", "Lettuce", 10.30m, "each" },
                    { 18, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/mushroom.png", "Mushroom", 12.80m, "kg" },
                    { 19, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/onion.png", "Onion", 20.60m, "kg" },
                    { 20, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/pea.png", "Pea", 25.40m, "kg" },
                    { 21, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/potato.png", "Potato", 50.50m, "kg" },
                    { 22, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/pumpkin.png", "Pumpkin", 23.00m, "each" },
                    { 23, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/radish.png", "Radish", 20.85m, "bunch" },
                    { 24, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/red_chili.png", "Red Chili", 11.50m, "kg" },
                    { 25, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/spinach.png", "Spinach", 15.20m, "bunch" },
                    { 26, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/tomato.png", "Tomato", 20.95m, "kg" },
                    { 27, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/turnip.png", "Turnip", 10.75m, "each" },
                    { 28, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/vegetables.png", "Vegetables", 14.00m, "each" },
                    { 29, "https://raw.githubusercontent.com/Abhayprince/Images-Icons/refs/heads/main/Vegetables/yellow_capsicum.png", "Yellow Capsicum", 11.80m, "each" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
