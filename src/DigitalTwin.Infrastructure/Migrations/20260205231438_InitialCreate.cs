using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalTwin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Motors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxOperatingTemperatureCelsius = table.Column<double>(type: "float", nullable: false),
                    MaxRPM = table.Column<double>(type: "float", nullable: false),
                    NominalCurrent = table.Column<double>(type: "float", nullable: false),
                    CurrentTemperature = table.Column<double>(type: "float", nullable: false),
                    CurrentRPM = table.Column<double>(type: "float", nullable: false),
                    VibrationVelocity = table.Column<double>(type: "float", nullable: false),
                    IsRunning = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motors", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Motors");
        }
    }
}
