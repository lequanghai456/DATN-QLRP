using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class add_isdelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isdelete",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isdelete",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isdelete",
                table: "ShowTimes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isdelete",
                table: "Sevices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isdelete",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isdelete",
                table: "Rates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isdelete",
                table: "Movies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isdelete",
                table: "Macs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isdelete",
                table: "Devices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isdelete",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isdelete",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isdelete",
                table: "Bills",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isdelete",
                table: "BillDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isdelete",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetRoles",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<bool>(
                name: "isdelete",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isdelete",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "isdelete",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "isdelete",
                table: "ShowTimes");

            migrationBuilder.DropColumn(
                name: "isdelete",
                table: "Sevices");

            migrationBuilder.DropColumn(
                name: "isdelete",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "isdelete",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "isdelete",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "isdelete",
                table: "Macs");

            migrationBuilder.DropColumn(
                name: "isdelete",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "isdelete",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "isdelete",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "isdelete",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "isdelete",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "isdelete",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "isdelete",
                table: "AspNetRoles");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetRoles",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);
        }
    }
}
