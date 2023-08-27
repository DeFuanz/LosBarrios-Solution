using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LosBarriosEvents.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatesessionspeakerwithformid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormIdAssigned",
                table: "sessionSpeakers",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormIdAssigned",
                table: "sessionSpeakers");
        }
    }
}
