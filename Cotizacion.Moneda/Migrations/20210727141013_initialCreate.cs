using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cotizacion.Moneda.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComprarMoneda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdUsuario = table.Column<string>(type: "TEXT", nullable: true),
                    MontoComprar = table.Column<decimal>(type: "TEXT", nullable: false),
                    TipoMoneda = table.Column<string>(type: "TEXT", nullable: true),
                    FechaCompra = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComprarMoneda", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComprarMoneda");
        }
    }
}
