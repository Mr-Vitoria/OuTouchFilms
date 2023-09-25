using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OuTouchFilms.Migrations
{
    /// <inheritdoc />
    public partial class AddDirectorsInFilm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DirectorIds",
                table: "Films",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DirectorIds",
                table: "Films");
        }
    }
}
