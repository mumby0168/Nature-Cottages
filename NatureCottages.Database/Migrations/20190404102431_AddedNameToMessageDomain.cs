using Microsoft.EntityFrameworkCore.Migrations;

namespace NatureCottages.Database.Migrations
{
    public partial class AddedNameToMessageDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ClientMessages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ClientMessages");
        }
    }
}
