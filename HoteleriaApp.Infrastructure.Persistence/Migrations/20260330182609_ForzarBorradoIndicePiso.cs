using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoteleriaApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ForzarBorradoIndicePiso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Piso_numero_piso",
                table: "Piso");

            migrationBuilder.CreateIndex(
                name: "IX_Piso_numero_piso",
                table: "Piso",
                column: "numero_piso");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Piso_numero_piso",
                table: "Piso");

            migrationBuilder.CreateIndex(
                name: "IX_Piso_numero_piso",
                table: "Piso",
                column: "numero_piso",
                unique: true);
        }
    }
}
