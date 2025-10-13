using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KafeApi.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class updateTableProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableCode",
                table: "Tables");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TableCode",
                table: "Tables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
