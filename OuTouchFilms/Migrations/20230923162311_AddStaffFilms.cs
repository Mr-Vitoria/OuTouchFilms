using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OuTouchFilms.Migrations
{
    /// <inheritdoc />
    public partial class AddStaffFilms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Actors",
                table: "Films",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Composers",
                table: "Films",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Designs",
                table: "Films",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Editors",
                table: "Films",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Operators",
                table: "Films",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Producers",
                table: "Films",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Writers",
                table: "Films",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    SwaggerId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilmStaffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Profession = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    StaffId = table.Column<int>(type: "integer", nullable: false),
                    FilmId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmStaffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilmStaffs_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmStaffs_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmStaffs_FilmId",
                table: "FilmStaffs",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmStaffs_StaffId",
                table: "FilmStaffs",
                column: "StaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmStaffs");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropColumn(
                name: "Actors",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Composers",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Designs",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Editors",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Operators",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Producers",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Writers",
                table: "Films");
        }
    }
}
