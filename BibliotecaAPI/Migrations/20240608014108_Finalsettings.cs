using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaAPI.Migrations
{
    public partial class Finalsettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstadoLibro",
                table: "Prestamos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EstadoPrestamo",
                table: "Prestamos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EstadoUsuario",
                table: "Prestamos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoLibro",
                table: "Prestamos");

            migrationBuilder.DropColumn(
                name: "EstadoPrestamo",
                table: "Prestamos");

            migrationBuilder.DropColumn(
                name: "EstadoUsuario",
                table: "Prestamos");
        }
    }
}
