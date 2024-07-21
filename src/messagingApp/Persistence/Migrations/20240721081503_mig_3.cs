using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1eb069b7-58f3-44e4-bb35-209f695ca12b"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Email", "IsVerified", "Nickname", "PasswordHash", "PasswordSalt", "ProfileImageIdentifier", "UpdatedDate", "VerificationCode" },
                values: new object[] { new Guid("fc56ab7c-7222-4736-a3ec-429c50794da3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@mail.com", false, "Admin", new byte[] { 147, 15, 218, 16, 50, 241, 244, 50, 48, 18, 22, 95, 14, 44, 20, 210, 157, 131, 252, 202, 147, 214, 92, 204, 91, 69, 47, 40, 29, 104, 97, 61, 42, 6, 127, 241, 44, 190, 37, 101, 135, 34, 42, 234, 42, 175, 148, 22, 184, 236, 1, 123, 239, 66, 199, 181, 192, 230, 68, 56, 137, 68, 212, 226 }, new byte[] { 50, 129, 29, 255, 145, 194, 127, 81, 233, 47, 180, 158, 53, 34, 103, 129, 163, 190, 234, 91, 76, 253, 74, 132, 68, 88, 166, 89, 160, 43, 156, 218, 178, 150, 224, 0, 184, 249, 49, 52, 95, 185, 21, 217, 204, 176, 238, 219, 173, 132, 48, 53, 21, 206, 207, 244, 95, 101, 190, 161, 88, 104, 216, 105, 159, 100, 10, 89, 119, 140, 31, 249, 125, 119, 119, 240, 244, 153, 99, 206, 128, 237, 85, 219, 226, 0, 189, 241, 182, 10, 8, 15, 183, 215, 202, 147, 181, 101, 50, 224, 183, 51, 112, 97, 20, 45, 4, 168, 243, 75, 75, 78, 191, 162, 27, 152, 189, 125, 21, 161, 104, 50, 226, 9, 129, 221, 136, 104 }, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fc56ab7c-7222-4736-a3ec-429c50794da3"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Email", "IsVerified", "Nickname", "PasswordHash", "PasswordSalt", "ProfileImageIdentifier", "UpdatedDate", "VerificationCode" },
                values: new object[] { new Guid("1eb069b7-58f3-44e4-bb35-209f695ca12b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@mail.com", false, "Admin", new byte[] { 152, 20, 213, 175, 100, 223, 142, 172, 252, 7, 69, 31, 65, 220, 12, 126, 51, 46, 23, 79, 106, 230, 139, 106, 68, 109, 160, 39, 148, 96, 195, 117, 9, 92, 120, 195, 35, 138, 235, 193, 189, 214, 166, 222, 51, 254, 247, 89, 85, 151, 213, 91, 187, 176, 122, 179, 246, 80, 18, 233, 244, 175, 61, 59 }, new byte[] { 45, 236, 223, 214, 238, 23, 245, 120, 114, 88, 193, 31, 192, 22, 59, 161, 155, 169, 248, 136, 69, 249, 150, 60, 217, 215, 32, 57, 103, 207, 63, 219, 89, 49, 156, 231, 233, 56, 9, 56, 9, 100, 237, 49, 158, 44, 131, 16, 19, 180, 217, 18, 177, 105, 113, 185, 213, 7, 213, 160, 103, 184, 114, 202, 189, 218, 167, 50, 99, 14, 92, 28, 152, 195, 57, 102, 2, 26, 189, 155, 28, 241, 150, 100, 213, 185, 111, 78, 146, 52, 68, 5, 142, 154, 186, 12, 215, 125, 89, 253, 154, 228, 140, 203, 97, 106, 18, 19, 240, 110, 1, 193, 91, 221, 161, 76, 165, 148, 225, 130, 184, 191, 70, 111, 133, 66, 186, 122 }, null, null, null });
        }
    }
}
