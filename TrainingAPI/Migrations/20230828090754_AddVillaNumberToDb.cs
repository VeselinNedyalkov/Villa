using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddVillaNumberToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VillaNumbers",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    SpecialDeails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VillaNumbers", x => x.VillaNo);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2023, 8, 28, 12, 7, 54, 749, DateTimeKind.Local).AddTicks(8260), new DateTime(2023, 8, 28, 12, 7, 54, 749, DateTimeKind.Local).AddTicks(8302) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2023, 8, 28, 12, 7, 54, 749, DateTimeKind.Local).AddTicks(8306), new DateTime(2023, 8, 28, 12, 7, 54, 749, DateTimeKind.Local).AddTicks(8307) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VillaNumbers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2023, 7, 11, 11, 13, 28, 102, DateTimeKind.Local).AddTicks(2316), new DateTime(2023, 7, 11, 11, 13, 28, 102, DateTimeKind.Local).AddTicks(2357) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2023, 7, 11, 11, 13, 28, 102, DateTimeKind.Local).AddTicks(2360), new DateTime(2023, 7, 11, 11, 13, 28, 102, DateTimeKind.Local).AddTicks(2361) });
        }
    }
}
