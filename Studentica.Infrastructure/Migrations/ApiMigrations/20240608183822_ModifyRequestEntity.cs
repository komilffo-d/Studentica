using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Studentica.Infrastructure.Migrations.ApiMigrations
{
    /// <inheritdoc />
    public partial class ModifyRequestEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "Requests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "NumberRequest",
                table: "Requests",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "NumberRequest",
                table: "Requests");
        }
    }
}
