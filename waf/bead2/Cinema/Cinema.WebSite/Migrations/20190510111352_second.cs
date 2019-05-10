using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinema.WebSite.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Movies_Movies",
                table: "Shows");

            migrationBuilder.DropIndex(
                name: "IX_Shows_Movies",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "Movies",
                table: "Shows");

            migrationBuilder.AddColumn<int>(
                name: "MovieRefId",
                table: "Shows",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Shows_MovieRefId",
                table: "Shows",
                column: "MovieRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Movies_MovieRefId",
                table: "Shows",
                column: "MovieRefId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Movies_MovieRefId",
                table: "Shows");

            migrationBuilder.DropIndex(
                name: "IX_Shows_MovieRefId",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "MovieRefId",
                table: "Shows");

            migrationBuilder.AddColumn<int>(
                name: "Movies",
                table: "Shows",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shows_Movies",
                table: "Shows",
                column: "Movies");

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Movies_Movies",
                table: "Shows",
                column: "Movies",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
