using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseManagementSystem.Data.Migrations;

public partial class AddActivityLogs : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ActivityLogs",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: true),
                Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                EntityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Reference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Details = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_ActivityLogs", x => x.Id));
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "ActivityLogs");
    }
}
