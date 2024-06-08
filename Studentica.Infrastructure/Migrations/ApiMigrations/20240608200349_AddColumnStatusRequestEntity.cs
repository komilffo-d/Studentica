using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Studentica.Infrastructure.Migrations.ApiMigrations
{
    /// <inheritdoc />
    public partial class AddColumnStatusRequestEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusRequest",
                table: "Requests",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusRequest",
                table: "Requests");
        }
    }
}
