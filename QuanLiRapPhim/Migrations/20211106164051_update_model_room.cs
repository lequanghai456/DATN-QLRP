using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class update_model_room : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7e0cda90-9179-406d-806d-6fd72c7626c2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "8e51bece-f2ba-4a6c-bfe4-ca3bad8bcf46");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b4fff984-182a-4858-986e-feaee7bf49e0", "AOhUaWCxMgvma1/Lyath2/CDLE3x8QImPQ6mfAlDe2DEM6dkAag+Z4DScJUuevC8nA==", "4a930a8e-6017-41b3-9fee-5d6cbb361554" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0771fbfe-a5e3-4386-b295-7351950a2bc6", "AGPxUkfNjkCVLM6/XKZ0TWaxqCV881uxiKWy2EykiCMeTtHh++QndSpxHguWjfldog==", "48b031d5-8b3f-4271-874e-124f347cd6fb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
