using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dash.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_IsFocused_Information : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFocused",
                table: "informations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFocused",
                table: "informations");
        }
    }
}
