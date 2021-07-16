using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Users_UserId",
                table: "Rates");

            migrationBuilder.DropTable(
                name: "MovieRate");

            migrationBuilder.DropIndex(
                name: "IX_Rates_UserId",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Rates");

            migrationBuilder.AddColumn<int>(
                name: "RateId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MovieUser",
                columns: table => new
                {
                    RateId = table.Column<int>(type: "int", nullable: false),
                    RatesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieUser", x => new { x.RateId, x.RatesId });
                    table.ForeignKey(
                        name: "FK_MovieUser_Movies_RateId",
                        column: x => x.RateId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieUser_Users_RatesId",
                        column: x => x.RatesId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "733cad73-b0ea-446a-9c5a-fa40723a5d8b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "2f5be8ca-c231-4816-b63e-4d572107c75f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "a9322bc7-a498-452c-8c31-f697d6a13f99");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3e003feb-7f0a-4949-8f0f-14c2fe8ac3b2", "AKrdQuuGUWOLy/uKp+DGYwW/fSWD43tuiKpsv52R/c10pKcpScnzJLkh5DHvV73G6Q==", "7d3a8bc1-c857-4e1d-9ac6-891a56a1faf1" });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_RateId",
                table: "Movies",
                column: "RateId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieUser_RatesId",
                table: "MovieUser",
                column: "RatesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Rates_RateId",
                table: "Movies",
                column: "RateId",
                principalTable: "Rates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Rates_RateId",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "MovieUser");

            migrationBuilder.DropIndex(
                name: "IX_Movies_RateId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "RateId",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Rates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MovieRate",
                columns: table => new
                {
                    MoviesId = table.Column<int>(type: "int", nullable: false),
                    RatesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieRate", x => new { x.MoviesId, x.RatesId });
                    table.ForeignKey(
                        name: "FK_MovieRate_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieRate_Rates_RatesId",
                        column: x => x.RatesId,
                        principalTable: "Rates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "89e65f5f-4e54-4d26-8c2c-7d98bef84a5a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a9208eea-daeb-44a1-bf68-d5ca6bb8f37e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "6404a3d5-4764-4dda-9611-5acfb1310e7d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f3765000-cb46-4589-8995-634afd61cede", "APCECMgqwgSvyY/Dup6jPrO2+lrdpUqZhhmVsh7ZI+TSxmk/Zon4fMuEdlIkIFyiag==", "10d187ae-b20e-44b0-8f55-389720a8757c" });

            migrationBuilder.CreateIndex(
                name: "IX_Rates_UserId",
                table: "Rates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieRate_RatesId",
                table: "MovieRate",
                column: "RatesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Users_UserId",
                table: "Rates",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
