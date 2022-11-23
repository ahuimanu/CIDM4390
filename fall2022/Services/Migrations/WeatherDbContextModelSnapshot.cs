﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Services.WeatherDataService;

#nullable disable

namespace Services.Migrations
{
    [DbContext(typeof(WeatherDbContext))]
    partial class WeatherDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.11");

            modelBuilder.Entity("Services.AirportInfoService.Airport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Airports", (string)null);
                });

            modelBuilder.Entity("Services.WeatherReportJobService.WeatherReportJob", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("JobActionType")
                        .HasColumnType("INTEGER");

                    b.Property<long>("JobFrequencyInMinutes")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("JobScheduledAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("WeatherReportJobs", (string)null);
                });

            modelBuilder.Entity("Services.WeatherReportJobService.WeatherReportJobResult", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("JobActionType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Observation")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("WeatherReportJobResults", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
