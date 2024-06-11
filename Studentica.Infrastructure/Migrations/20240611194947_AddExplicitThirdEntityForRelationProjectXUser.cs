using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Studentica.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExplicitThirdEntityForRelationProjectXUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_XRef_Users_ProjectsData_ProjectsId",
                table: "Projects_XRef_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_XRef_Users_UsersData_MembersId",
                table: "Projects_XRef_Users");

            migrationBuilder.RenameColumn(
                name: "ProjectsId",
                table: "Projects_XRef_Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "MembersId",
                table: "Projects_XRef_Users",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_XRef_Users_ProjectsId",
                table: "Projects_XRef_Users",
                newName: "IX_Projects_XRef_Users_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityId",
                table: "UsersData",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_XRef_Users_ProjectsData_ProjectId",
                table: "Projects_XRef_Users",
                column: "ProjectId",
                principalTable: "ProjectsData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_XRef_Users_UsersData_UserId",
                table: "Projects_XRef_Users",
                column: "UserId",
                principalTable: "UsersData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_XRef_Users_ProjectsData_ProjectId",
                table: "Projects_XRef_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_XRef_Users_UsersData_UserId",
                table: "Projects_XRef_Users");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Projects_XRef_Users",
                newName: "ProjectsId");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Projects_XRef_Users",
                newName: "MembersId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_XRef_Users_UserId",
                table: "Projects_XRef_Users",
                newName: "IX_Projects_XRef_Users_ProjectsId");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdentityId",
                table: "UsersData",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_XRef_Users_ProjectsData_ProjectsId",
                table: "Projects_XRef_Users",
                column: "ProjectsId",
                principalTable: "ProjectsData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_XRef_Users_UsersData_MembersId",
                table: "Projects_XRef_Users",
                column: "MembersId",
                principalTable: "UsersData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
