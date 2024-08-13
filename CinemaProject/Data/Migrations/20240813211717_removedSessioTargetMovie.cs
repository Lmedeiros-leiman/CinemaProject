using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaProject.Migrations
{
    /// <inheritdoc />
    public partial class removedSessioTargetMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Movies_TargetMovieId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_TargetMovieId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "TargetMovieId",
                table: "Sessions");

            migrationBuilder.AddColumn<long>(
                name: "MovieId",
                table: "Sessions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_MovieId",
                table: "Sessions",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Movies_MovieId",
                table: "Sessions",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Movies_MovieId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_MovieId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Sessions");

            migrationBuilder.AddColumn<long>(
                name: "TargetMovieId",
                table: "Sessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_TargetMovieId",
                table: "Sessions",
                column: "TargetMovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Movies_TargetMovieId",
                table: "Sessions",
                column: "TargetMovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
