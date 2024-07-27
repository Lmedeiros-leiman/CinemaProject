using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_Movies_MovieId",
                table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Attachment_MoviePosterImageId",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attachment",
                table: "Attachment");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_MovieId",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "MovieReleaseDate",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Attachment");

            migrationBuilder.RenameTable(
                name: "Attachment",
                newName: "Attachments");

            migrationBuilder.RenameColumn(
                name: "MoviePosterImageId",
                table: "Movies",
                newName: "PosterImageId");

            migrationBuilder.RenameColumn(
                name: "MovieName",
                table: "Movies",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "MovieDescription",
                table: "Movies",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_MoviePosterImageId",
                table: "Movies",
                newName: "IX_Movies_PosterImageId");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Movies",
                type: "datetime2",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "URLPath",
                table: "Attachments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Attachments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Attachments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attachments",
                table: "Attachments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Attachments_PosterImageId",
                table: "Movies",
                column: "PosterImageId",
                principalTable: "Attachments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Attachments_PosterImageId",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attachments",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Attachments");

            migrationBuilder.RenameTable(
                name: "Attachments",
                newName: "Attachment");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Movies",
                newName: "MovieName");

            migrationBuilder.RenameColumn(
                name: "PosterImageId",
                table: "Movies",
                newName: "MoviePosterImageId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Movies",
                newName: "MovieDescription");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_PosterImageId",
                table: "Movies",
                newName: "IX_Movies_MoviePosterImageId");

            migrationBuilder.AddColumn<DateTime>(
                name: "MovieReleaseDate",
                table: "Movies",
                type: "datetime2",
                rowVersion: true,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "URLPath",
                table: "Attachment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DataType",
                table: "Attachment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Attachment",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attachment",
                table: "Attachment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_MovieId",
                table: "Attachment",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_Movies_MovieId",
                table: "Attachment",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Attachment_MoviePosterImageId",
                table: "Movies",
                column: "MoviePosterImageId",
                principalTable: "Attachment",
                principalColumn: "Id");
        }
    }
}
