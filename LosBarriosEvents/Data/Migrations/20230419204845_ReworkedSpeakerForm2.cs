using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LosBarriosEvents.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReworkedSpeakerForm2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpeakerForms_Speakers_SpeakerId",
                table: "SpeakerForms");

            migrationBuilder.AlterColumn<string>(
                name: "SpeakerId",
                table: "SpeakerForms",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_SpeakerForms_Speakers_SpeakerId",
                table: "SpeakerForms",
                column: "SpeakerId",
                principalTable: "Speakers",
                principalColumn: "SpeakerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpeakerForms_Speakers_SpeakerId",
                table: "SpeakerForms");

            migrationBuilder.AlterColumn<string>(
                name: "SpeakerId",
                table: "SpeakerForms",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SpeakerForms_Speakers_SpeakerId",
                table: "SpeakerForms",
                column: "SpeakerId",
                principalTable: "Speakers",
                principalColumn: "SpeakerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
