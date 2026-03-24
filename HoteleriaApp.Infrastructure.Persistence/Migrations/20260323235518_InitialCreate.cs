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
                name: "Amenities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.Id);
                });

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
                name: "Categories",
                columns: table => new
                {
                    id_categoria = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PricePerNight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Characteristics = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Capacidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.id_categoria);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    id_cliente = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    password_hash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    id_piso = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    numero_piso = table.Column<int>(type: "int", nullable: false),
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
                    id_servicio = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    id_temporada = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "TiposHabitacion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CapacidadBase = table.Column<int>(type: "int", nullable: false),
                    PrecioBasePorNoche = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposHabitacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id_usuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "Categoria_Servicio",
                columns: table => new
                {
                    id_categoria = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_servicio = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria_Servicio", x => new { x.id_categoria, x.id_servicio });
                    table.ForeignKey(
                        name: "FK_Categoria_Servicio_Categories_id_categoria",
                        column: x => x.id_categoria,
                        principalTable: "Categories",
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
                    id_tarifa = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_categoria = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_temporada = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    precio_noche = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarifa", x => x.id_tarifa);
                    table.ForeignKey(
                        name: "FK_Tarifa_Categories_id_categoria",
                        column: x => x.id_categoria,
                        principalTable: "Categories",
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
                name: "Habitacion",
                columns: table => new
                {
                    id_habitacion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    numero = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    piso = table.Column<int>(type: "int", nullable: false),
                    tipo_habitacion_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    capacidad = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCategoria = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitacion", x => x.id_habitacion);
                    table.ForeignKey(
                        name: "FK_Habitacion_Categories_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categories",
                        principalColumn: "id_categoria",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Habitacion_TiposHabitacion_tipo_habitacion_id",
                        column: x => x.tipo_habitacion_id,
                        principalTable: "TiposHabitacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    id_reserva = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    numero_reserva = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    id_cliente = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_usuario = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    id_categoria = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fecha_entrada = table.Column<DateOnly>(type: "date", nullable: false),
                    fecha_salida = table.Column<DateOnly>(type: "date", nullable: false),
                    numero_huespedes = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Confirmada"),
                    precio_base_noche = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_noches = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Reserva_Categories_id_categoria",
                        column: x => x.id_categoria,
                        principalTable: "Categories",
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
                name: "BloqueosHabitacion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HabitacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaFin = table.Column<DateOnly>(type: "date", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoBloqueo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloqueosHabitacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloqueosHabitacion_Habitacion_HabitacionId",
                        column: x => x.HabitacionId,
                        principalTable: "Habitacion",
                        principalColumn: "id_habitacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HabitacionesAmenities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HabitacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmenityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitacionesAmenities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HabitacionesAmenities_Amenities_AmenityId",
                        column: x => x.AmenityId,
                        principalTable: "Amenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HabitacionesAmenities_Habitacion_HabitacionId",
                        column: x => x.HabitacionId,
                        principalTable: "Habitacion",
                        principalColumn: "id_habitacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Detalle_Reserva",
                columns: table => new
                {
                    id_detalle = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_reserva = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_habitacion = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    id_historial = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_reserva = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    estado_anterior = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    estado_nuevo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    id_usuario = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    id_pago = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_reserva = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    id_reserva_servicio = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_reserva = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_servicio = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "ReservasHabitaciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HabitacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EsPrincipal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservasHabitaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservasHabitaciones_Habitacion_HabitacionId",
                        column: x => x.HabitacionId,
                        principalTable: "Habitacion",
                        principalColumn: "id_habitacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservasHabitaciones_Reserva_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reserva",
                        principalColumn: "id_reserva",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BloqueosHabitacion_HabitacionId",
                table: "BloqueosHabitacion",
                column: "HabitacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_Servicio_id_servicio",
                table: "Categoria_Servicio",
                column: "id_servicio");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_nombre",
                table: "Categories",
                column: "nombre",
                unique: true);

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
                name: "IX_Habitacion_IdCategoria",
                table: "Habitacion",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Habitacion_numero",
                table: "Habitacion",
                column: "numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Habitacion_tipo_habitacion_id",
                table: "Habitacion",
                column: "tipo_habitacion_id");

            migrationBuilder.CreateIndex(
                name: "IX_HabitacionesAmenities_AmenityId",
                table: "HabitacionesAmenities",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitacionesAmenities_HabitacionId",
                table: "HabitacionesAmenities",
                column: "HabitacionId");

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
                name: "IX_ReservasHabitaciones_HabitacionId",
                table: "ReservasHabitaciones",
                column: "HabitacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservasHabitaciones_ReservaId",
                table: "ReservasHabitaciones",
                column: "ReservaId");

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
                name: "BloqueosHabitacion");

            migrationBuilder.DropTable(
                name: "Categoria_Servicio");

            migrationBuilder.DropTable(
                name: "Detalle_Reserva");

            migrationBuilder.DropTable(
                name: "HabitacionesAmenities");

            migrationBuilder.DropTable(
                name: "Historial_Estado_Reserva");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "Piso");

            migrationBuilder.DropTable(
                name: "Reserva_Servicio");

            migrationBuilder.DropTable(
                name: "ReservasHabitaciones");

            migrationBuilder.DropTable(
                name: "Tarifa");

            migrationBuilder.DropTable(
                name: "Amenities");

            migrationBuilder.DropTable(
                name: "Servicio");

            migrationBuilder.DropTable(
                name: "Habitacion");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "Temporada");

            migrationBuilder.DropTable(
                name: "TiposHabitacion");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
