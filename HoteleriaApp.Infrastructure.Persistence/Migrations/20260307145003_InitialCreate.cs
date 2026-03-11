using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoteleriaApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auditoria",
                columns: table => new
                {
                    id_auditoria = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<int>(type: "int", nullable: true),
                    tabla = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    operacion = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditoria", x => x.id_auditoria);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    id_categoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    capacidad_max = table.Column<byte>(type: "tinyint", nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.id_categoria);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    id_cliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    password_hash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    fecha_registro = table.Column<DateOnly>(type: "date", nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.id_cliente);
                });

            migrationBuilder.CreateTable(
                name: "Piso",
                columns: table => new
                {
                    id_piso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numero_piso = table.Column<short>(type: "smallint", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Piso", x => x.id_piso);
                });

            migrationBuilder.CreateTable(
                name: "Servicio",
                columns: table => new
                {
                    id_servicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicio", x => x.id_servicio);
                });

            migrationBuilder.CreateTable(
                name: "Temporada",
                columns: table => new
                {
                    id_temporada = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    fecha_inicio = table.Column<DateOnly>(type: "date", nullable: false),
                    fecha_fin = table.Column<DateOnly>(type: "date", nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temporada", x => x.id_temporada);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    rol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "Habitacion",
                columns: table => new
                {
                    id_habitacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numero_habitacion = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    id_piso = table.Column<int>(type: "int", nullable: false),
                    id_categoria = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Disponible"),
                    descripcion_adicional = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    fecha_ultima_actualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitacion", x => x.id_habitacion);
                    table.ForeignKey(
                        name: "FK_Habitacion_Categoria_id_categoria",
                        column: x => x.id_categoria,
                        principalTable: "Categoria",
                        principalColumn: "id_categoria",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Habitacion_Piso_id_piso",
                        column: x => x.id_piso,
                        principalTable: "Piso",
                        principalColumn: "id_piso",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categoria_Servicio",
                columns: table => new
                {
                    id_categoria = table.Column<int>(type: "int", nullable: false),
                    id_servicio = table.Column<int>(type: "int", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria_Servicio", x => new { x.id_categoria, x.id_servicio });
                    table.ForeignKey(
                        name: "FK_Categoria_Servicio_Categoria_id_categoria",
                        column: x => x.id_categoria,
                        principalTable: "Categoria",
                        principalColumn: "id_categoria",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categoria_Servicio_Servicio_id_servicio",
                        column: x => x.id_servicio,
                        principalTable: "Servicio",
                        principalColumn: "id_servicio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tarifa",
                columns: table => new
                {
                    id_tarifa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_categoria = table.Column<int>(type: "int", nullable: false),
                    id_temporada = table.Column<int>(type: "int", nullable: true),
                    precio_noche = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarifa", x => x.id_tarifa);
                    table.ForeignKey(
                        name: "FK_Tarifa_Categoria_id_categoria",
                        column: x => x.id_categoria,
                        principalTable: "Categoria",
                        principalColumn: "id_categoria",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tarifa_Temporada_id_temporada",
                        column: x => x.id_temporada,
                        principalTable: "Temporada",
                        principalColumn: "id_temporada",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    id_reserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numero_reserva = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    id_cliente = table.Column<int>(type: "int", nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: true),
                    id_categoria = table.Column<int>(type: "int", nullable: false),
                    fecha_entrada = table.Column<DateOnly>(type: "date", nullable: false),
                    fecha_salida = table.Column<DateOnly>(type: "date", nullable: false),
                    numero_huespedes = table.Column<byte>(type: "tinyint", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Confirmada"),
                    precio_base_noche = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_noches = table.Column<short>(type: "smallint", nullable: false),
                    subtotal_habitacion = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_servicios = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0m),
                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fecha_cancelacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    referencia_externa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.id_reserva);
                    table.ForeignKey(
                        name: "FK_Reserva_Categoria_id_categoria",
                        column: x => x.id_categoria,
                        principalTable: "Categoria",
                        principalColumn: "id_categoria",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reserva_Cliente_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "Cliente",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reserva_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Detalle_Reserva",
                columns: table => new
                {
                    id_detalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_reserva = table.Column<int>(type: "int", nullable: false),
                    id_habitacion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalle_Reserva", x => x.id_detalle);
                    table.ForeignKey(
                        name: "FK_Detalle_Reserva_Habitacion_id_habitacion",
                        column: x => x.id_habitacion,
                        principalTable: "Habitacion",
                        principalColumn: "id_habitacion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detalle_Reserva_Reserva_id_reserva",
                        column: x => x.id_reserva,
                        principalTable: "Reserva",
                        principalColumn: "id_reserva",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Historial_Estado_Reserva",
                columns: table => new
                {
                    id_historial = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_reserva = table.Column<int>(type: "int", nullable: false),
                    estado_anterior = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    estado_nuevo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: true),
                    fecha_cambio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    observacion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historial_Estado_Reserva", x => x.id_historial);
                    table.ForeignKey(
                        name: "FK_Historial_Estado_Reserva_Reserva_id_reserva",
                        column: x => x.id_reserva,
                        principalTable: "Reserva",
                        principalColumn: "id_reserva",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Historial_Estado_Reserva_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    id_pago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_reserva = table.Column<int>(type: "int", nullable: false),
                    monto = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Pendiente"),
                    referencia_externa = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    fecha_pago = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x.id_pago);
                    table.ForeignKey(
                        name: "FK_Pago_Reserva_id_reserva",
                        column: x => x.id_reserva,
                        principalTable: "Reserva",
                        principalColumn: "id_reserva",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reserva_Servicio",
                columns: table => new
                {
                    id_reserva_servicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_reserva = table.Column<int>(type: "int", nullable: false),
                    id_servicio = table.Column<int>(type: "int", nullable: false),
                    precio_aplicado = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva_Servicio", x => x.id_reserva_servicio);
                    table.ForeignKey(
                        name: "FK_Reserva_Servicio_Reserva_id_reserva",
                        column: x => x.id_reserva,
                        principalTable: "Reserva",
                        principalColumn: "id_reserva",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_Servicio_Servicio_id_servicio",
                        column: x => x.id_servicio,
                        principalTable: "Servicio",
                        principalColumn: "id_servicio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_nombre",
                table: "Categoria",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_Servicio_id_servicio",
                table: "Categoria_Servicio",
                column: "id_servicio");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_email",
                table: "Cliente",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_Reserva_id_habitacion",
                table: "Detalle_Reserva",
                column: "id_habitacion");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_Reserva_id_reserva",
                table: "Detalle_Reserva",
                column: "id_reserva");

            migrationBuilder.CreateIndex(
                name: "IX_Habitacion_id_categoria",
                table: "Habitacion",
                column: "id_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Habitacion_id_piso",
                table: "Habitacion",
                column: "id_piso");

            migrationBuilder.CreateIndex(
                name: "IX_Habitacion_numero_habitacion",
                table: "Habitacion",
                column: "numero_habitacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Historial_Estado_Reserva_id_reserva",
                table: "Historial_Estado_Reserva",
                column: "id_reserva");

            migrationBuilder.CreateIndex(
                name: "IX_Historial_Estado_Reserva_id_usuario",
                table: "Historial_Estado_Reserva",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_id_reserva",
                table: "Pago",
                column: "id_reserva");

            migrationBuilder.CreateIndex(
                name: "IX_Piso_numero_piso",
                table: "Piso",
                column: "numero_piso",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_id_categoria",
                table: "Reserva",
                column: "id_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_id_cliente",
                table: "Reserva",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_id_usuario",
                table: "Reserva",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_numero_reserva",
                table: "Reserva",
                column: "numero_reserva",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_Servicio_id_reserva_id_servicio",
                table: "Reserva_Servicio",
                columns: new[] { "id_reserva", "id_servicio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_Servicio_id_servicio",
                table: "Reserva_Servicio",
                column: "id_servicio");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_nombre",
                table: "Servicio",
                column: "nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tarifa_id_categoria",
                table: "Tarifa",
                column: "id_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Tarifa_id_temporada",
                table: "Tarifa",
                column: "id_temporada");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_email",
                table: "Usuario",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditoria");

            migrationBuilder.DropTable(
                name: "Categoria_Servicio");

            migrationBuilder.DropTable(
                name: "Detalle_Reserva");

            migrationBuilder.DropTable(
                name: "Historial_Estado_Reserva");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "Reserva_Servicio");

            migrationBuilder.DropTable(
                name: "Tarifa");

            migrationBuilder.DropTable(
                name: "Habitacion");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "Servicio");

            migrationBuilder.DropTable(
                name: "Temporada");

            migrationBuilder.DropTable(
                name: "Piso");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
