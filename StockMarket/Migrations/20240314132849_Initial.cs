using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockMarket.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Securities",
                columns: table => new
                {
                    Ticker = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Securities", x => x.Ticker);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    XAPIKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Funds = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdersHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Ticker = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    VolumeRequested = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdersHistory_Securities_Ticker",
                        column: x => x.Ticker,
                        principalTable: "Securities",
                        principalColumn: "Ticker",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdersHistory_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsersPortfolios",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersPortfolios", x => new { x.UserId, x.Ticker });
                    table.ForeignKey(
                        name: "FK_UsersPortfolios_Securities_Ticker",
                        column: x => x.Ticker,
                        principalTable: "Securities",
                        principalColumn: "Ticker",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersPortfolios_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActiveOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    VolumeRemaining = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActiveOrders_OrdersHistory_Id",
                        column: x => x.Id,
                        principalTable: "OrdersHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActiveOrders_Securities_Ticker",
                        column: x => x.Ticker,
                        principalTable: "Securities",
                        principalColumn: "Ticker",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActiveOrders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderFills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    IdBuyOrder = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSellOrder = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Volume = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsSettled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderFills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderFills_ActiveOrders_IdBuyOrder",
                        column: x => x.IdBuyOrder,
                        principalTable: "ActiveOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderFills_ActiveOrders_IdSellOrder",
                        column: x => x.IdSellOrder,
                        principalTable: "ActiveOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveOrders_Ticker",
                table: "ActiveOrders",
                column: "Ticker");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveOrders_UserId",
                table: "ActiveOrders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderFills_IdBuyOrder",
                table: "OrderFills",
                column: "IdBuyOrder");

            migrationBuilder.CreateIndex(
                name: "IX_OrderFills_IdSellOrder",
                table: "OrderFills",
                column: "IdSellOrder");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersHistory_Ticker",
                table: "OrdersHistory",
                column: "Ticker");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersHistory_UserId",
                table: "OrdersHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_XAPIKey",
                table: "Users",
                column: "XAPIKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersPortfolios_Ticker",
                table: "UsersPortfolios",
                column: "Ticker");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderFills");

            migrationBuilder.DropTable(
                name: "UsersPortfolios");

            migrationBuilder.DropTable(
                name: "ActiveOrders");

            migrationBuilder.DropTable(
                name: "OrdersHistory");

            migrationBuilder.DropTable(
                name: "Securities");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
