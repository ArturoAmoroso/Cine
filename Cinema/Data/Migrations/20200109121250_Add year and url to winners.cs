using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinema.Data.Migrations
{
    public partial class Addyearandurltowinners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imgUrlActor",
                table: "Winners",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "imgUrlMovie",
                table: "Winners",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "year",
                table: "Movies",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imgUrlActor",
                table: "Winners");

            migrationBuilder.DropColumn(
                name: "imgUrlMovie",
                table: "Winners");

            migrationBuilder.DropColumn(
                name: "year",
                table: "Movies");
        }
    }
}
