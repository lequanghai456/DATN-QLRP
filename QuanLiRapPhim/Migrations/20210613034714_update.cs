using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoleId",
                table: "Rooms",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_AspNetRoles_RoleId",
                table: "Rooms",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_AspNetRoles_RoleId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoleId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Rooms");
        }
    }
}
