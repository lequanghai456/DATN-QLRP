using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class fixseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "99635a72-f73e-40de-9d1c-22b8b11c746c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "14be720c-0a2a-4af1-926c-382938bde83d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b497f97-be69-4220-aaa9-d78de64aeae5", "AJfS1Gwqp/ZQQwtvXIWnEOiTlyyFz/CwzCJzeSpl6xRM4JCGrSTWGjxXRa98gVMU4w==", "6c527002-4dca-4f15-997f-128f3fd70dde" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7dffdae2-b647-4791-9323-367b29d701ee", "ANqZeh9tJhaFVLCT0qamj+r7uOLAVhYbwGcyyTfiL6AvmPvXmdhQn7BAM0iey9qTJw==", "a9922bf7-997c-4490-97c5-a1ccf767b552" });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4,
                column: "Trailer",
                value: "4.mp4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "20111ce9-70cc-4a42-b52d-1a3e6345e4f1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "6b7e7fdb-437c-4b49-91e0-8551d14efe91");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c800d189-5bfa-44b4-8562-f34ed625ecae", "AIOQbfZnv13lkBGAbHC1Byg0uQj7+rJYE/dZIWU0X3MAaZFBhbIR3cuvLcRWO7dE4g==", "5034fd11-9214-425f-846d-b7ccb7aa1b49" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a30017aa-7d32-4c0f-bc5c-f07dca53f7da", "AGx0Q74WiPrkVLIma2NXQQG+x0QB5L5pJUlvU6Rfwut/TuicnZOczs9bcK1+STJXhA==", "35d23161-71c2-4b3d-b3d7-dce0d530dba0" });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4,
                column: "Trailer",
                value: "3.mp4");
        }
    }
}
