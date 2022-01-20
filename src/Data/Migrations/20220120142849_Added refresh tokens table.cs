using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Addedrefreshtokenstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("482fbb1f-113e-4c57-901c-080b9df86f7e"));

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: true),
                    Token = table.Column<string>(type: "longtext", nullable: true),
                    JwtId = table.Column<string>(type: "longtext", nullable: true),
                    IsUsed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsRevoked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreateAt", "Email", "Name", "UpdateAt" },
                values: new object[] { new Guid("26778e25-abe0-4fe0-a40b-e8bba6b538b6"), new DateTime(2022, 1, 20, 11, 28, 48, 2, DateTimeKind.Local).AddTicks(2984), "admin@mail.com", "Administrador", new DateTime(2022, 1, 20, 11, 28, 48, 15, DateTimeKind.Local).AddTicks(2243) });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("26778e25-abe0-4fe0-a40b-e8bba6b538b6"));

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreateAt", "Email", "Name", "UpdateAt" },
                values: new object[] { new Guid("482fbb1f-113e-4c57-901c-080b9df86f7e"), new DateTime(2022, 1, 19, 16, 43, 45, 693, DateTimeKind.Local).AddTicks(5924), "admin@mail.com", "Administrador", new DateTime(2022, 1, 19, 16, 43, 45, 695, DateTimeKind.Local).AddTicks(8003) });
        }
    }
}
