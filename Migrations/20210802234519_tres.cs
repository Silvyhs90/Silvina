using Microsoft.EntityFrameworkCore.Migrations;

namespace tp5Fi.Migrations
{
    public partial class tres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "nombreAlojamiento",
                table: "Reserva",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nombreAlojamiento",
                table: "Reserva");
        }
    }
}
