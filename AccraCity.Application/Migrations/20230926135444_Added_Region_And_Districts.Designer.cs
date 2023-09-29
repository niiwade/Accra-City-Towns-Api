﻿// <auto-generated />
using System;
using System.Collections.Generic;
using AccraCity.Application.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AccraCity.Application.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230926135444_Added_Region_And_Districts")]
    partial class Added_Region_And_Districts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AccraCity.Application.Models.District", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DistrictName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("AccraCity.Application.Models.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("RegionName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("AccraCity.Application.Models.Town", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("DistrictId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastModifiedDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("Latitude")
                        .HasColumnType("real");

                    b.Property<float>("Longitude")
                        .HasColumnType("real");

                    b.Property<List<string>>("NearbyTowns")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<List<string>>("NotableLandMarks")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<int>("Population")
                        .HasColumnType("integer");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TownName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.HasIndex("RegionId");

                    b.ToTable("Town");
                });

            modelBuilder.Entity("AccraCity.Application.Models.District", b =>
                {
                    b.HasOne("AccraCity.Application.Models.Region", "Region")
                        .WithMany("Districts")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("AccraCity.Application.Models.Town", b =>
                {
                    b.HasOne("AccraCity.Application.Models.District", "District")
                        .WithMany("Towns")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccraCity.Application.Models.Region", "Region")
                        .WithMany("Towns")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("District");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("AccraCity.Application.Models.District", b =>
                {
                    b.Navigation("Towns");
                });

            modelBuilder.Entity("AccraCity.Application.Models.Region", b =>
                {
                    b.Navigation("Districts");

                    b.Navigation("Towns");
                });
#pragma warning restore 612, 618
        }
    }
}