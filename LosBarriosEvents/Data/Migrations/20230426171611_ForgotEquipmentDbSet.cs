using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LosBarriosEvents.Data.Migrations
{
    /// <inheritdoc />
    public partial class ForgotEquipmentDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpeakerFormEquipment_Equipment_EquipmentId",
                table: "SpeakerFormEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_SpeakerFormEquipment_SpeakerForms_SpeakerFormId",
                table: "SpeakerFormEquipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpeakerFormEquipment",
                table: "SpeakerFormEquipment");

            migrationBuilder.RenameTable(
                name: "SpeakerFormEquipment",
                newName: "SpeakerFormEquipment");

            migrationBuilder.RenameIndex(
                name: "IX_SpeakerFormEquipment_EquipmentId",
                table: "SpeakerFormEquipment",
                newName: "IX_SpeakerFormEquipment_EquipmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpeakerFormEquipment",
                table: "SpeakerFormEquipment",
                columns: new[] { "SpeakerFormId", "EquipmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SpeakerFormEquipment_Equipment_EquipmentId",
                table: "SpeakerFormEquipment",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "EquipmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpeakerFormEquipment_SpeakerForms_SpeakerFormId",
                table: "SpeakerFormEquipment",
                column: "SpeakerFormId",
                principalTable: "SpeakerForms",
                principalColumn: "SpeakerFormId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpeakerFormEquipment_Equipment_EquipmentId",
                table: "SpeakerFormEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_SpeakerFormEquipment_SpeakerForms_SpeakerFormId",
                table: "SpeakerFormEquipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpeakerFormEquipment",
                table: "SpeakerFormEquipment");

            migrationBuilder.RenameTable(
                name: "SpeakerFormEquipment",
                newName: "SpeakerFormEquipment");

            migrationBuilder.RenameIndex(
                name: "IX_SpeakerFormEquipment_EquipmentId",
                table: "SpeakerFormEquipment",
                newName: "IX_SpeakerFormEquipment_EquipmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpeakerFormEquipment",
                table: "SpeakerFormEquipment",
                columns: new[] { "SpeakerFormId", "EquipmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_SpeakerFormEquipment_Equipment_EquipmentId",
                table: "SpeakerFormEquipment",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "EquipmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpeakerFormEquipment_SpeakerForms_SpeakerFormId",
                table: "SpeakerFormEquipment",
                column: "SpeakerFormId",
                principalTable: "SpeakerForms",
                principalColumn: "SpeakerFormId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
