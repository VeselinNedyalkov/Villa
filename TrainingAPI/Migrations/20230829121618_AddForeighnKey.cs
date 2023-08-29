using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForeighnKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaID",
                table: "VillaNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2023, 8, 29, 15, 16, 18, 371, DateTimeKind.Local).AddTicks(2357), new DateTime(2023, 8, 29, 15, 16, 18, 371, DateTimeKind.Local).AddTicks(2401) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2023, 8, 29, 15, 16, 18, 371, DateTimeKind.Local).AddTicks(2404), new DateTime(2023, 8, 29, 15, 16, 18, 371, DateTimeKind.Local).AddTicks(2406) });

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumbers_VillaID",
                table: "VillaNumbers",
                column: "VillaID");

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumbers_Villas_VillaID",
                table: "VillaNumbers",
                column: "VillaID",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumbers_Villas_VillaID",
                table: "VillaNumbers");

            migrationBuilder.DropIndex(
                name: "IX_VillaNumbers_VillaID",
                table: "VillaNumbers");

            migrationBuilder.DropColumn(
                name: "VillaID",
                table: "VillaNumbers");

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
    }
}
