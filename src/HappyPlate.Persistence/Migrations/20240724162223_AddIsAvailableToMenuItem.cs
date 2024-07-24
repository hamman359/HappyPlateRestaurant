using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyPlate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIsAvailableToMenuItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "MenuItem",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "MenuItem");
        }
    }
}
