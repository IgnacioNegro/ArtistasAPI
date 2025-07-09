using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EJERCICIOAPI.Migrations
{
    /// <inheritdoc />
    public partial class Funcionando : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Artistas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordEncriptado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artistas_UsuarioId",
                table: "Artistas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artistas_Usuarios_UsuarioId",
                table: "Artistas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artistas_Usuarios_UsuarioId",
                table: "Artistas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Artistas_UsuarioId",
                table: "Artistas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Artistas");
        }
    }
}
