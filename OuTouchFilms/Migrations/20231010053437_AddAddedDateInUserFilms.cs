using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OuTouchFilms.Migrations
{
    /// <inheritdoc />
    public partial class AddAddedDateInUserFilms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "AddedDate",
                table: "UserFilms",
                type: "date",
                nullable: false,
                defaultValue: DateOnly.FromDateTime(DateTime.Now));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "UserFilms");
        }
    }
}
