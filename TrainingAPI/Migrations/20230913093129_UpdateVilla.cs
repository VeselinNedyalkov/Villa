using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainingAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedTime",
                table: "Villas",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedTime",
                table: "Villas",
                newName: "CreatedDate");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 9, 13, 12, 31, 29, 16, DateTimeKind.Local).AddTicks(8229), "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://dotnetmastery.com/bluevillaimages/villa3.jpg", "Royal Villa", 4, 200.0, 550, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 9, 13, 12, 31, 29, 16, DateTimeKind.Local).AddTicks(8429), "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://dotnetmastery.com/bluevillaimages/villa1.jpg", "Premium Pool Villa", 4, 300.0, 550, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedDate" },
                values: new object[,]
                {
                    { 3, "", new DateTime(2023, 9, 13, 12, 31, 29, 16, DateTimeKind.Local).AddTicks(8432), "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://dotnetmastery.com/bluevillaimages/villa4.jpg", "Luxury Pool Villa", 4, 400.0, 750, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "", new DateTime(2023, 9, 13, 12, 31, 29, 16, DateTimeKind.Local).AddTicks(8434), "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://dotnetmastery.com/bluevillaimages/villa5.jpg", "Diamond Villa", 4, 550.0, 900, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "", new DateTime(2023, 9, 13, 12, 31, 29, 16, DateTimeKind.Local).AddTicks(8436), "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.", "https://dotnetmastery.com/bluevillaimages/villa2.jpg", "Diamond Pool Villa", 4, 600.0, 1100, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Villas",
                newName: "UpdatedTime");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Villas",
                newName: "CreatedTime");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedTime", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedTime" },
                values: new object[] { new DateTime(2023, 9, 5, 11, 24, 41, 884, DateTimeKind.Local).AddTicks(8624), "Nice villa", "https://www.myluxoria.com/storage/app/uploads/public/630/77d/1e4/63077d1e4e7a2970728706.jpg", "Pool View", 8, 5.0, 100, new DateTime(2023, 9, 5, 11, 24, 41, 884, DateTimeKind.Local).AddTicks(8668) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedTime", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedTime" },
                values: new object[] { new DateTime(2023, 9, 5, 11, 24, 41, 884, DateTimeKind.Local).AddTicks(8672), "10 people vilal with good villa", "https://media.architecturaldigest.com/photos/61b24b1bdf5163297d83ae8c/4:3/w_3763,h_2822,c_limit/Stella_Maris_Exterior.jpg", "Sea view", 15, 5.0, 120, new DateTime(2023, 9, 5, 11, 24, 41, 884, DateTimeKind.Local).AddTicks(8673) });
        }
    }
}
