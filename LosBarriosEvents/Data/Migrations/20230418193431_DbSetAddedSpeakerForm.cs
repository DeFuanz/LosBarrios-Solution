using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LosBarriosEvents.Data.Migrations
{
    /// <inheritdoc />
    public partial class DbSetAddedSpeakerForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpeakerForm_Speakers_SpeakerId",
                table: "SpeakerForm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpeakerForm",
                table: "SpeakerForm");

            migrationBuilder.RenameTable(
                name: "SpeakerForm",
                newName: "SpeakerForms");

            migrationBuilder.RenameIndex(
                name: "IX_SpeakerForm_SpeakerId",
                table: "SpeakerForms",
                newName: "IX_SpeakerForms_SpeakerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpeakerForms",
                table: "SpeakerForms",
                column: "SpeakerFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpeakerForms_Speakers_SpeakerId",
                table: "SpeakerForms",
                column: "SpeakerId",
                principalTable: "Speakers",
                principalColumn: "SpeakerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpeakerForms_Speakers_SpeakerId",
                table: "SpeakerForms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpeakerForms",
                table: "SpeakerForms");

            migrationBuilder.RenameTable(
                name: "SpeakerForms",
                newName: "SpeakerForm");

            migrationBuilder.RenameIndex(
                name: "IX_SpeakerForms_SpeakerId",
                table: "SpeakerForm",
                newName: "IX_SpeakerForm_SpeakerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpeakerForm",
                table: "SpeakerForm",
                column: "SpeakerFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpeakerForm_Speakers_SpeakerId",
                table: "SpeakerForm",
                column: "SpeakerId",
                principalTable: "Speakers",
                principalColumn: "SpeakerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
