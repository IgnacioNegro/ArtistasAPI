using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EJERCICIOAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreatedDTOforEspectaculos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artistas_Espectaculos_EspectaculoId",
                table: "Artistas");

            migrationBuilder.DropIndex(
                name: "IX_Artistas_EspectaculoId",
                table: "Artistas");

            migrationBuilder.DropColumn(
                name: "EspectaculoId",
                table: "Artistas");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuarios",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ArtistaId",
                table: "Espectaculos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Categorias",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Artistas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Espectaculos_ArtistaId",
                table: "Espectaculos",
                column: "ArtistaId");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_Nombre",
                table: "Categorias",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artistas_Nombre",
                table: "Artistas",
                column: "Nombre",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Espectaculos_Artistas_ArtistaId",
                table: "Espectaculos",
                column: "ArtistaId",
                principalTable: "Artistas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Espectaculos_Artistas_ArtistaId",
                table: "Espectaculos");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Espectaculos_ArtistaId",
                table: "Espectaculos");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_Nombre",
                table: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Artistas_Nombre",
                table: "Artistas");

            migrationBuilder.DropColumn(
                name: "ArtistaId",
                table: "Espectaculos");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Artistas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "EspectaculoId",
                table: "Artistas",
                type: "int",
                nullable: true);

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
    }
}
