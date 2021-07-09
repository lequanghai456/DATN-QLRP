using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class create_SeviceCat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_Sevices_SeviceId",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Sevices");

            migrationBuilder.RenameColumn(
                name: "SeviceId",
                table: "BillDetails",
                newName: "SeviceCatId");

            migrationBuilder.RenameIndex(
                name: "IX_BillDetails_SeviceId",
                table: "BillDetails",
                newName: "IX_BillDetails_SeviceCatId");

            migrationBuilder.CreateTable(
                name: "SeviceCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSevice = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeviceCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeviceCategories_Sevices_IdSevice",
                        column: x => x.IdSevice,
                        principalTable: "Sevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6e5ab9df-1705-41bf-9c8e-c43c11c5ea73");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "2724f1e2-3ca4-4b58-bfae-1374a7bf1c07");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "b525b4d5-0fd5-4c34-b05b-f81cbf8e3ed4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "416e2c95-c436-4f86-9b37-319130146ba8", "AKzfBQIMHd646Jm/8cfwVnoshji856Q3EF37Vb3phmvQnYLz6/3pFLCPV6PLXx+hcw==", "c9adcc87-176a-4ad7-89b2-0e77ab8f96c6" });

            migrationBuilder.InsertData(
                table: "SeviceCategories",
                columns: new[] { "Id", "IdSevice", "IsDeleted", "Name", "price" },
                values: new object[,]
                {
                    { 1, 1, false, "Big", 10000m },
                    { 2, 1, false, "Small", 5000m },
                    { 3, 1, false, "Medium", 7000m },
                    { 4, 2, false, "Big", 10000m },
                    { 5, 2, false, "Small", 5000m },
                    { 6, 2, false, "Medium", 7000m },
                    { 7, 3, false, "Big", 10000m },
                    { 8, 3, false, "Small", 5000m },
                    { 9, 3, false, "Medium", 7000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeviceCategories_IdSevice",
                table: "SeviceCategories",
                column: "IdSevice");

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_SeviceCategories_SeviceCatId",
                table: "BillDetails",
                column: "SeviceCatId",
                principalTable: "SeviceCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_SeviceCategories_SeviceCatId",
                table: "BillDetails");

            migrationBuilder.DropTable(
                name: "SeviceCategories");

            migrationBuilder.RenameColumn(
                name: "SeviceCatId",
                table: "BillDetails",
                newName: "SeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_BillDetails_SeviceCatId",
                table: "BillDetails",
                newName: "IX_BillDetails_SeviceId");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Sevices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ed4d2b30-be60-4c26-944a-a44a0337cd8c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5737dfae-50a7-486d-949a-bf436f481487");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "dbf33b53-c2c9-4b5e-9815-14022f5b6466");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0a929677-52ce-4bff-922b-c4e07e50a96c", "AOhFoENeAPMjltHLkWUBkRgPRJHxU1x/NqlyV37YjD/zuLertiQh+uEIJmP4OzehCw==", "1e16d41a-eb04-4e9e-94e4-1e0538601455" });

            migrationBuilder.UpdateData(
                table: "Sevices",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 10000m);

            migrationBuilder.UpdateData(
                table: "Sevices",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 10000m);

            migrationBuilder.UpdateData(
                table: "Sevices",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 10000m);

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_Sevices_SeviceId",
                table: "BillDetails",
                column: "SeviceId",
                principalTable: "Sevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
