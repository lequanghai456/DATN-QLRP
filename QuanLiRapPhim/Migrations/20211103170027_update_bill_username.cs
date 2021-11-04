using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class update_bill_username : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Users_UserId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_UserId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bills");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "273bf690-2c0d-4b40-ab9f-2a65da92da0f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "69d4ea5f-9d58-4daf-89df-5a618c3f68d8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1cb06bb-2e74-4be0-a363-f69cb194407b", "AGDSblkSTROtDwG06K9zWBXpdcrK4yzRpAZy4OBp6xe+Ji/vEnLuKAvDzS63nS1ykA==", "9bd097b1-dd53-47ed-ab82-cc29a7d7ad26" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ac45417-4b43-42c6-8eeb-c7fbcf44ea65", "AApPqw59GAHh9VLusZWuEW/mKcFHxWx1CpmJby++8m7iXH/PDmpKAjqgDht+1UPLQg==", "5f14c1bf-a70d-483c-a3ed-2b084bffe712" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Bills");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "00735db5-3e36-4069-8704-710dd7f476ce");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ab89281d-8425-4132-b19e-7de910b1066d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9cfd4d94-5d5a-42a0-b994-f44aba114417", "AFGJ9NWPYyocQ1nRYPEfGAqrzjoGQHZ1ehh2lciFx60ky+2xFVALt66Bc9Puu/23+A==", "cda907bc-85a0-47b0-8118-4ffd67cc6244" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d5f5520-7aca-474a-9523-7375807872dc", "APMvutPVqwwC6OLQuC/nUWU4dfPIOwD0FBKoRblsTpq0ijxm8vjrT1ehx9eVVJbY4Q==", "cdc0e105-62e5-44d5-88bd-f6bf9b42b3fb" });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_UserId",
                table: "Bills",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Users_UserId",
                table: "Bills",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
