using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Studentica.Infrastructure.Migrations.ApiMigrations
{
    /// <inheritdoc />
    public partial class AddNameUniversityUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameUniversity",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameUniversity",
                table: "Users");
        }
    }
}
