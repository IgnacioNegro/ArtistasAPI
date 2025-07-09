using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EJERCICIOAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDTOs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArtistaNombre",
                table: "Espectaculos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArtistaNombre",
                table: "Espectaculos");
        }
    }
}
