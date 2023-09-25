using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OuTouchFilms.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStaffsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmStaffs");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.RenameColumn(
                name: "Writers",
                table: "Films",
                newName: "WriterIds");

            migrationBuilder.RenameColumn(
                name: "Producers",
                table: "Films",
                newName: "ProducerIds");

            migrationBuilder.RenameColumn(
                name: "Operators",
                table: "Films",
                newName: "OperatorIds");

            migrationBuilder.RenameColumn(
                name: "Editors",
                table: "Films",
                newName: "EditorIds");

            migrationBuilder.RenameColumn(
                name: "Designs",
                table: "Films",
                newName: "DesignIds");

            migrationBuilder.RenameColumn(
                name: "Composers",
                table: "Films",
                newName: "ComposerIds");

            migrationBuilder.RenameColumn(
                name: "Actors",
                table: "Films",
                newName: "ActorIds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WriterIds",
                table: "Films",
                newName: "Writers");

            migrationBuilder.RenameColumn(
                name: "ProducerIds",
                table: "Films",
                newName: "Producers");

            migrationBuilder.RenameColumn(
                name: "OperatorIds",
                table: "Films",
                newName: "Operators");

            migrationBuilder.RenameColumn(
                name: "EditorIds",
                table: "Films",
                newName: "Editors");

            migrationBuilder.RenameColumn(
                name: "DesignIds",
                table: "Films",
                newName: "Designs");

            migrationBuilder.RenameColumn(
                name: "ComposerIds",
                table: "Films",
                newName: "Composers");

            migrationBuilder.RenameColumn(
                name: "ActorIds",
                table: "Films",
                newName: "Actors");

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SwaggerId = table.Column<string>(type: "text", nullable: false)
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
                    FilmId = table.Column<int>(type: "integer", nullable: false),
                    StaffId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Profession = table.Column<string>(type: "text", nullable: false)
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
    }
}
