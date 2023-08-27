using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LosBarriosEvents.Data.Migrations
{
    /// <inheritdoc />
    public partial class SpeakerModelUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Speakers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Speakers",
                newName: "Bio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Speakers",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "Speakers",
                newName: "firstName");
        }
    }
}
