using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DA.Migrations
{
    /// <inheritdoc />
    public partial class AgregarAnioFormacionArtista : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnioFormacion",
                table: "Artistas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Artistas",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "AnioFormacion",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Artistas",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "AnioFormacion",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Artistas",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                column: "AnioFormacion",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnioFormacion",
                table: "Artistas");
        }
    }
}
