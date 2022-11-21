using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Migrations
{
    public partial class WeatherJobTypeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JobType",
                table: "WeatherReportJobs",
                newName: "JobActionType");

            migrationBuilder.AddColumn<DateTime>(
                name: "JobScheduledAt",
                table: "WeatherReportJobs",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobScheduledAt",
                table: "WeatherReportJobs");

            migrationBuilder.RenameColumn(
                name: "JobActionType",
                table: "WeatherReportJobs",
                newName: "JobType");
        }
    }
}
