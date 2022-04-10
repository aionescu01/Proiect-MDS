using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectV1.DAL.Migrations
{
    public partial class Player_Staff_Name_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Staff",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Players",
                newName: "Name");

            migrationBuilder.AddColumn<float>(
                name: "Value",
                table: "Players",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Staff",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Players",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
