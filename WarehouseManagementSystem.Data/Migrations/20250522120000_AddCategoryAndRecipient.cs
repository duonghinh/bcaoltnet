using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseManagementSystem.Data.Migrations;

public partial class AddCategoryAndRecipient : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Category",
            table: "Items",
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: false,
            defaultValue: "Khác");

        migrationBuilder.AddColumn<string>(
            name: "RecipientName",
            table: "WithdrawalOrders",
            type: "nvarchar(200)",
            maxLength: 200,
            nullable: false,
            defaultValue: "");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "Category", table: "Items");
        migrationBuilder.DropColumn(name: "RecipientName", table: "WithdrawalOrders");
    }
}
