using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLiRapPhim.Migrations
{
    public partial class updateseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SeviceCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6b7e7fdb-437c-4b49-91e0-8551d14efe91", "staff", "STAFF" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c800d189-5bfa-44b4-8562-f34ed625ecae", "AIOQbfZnv13lkBGAbHC1Byg0uQj7+rJYE/dZIWU0X3MAaZFBhbIR3cuvLcRWO7dE4g==", "5034fd11-9214-425f-846d-b7ccb7aa1b49" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FullName", "Img", "IsDelete", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 2, 0, "a30017aa-7d32-4c0f-bc5c-f07dca53f7da", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0306181113@caothang.edu.vn", false, "Lê Quang Hải", "manager1.img", false, false, null, null, "manager1", "AGx0Q74WiPrkVLIma2NXQQG+x0QB5L5pJUlvU6Rfwut/TuicnZOczs9bcK1+STJXhA==", null, false, 2, "35d23161-71c2-4b3d-b3d7-dce0d530dba0", false, "manager1" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "Trinh thám");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Phiêu lưu");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsDelete", "Title" },
                values: new object[,]
                {
                    { 3, false, "Hành động" },
                    { 4, false, "Hoạt hình" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Describe", "IsDelete", "MacId", "Poster", "Status", "Time", "Title", "TotalRating", "TotalReviewers", "Trailer" },
                values: new object[,]
                {
                    { 4, "Cuối thời Nam Tống, Tiểu Bạch vì muốn cứu Hứa Tiên mà dâng nước dìm Kim Sơn tự, cuối cùng bị Pháp Hải nhốt dưới tháp Lôi Phong. Tiểu Thanh bất ngờ bị Pháp Hải đánh rơi vào ảo cảnh của Tu La thành quỷ dị. Mấy lần nguy cấp, Tiểu Thanh được một thiếu niên thần bí cứu, Tiểu Thanh mang theo chấp niệm cứu Tiểu Bạch ra ngoài mà trải qua kiếp nạn rồi trưởng thành, cùng thiếu niên thần bí đi tìm biện pháp.", false, 1, "4.jpg", 0, 132, "BẠCH XÀ 2: THANH XÀ KIẾP KHỞI", 0, 0, "3.mp4" },
                    { 5, "Trước khi Himura Kenshin gặp Kaoru, anh ta là một sát thủ đáng sợ được gọi là Hitokiri Battousai. 'Rurouni Kenshin: The Beginning' kể câu chuyện về một Kenshin trẻ tuổi, người trở thành sát thủ số một cho Ishin Shishi (sau này trở thành 'thời Minh Trị'), người đã chiến đấu chống lại Mạc phủ trong những ngày cuối cùng của thời đại Tokugawa, và làm thế nào anh ấy có dấu 'X' nổi tiếng trên má trái của mình. Trong những ngày đầu của HimuraKenshin trong vai Hitokiri Battousai, một ngày nọ, anh gặp Yukishiro Tomoe, một thiếu nữ xinh đẹp, có vẻ ngoài thanh thoát nhưng luôn mang một khuôn mặt buồn bã. Battousai và Tomoe yêu nhau nhưng ít ai biết rằng, Tomoe mang trong lòng một gánh nặng to lớn sẽ thay đổi cuộc đời của Himura Kenshin mãi mãi.", false, 1, "5.jpg", 0, 140, "LÃNG KHÁCH KENSHIN: KHỞI ĐẦU", 0, 0, "5.mp4" }
                });

            migrationBuilder.InsertData(
                table: "SeviceCategories",
                columns: new[] { "Id", "IsDelete", "Name" },
                values: new object[,]
                {
                    { 1, false, "Big" },
                    { 2, false, "Small" },
                    { 3, false, "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Sevices",
                columns: new[] { "Id", "IsDelete", "IsFood", "Name" },
                values: new object[,]
                {
                    { 1, false, true, "Bắp rang" },
                    { 2, false, false, "CoCa" },
                    { 3, false, false, "Pepsi" }
                });

            migrationBuilder.InsertData(
                table: "seviceSeviceCategories",
                columns: new[] { "Id", "IdSevice", "IdSeviceCategory", "Price", "isDelete" },
                values: new object[,]
                {
                    { 1, 1, 1, 30000m, false },
                    { 4, 2, 1, 10000m, false },
                    { 6, 3, 1, 10000m, false },
                    { 2, 1, 2, 25000m, false },
                    { 5, 2, 2, 30000m, false },
                    { 7, 3, 2, 30000m, false },
                    { 3, 1, 3, 10000m, false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "seviceSeviceCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "seviceSeviceCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "seviceSeviceCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "seviceSeviceCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "seviceSeviceCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "seviceSeviceCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "seviceSeviceCategories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SeviceCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SeviceCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SeviceCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sevices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sevices",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sevices",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SeviceCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ec9fd79b-736b-4a50-9810-19ed828541c3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5159a846-fbe5-4887-9e0e-643756f27fd0", "manager movie", "MANAGER MOVIE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDelete", "Name", "NormalizedName" },
                values: new object[] { 3, "b1f06639-ba2e-41fc-a207-392df9b1e3cd", false, "staff", "STAFF" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "961e34d9-ab37-4f52-97b4-b51a712b96e4", "AP+9g49g4kaPlyC/4odnxkizZMMBVdmDBg/VDcVCJyoG098aVMSCtHG94KfIcnpM0A==", "a007f7c5-cc58-446e-9de9-5ce3590de3e3" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "Detective");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Adventure");
        }
    }
}
