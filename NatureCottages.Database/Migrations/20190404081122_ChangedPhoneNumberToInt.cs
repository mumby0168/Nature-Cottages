using Microsoft.EntityFrameworkCore.Migrations;

namespace NatureCottages.Database.Migrations
{
    public partial class ChangedPhoneNumberToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
