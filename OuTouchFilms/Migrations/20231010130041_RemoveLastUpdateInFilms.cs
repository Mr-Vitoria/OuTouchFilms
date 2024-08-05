using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OuTouchFilms.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLastUpdateInFilms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "Films");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastUpdate",
                table: "Films",
                type: "text",
                nullable: true);
        }
    }
}
