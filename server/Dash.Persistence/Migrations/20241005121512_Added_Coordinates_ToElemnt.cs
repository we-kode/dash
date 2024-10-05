using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dash.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_Coordinates_ToElemnt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cols",
                table: "elements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rows",
                table: "elements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "X",
                table: "elements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Y",
                table: "elements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cols",
                table: "elements");

            migrationBuilder.DropColumn(
                name: "Rows",
                table: "elements");

            migrationBuilder.DropColumn(
                name: "X",
                table: "elements");

            migrationBuilder.DropColumn(
                name: "Y",
                table: "elements");
        }
    }
}
