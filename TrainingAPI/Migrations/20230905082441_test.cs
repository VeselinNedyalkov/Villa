using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingAPI.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2023, 9, 5, 11, 24, 41, 884, DateTimeKind.Local).AddTicks(8624), new DateTime(2023, 9, 5, 11, 24, 41, 884, DateTimeKind.Local).AddTicks(8668) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2023, 9, 5, 11, 24, 41, 884, DateTimeKind.Local).AddTicks(8672), new DateTime(2023, 9, 5, 11, 24, 41, 884, DateTimeKind.Local).AddTicks(8673) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2023, 8, 31, 13, 53, 31, 454, DateTimeKind.Local).AddTicks(1193), new DateTime(2023, 8, 31, 13, 53, 31, 454, DateTimeKind.Local).AddTicks(1234) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2023, 8, 31, 13, 53, 31, 454, DateTimeKind.Local).AddTicks(1238), new DateTime(2023, 8, 31, 13, 53, 31, 454, DateTimeKind.Local).AddTicks(1239) });
        }
    }
}
