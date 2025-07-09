using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EJERCICIOAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreadoEspectaculos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EspectaculoId",
                table: "Artistas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Espectaculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fechayhora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Espectaculos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artistas_EspectaculoId",
                table: "Artistas",
                column: "EspectaculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artistas_Espectaculos_EspectaculoId",
                table: "Artistas",
                column: "EspectaculoId",
                principalTable: "Espectaculos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artistas_Espectaculos_EspectaculoId",
                table: "Artistas");

            migrationBuilder.DropTable(
                name: "Espectaculos");

            migrationBuilder.DropIndex(
                name: "IX_Artistas_EspectaculoId",
                table: "Artistas");

            migrationBuilder.DropColumn(
                name: "EspectaculoId",
                table: "Artistas");
        }
    }
}
