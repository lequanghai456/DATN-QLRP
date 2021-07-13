using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class updateMovieUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieUser_Movies_RateId",
                table: "MovieUser");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieUser_Users_RatesId",
                table: "MovieUser");

            migrationBuilder.RenameColumn(
                name: "RatesId",
                table: "MovieUser",
                newName: "RatedUsersId");

            migrationBuilder.RenameColumn(
                name: "RateId",
                table: "MovieUser",
                newName: "MoviesRatedId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieUser_RatesId",
                table: "MovieUser",
                newName: "IX_MovieUser_RatedUsersId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "cfe4b557-d14d-4183-b387-5cb2a3a896f5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "65ec4011-2dfb-40d8-aeb7-e64196451984");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "1b3949bf-856b-4b55-a3d7-478fd8eb71df");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3ce37990-4348-436e-a04b-f3cd5f159261", "AIiUyy+dCW9m8LXVlrLSRngQD3QPxly6DQ4BlilPe3vQgMSsESxJcs99BTCSJNg3XA==", "d0d8e000-49dd-4dd5-8a2a-d7c03b4ff7a0" });

            migrationBuilder.AddForeignKey(
                name: "FK_MovieUser_Movies_MoviesRatedId",
                table: "MovieUser",
                column: "MoviesRatedId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieUser_Users_RatedUsersId",
                table: "MovieUser",
                column: "RatedUsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieUser_Movies_MoviesRatedId",
                table: "MovieUser");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieUser_Users_RatedUsersId",
                table: "MovieUser");

            migrationBuilder.RenameColumn(
                name: "RatedUsersId",
                table: "MovieUser",
                newName: "RatesId");

            migrationBuilder.RenameColumn(
                name: "MoviesRatedId",
                table: "MovieUser",
                newName: "RateId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieUser_RatedUsersId",
                table: "MovieUser",
                newName: "IX_MovieUser_RatesId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_MovieUser_Movies_RateId",
                table: "MovieUser",
                column: "RateId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieUser_Users_RatesId",
                table: "MovieUser",
                column: "RatesId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
