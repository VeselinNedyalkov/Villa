﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrainingAPI.Data;

#nullable disable

namespace TrainingAPI.Migrations
{
    [DbContext(typeof(AplicationDbContext))]
    [Migration("20230828090754_AddVillaNumberToDb")]
    partial class AddVillaNumberToDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TrainingAPI.Models.DTO.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Occupancy")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("Sqft")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenity = "",
                            CreatedTime = new DateTime(2023, 8, 28, 12, 7, 54, 749, DateTimeKind.Local).AddTicks(8260),
                            Details = "Nice villa",
                            ImageUrl = "https://www.myluxoria.com/storage/app/uploads/public/630/77d/1e4/63077d1e4e7a2970728706.jpg",
                            Name = "Pool View",
                            Occupancy = 8,
                            Rate = 5.0,
                            Sqft = 100,
                            UpdatedTime = new DateTime(2023, 8, 28, 12, 7, 54, 749, DateTimeKind.Local).AddTicks(8302)
                        },
                        new
                        {
                            Id = 2,
                            Amenity = "",
                            CreatedTime = new DateTime(2023, 8, 28, 12, 7, 54, 749, DateTimeKind.Local).AddTicks(8306),
                            Details = "10 people vilal with good villa",
                            ImageUrl = "https://media.architecturaldigest.com/photos/61b24b1bdf5163297d83ae8c/4:3/w_3763,h_2822,c_limit/Stella_Maris_Exterior.jpg",
                            Name = "Sea view",
                            Occupancy = 15,
                            Rate = 5.0,
                            Sqft = 120,
                            UpdatedTime = new DateTime(2023, 8, 28, 12, 7, 54, 749, DateTimeKind.Local).AddTicks(8307)
                        });
                });

            modelBuilder.Entity("TrainingAPI.Models.VillaNumber", b =>
                {
                    b.Property<int>("VillaNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SpecialDeails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("VillaNo");

                    b.ToTable("VillaNumbers");
                });
#pragma warning restore 612, 618
        }
    }
}
