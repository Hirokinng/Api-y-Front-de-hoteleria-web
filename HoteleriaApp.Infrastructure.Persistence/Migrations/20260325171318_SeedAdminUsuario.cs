using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoteleriaApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "id_usuario", "activo", "email", "fecha_creacion", "nombre", "password_hash", "rol" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), true, "admin@hotel.com", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Administrador del Sistema", "$2a$12$a7qVkoub1TSN/y/2v9Q6X.97Jd7cKrReyunKGXrVvuEWYKsqD6nEq", "Administrador" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "id_usuario",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));
        }
    }
}
