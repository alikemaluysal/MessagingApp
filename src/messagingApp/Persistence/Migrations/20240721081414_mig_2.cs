using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2e5eebaa-e56a-472c-94b2-eaf0f0215901"));

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Email", "IsVerified", "Nickname", "PasswordHash", "PasswordSalt", "ProfileImageIdentifier", "UpdatedDate", "VerificationCode" },
                values: new object[] { new Guid("1eb069b7-58f3-44e4-bb35-209f695ca12b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@mail.com", false, "Admin", new byte[] { 152, 20, 213, 175, 100, 223, 142, 172, 252, 7, 69, 31, 65, 220, 12, 126, 51, 46, 23, 79, 106, 230, 139, 106, 68, 109, 160, 39, 148, 96, 195, 117, 9, 92, 120, 195, 35, 138, 235, 193, 189, 214, 166, 222, 51, 254, 247, 89, 85, 151, 213, 91, 187, 176, 122, 179, 246, 80, 18, 233, 244, 175, 61, 59 }, new byte[] { 45, 236, 223, 214, 238, 23, 245, 120, 114, 88, 193, 31, 192, 22, 59, 161, 155, 169, 248, 136, 69, 249, 150, 60, 217, 215, 32, 57, 103, 207, 63, 219, 89, 49, 156, 231, 233, 56, 9, 56, 9, 100, 237, 49, 158, 44, 131, 16, 19, 180, 217, 18, 177, 105, 113, 185, 213, 7, 213, 160, 103, 184, 114, 202, 189, 218, 167, 50, 99, 14, 92, 28, 152, 195, 57, 102, 2, 26, 189, 155, 28, 241, 150, 100, 213, 185, 111, 78, 146, 52, 68, 5, 142, 154, 186, 12, 215, 125, 89, 253, 154, 228, 140, 203, 97, 106, 18, 19, 240, 110, 1, 193, 91, 221, 161, 76, 165, 148, 225, 130, 184, 191, 70, 111, 133, 66, 186, 122 }, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId_RoleId",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1eb069b7-58f3-44e4-bb35-209f695ca12b"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Email", "IsVerified", "Nickname", "PasswordHash", "PasswordSalt", "ProfileImageIdentifier", "UpdatedDate", "VerificationCode" },
                values: new object[] { new Guid("2e5eebaa-e56a-472c-94b2-eaf0f0215901"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@mail.com", false, "Admin", new byte[] { 155, 74, 162, 251, 141, 44, 200, 154, 57, 213, 77, 254, 94, 93, 156, 244, 22, 171, 210, 68, 43, 61, 72, 191, 40, 98, 122, 41, 113, 23, 67, 158, 15, 115, 7, 157, 51, 65, 139, 100, 156, 167, 64, 170, 162, 83, 113, 136, 231, 149, 218, 77, 63, 7, 15, 17, 194, 213, 78, 102, 156, 175, 116, 53 }, new byte[] { 192, 42, 82, 60, 200, 221, 226, 194, 102, 18, 246, 230, 67, 37, 3, 26, 252, 251, 43, 51, 190, 165, 214, 200, 210, 207, 89, 200, 86, 68, 197, 148, 152, 57, 89, 125, 246, 165, 84, 101, 183, 10, 164, 105, 170, 157, 188, 43, 176, 191, 144, 232, 211, 34, 172, 109, 139, 179, 117, 120, 158, 22, 47, 217, 113, 174, 192, 118, 59, 117, 198, 241, 218, 143, 131, 228, 123, 144, 54, 50, 143, 131, 55, 9, 121, 240, 214, 30, 73, 255, 186, 162, 181, 96, 190, 49, 230, 128, 47, 224, 98, 193, 22, 108, 129, 147, 33, 104, 130, 225, 230, 222, 3, 205, 53, 204, 66, 202, 238, 146, 150, 66, 29, 150, 40, 117, 172, 85 }, null, null, null });
        }
    }
}
