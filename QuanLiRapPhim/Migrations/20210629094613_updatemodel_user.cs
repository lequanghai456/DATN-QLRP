using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class updatemodel_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ConfirmEmail",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Trailer",
                value: "1.mp4");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Trailer",
                value: "2.mp4");

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Describe", "IsDelete", "MacId", "Poster", "Status", "Time", "Title", "TotalRating", "TotalReviewers", "Trailer" },
                values: new object[] { 3, "Đây là phần tiếp theo của bom tấn vô cùng ăn khách – “G.I. Joe: The Rise of Cobra”. Nội dung phần 2 của “G.I. Joe” bắt đầu khi những người lãnh đạo nước Mỹ bị tổ chức Cobra (kẻ thù không đội trời chung của đội đặc nhiệm G.I.Joe) kiểm soát và ra lệnh loại bỏ G.I.Joe. Toàn bộ nhóm đặc vụ bị gài bẫy và gần như bị xóa sổ. Những người còn sống của đội đặc nhiệm tìm đến sự giúp đỡ của người lãnh đạo G.I. Joe năm xưa – tướng Joe Colton để cùng nhau tìm nguyên nhân thực sự của mọi chuyện và tìm cách giải cứu nước Mỹ.", false, 1, "3.jpg", 0, 110, "BIỆT ĐỘI G.I. JOE: BÁO THÙ", 0, 0, "3.mp4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "ConfirmEmail",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Trailer",
                value: "");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Trailer",
                value: "");
        }
    }
}
