using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tp5Fi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alojamiento",
                columns: table => new
                {
                    codigo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo = table.Column<string>(nullable: true),
                    ciudad = table.Column<string>(nullable: true),
                    barrio = table.Column<string>(nullable: true),
                    estrellas = table.Column<int>(nullable: false),
                    cantPersonas = table.Column<int>(nullable: false),
                    tv = table.Column<bool>(nullable: false),
                    precioxDia = table.Column<double>(nullable: false),
                    precioxPersona = table.Column<double>(nullable: false),
                    habitacion = table.Column<int>(nullable: false),
                    baño = table.Column<int>(nullable: false),
                    ocupado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alojamiento", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FDesde = table.Column<DateTime>(nullable: false),
                    FHasta = table.Column<DateTime>(nullable: false),
                    codigoAlojamiento = table.Column<int>(nullable: false),
                    codigoPersona = table.Column<int>(nullable: false),
                    precio = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    usuarioID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dni = table.Column<int>(nullable: false),
                    nombre = table.Column<string>(nullable: true),
                    mail = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    esAdmin = table.Column<bool>(nullable: false),
                    bloqueado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.usuarioID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alojamiento");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
