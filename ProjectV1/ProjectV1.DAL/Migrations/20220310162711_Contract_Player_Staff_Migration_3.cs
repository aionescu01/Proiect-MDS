using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectV1.DAL.Migrations
{
    public partial class Contract_Player_Staff_Migration_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Staff",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Staff",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Staff",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Staff",
                newName: "Name");
        }
    }
}
