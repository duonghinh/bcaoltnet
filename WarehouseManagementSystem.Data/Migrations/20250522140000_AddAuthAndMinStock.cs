using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseManagementSystem.Data.Migrations;

public partial class AddAuthAndMinStock : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "MinStockQuantity",
            table: "Items",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateTable(
            name: "Roles",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_Roles", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false),
                RoleId = table.Column<int>(type: "int", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
                table.ForeignKey(
                    name: "FK_Users_Roles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "Roles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Users_RoleId",
            table: "Users",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "IX_Users_Username",
            table: "Users",
            column: "Username",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Users");
        migrationBuilder.DropTable(name: "Roles");
        migrationBuilder.DropColumn(name: "MinStockQuantity", table: "Items");
    }
}
