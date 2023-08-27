using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LosBarriosEvents.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedKeywords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Keywords",
                table: "SpeakerForms",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Keywords",
                table: "SpeakerForms");
        }
    }
}
