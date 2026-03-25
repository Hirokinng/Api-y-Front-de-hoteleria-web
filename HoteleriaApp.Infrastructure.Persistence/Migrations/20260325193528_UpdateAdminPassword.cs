using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoteleriaApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdminPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "id_usuario",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "password_hash",
                value: "$2a$12$S6s9vLsPCLjA42bJrdHQ8.yWV4rivSdRP13COZsRKOZP8pOvyrG2y");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "id_usuario",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "password_hash",
                value: "$2a$12$a7qVkoub1TSN/y/2v9Q6X.97Jd7cKrReyunKGXrVvuEWYKsqD6nEq");
        }
    }
}
