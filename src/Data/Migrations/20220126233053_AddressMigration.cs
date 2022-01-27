using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddressMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    FullName = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false),
                    Phone = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    PostalCode = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    State = table.Column<string>(type: "varchar(35)", maxLength: 35, nullable: false),
                    City = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false),
                    District = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false),
                    Street = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false),
                    AddressNumber = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: false),
                    Additional = table.Column<string>(type: "varchar(35)", maxLength: 35, nullable: true),
                    Notes = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("01b168fe-810b-432d-9010-233ba0b380e9"),
                column: "ConcurrencyStamp",
                value: "a636b7ff-857a-4e7e-8f6e-6b99b1818857");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2301d884-221a-4e7d-b509-0113dcc043e1"),
                column: "ConcurrencyStamp",
                value: "32271f22-7d6a-4868-a9ee-271672f31cdd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("78a7570f-3ce5-48ba-9461-80283ed1d94d"),
                column: "ConcurrencyStamp",
                value: "b78a63d9-3b5e-4112-8f40-48a66e372cd7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "0fefd168-0ca8-4dfd-beee-7a5638164516", new DateTime(2022, 1, 26, 23, 30, 52, 957, DateTimeKind.Utc).AddTicks(2809), "AQAAAAEAACcQAAAAEFe/wK02eYy8jNj93XFGer0+bqfkHC4Ri1PnAx1CkNNQfg/gsJ3kgQh+tQ+JWkhnJg==" });

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                table: "Address",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("01b168fe-810b-432d-9010-233ba0b380e9"),
                column: "ConcurrencyStamp",
                value: "044f4b06-a804-462b-afe0-c38e855e2501");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2301d884-221a-4e7d-b509-0113dcc043e1"),
                column: "ConcurrencyStamp",
                value: "10eab91b-7248-4327-94fb-7d839a4a94c1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("78a7570f-3ce5-48ba-9461-80283ed1d94d"),
                column: "ConcurrencyStamp",
                value: "f3c31595-4092-49eb-bc62-a48f50aa6373");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b22698b8-42a2-4115-9631-1c2d1e2ac5f7"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "3310e7d1-d2cb-457b-a28c-d002c56b4d19", new DateTime(2022, 1, 26, 17, 58, 33, 713, DateTimeKind.Utc).AddTicks(5087), "AQAAAAEAACcQAAAAENOIV1OrIgcHsyzqVS6as8ZGQEOM8Jo/aoiVLIFtoAtw9VW4VesJpXMSWFRrvRlrZA==" });
        }
    }
}
