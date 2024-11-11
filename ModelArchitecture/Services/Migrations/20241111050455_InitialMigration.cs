using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuestProfileJobResults",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    IsEmailValid = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsEmailSent = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestProfileJobResults", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GuestProfileJobs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    IsEmailValid = table.Column<bool>(type: "INTEGER", nullable: false),
                    JobFrequencyInMinutes = table.Column<int>(type: "INTEGER", nullable: false),
                    JobScheduledAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestProfileJobs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GuestProfiles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Age = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestProfiles", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuestProfileJobResults");

            migrationBuilder.DropTable(
                name: "GuestProfileJobs");

            migrationBuilder.DropTable(
                name: "GuestProfiles");
        }
    }
}
