using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinema.Data.Migrations
{
    public partial class imgURLadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imgURL",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "imgURL",
                table: "Actors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imgURL",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "imgURL",
                table: "Actors");
        }
    }
}
