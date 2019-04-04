using Microsoft.EntityFrameworkCore.Migrations;

namespace NatureCottages.Database.Migrations
{
    public partial class addedClosedTagtoMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "ClientMessages",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "ClientMessages");
        }
    }
}
