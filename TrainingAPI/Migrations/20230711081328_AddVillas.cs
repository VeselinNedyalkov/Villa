using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainingAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddVillas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Detaisl",
                table: "Villas",
                newName: "Details");

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedTime", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedTime" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2023, 7, 11, 11, 13, 28, 102, DateTimeKind.Local).AddTicks(2316), "Nice villa", "https://www.myluxoria.com/storage/app/uploads/public/630/77d/1e4/63077d1e4e7a2970728706.jpg", "Pool View", 8, 5.0, 100, new DateTime(2023, 7, 11, 11, 13, 28, 102, DateTimeKind.Local).AddTicks(2357) },
                    { 2, "", new DateTime(2023, 7, 11, 11, 13, 28, 102, DateTimeKind.Local).AddTicks(2360), "10 people vilal with good villa", "https://media.architecturaldigest.com/photos/61b24b1bdf5163297d83ae8c/4:3/w_3763,h_2822,c_limit/Stella_Maris_Exterior.jpg", "Sea view", 15, 5.0, 120, new DateTime(2023, 7, 11, 11, 13, 28, 102, DateTimeKind.Local).AddTicks(2361) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "Details",
                table: "Villas",
                newName: "Detaisl");
        }
    }
}
