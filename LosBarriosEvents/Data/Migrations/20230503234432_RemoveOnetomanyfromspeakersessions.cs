using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LosBarriosEvents.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOnetomanyfromspeakersessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Speakers_Sessions_SessionId",
                table: "Speakers");

            migrationBuilder.DropIndex(
                name: "IX_Speakers_SessionId",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Speakers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Speakers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_SessionId",
                table: "Speakers",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Speakers_Sessions_SessionId",
                table: "Speakers",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "SessionId");
        }
    }
}
