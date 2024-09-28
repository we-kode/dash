using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dash.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_NameColumn_INformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "informations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "informations");
        }
    }
}
