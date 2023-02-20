using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapinews.Migrations
{
    /// <inheritdoc />
    public partial class Updatemigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookMark_News",
                table: "BookMark");

            migrationBuilder.DropForeignKey(
                name: "FK_BookMark_Users",
                table: "BookMark");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookMark",
                table: "BookMark");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "News");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "BookMark");

            migrationBuilder.RenameTable(
                name: "BookMark",
                newName: "BookMarks");

            migrationBuilder.RenameIndex(
                name: "IX_BookMark_UserId",
                table: "BookMarks",
                newName: "IX_BookMarks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BookMark_NewsId",
                table: "BookMarks",
                newName: "IX_BookMarks_NewsId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Aurthor",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookMarks",
                table: "BookMarks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookMarks_News_NewsId",
                table: "BookMarks",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookMarks_Users_UserId",
                table: "BookMarks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookMarks_News_NewsId",
                table: "BookMarks");

            migrationBuilder.DropForeignKey(
                name: "FK_BookMarks_Users_UserId",
                table: "BookMarks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookMarks",
                table: "BookMarks");

            migrationBuilder.RenameTable(
                name: "BookMarks",
                newName: "BookMark");

            migrationBuilder.RenameIndex(
                name: "IX_BookMarks_UserId",
                table: "BookMark",
                newName: "IX_BookMark_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BookMarks_NewsId",
                table: "BookMark",
                newName: "IX_BookMark_NewsId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "News",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Aurthor",
                table: "News",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "News",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "BookMark",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookMark",
                table: "BookMark",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookMark_News",
                table: "BookMark",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookMark_Users",
                table: "BookMark",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
