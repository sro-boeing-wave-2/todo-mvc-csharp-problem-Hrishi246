using Microsoft.EntityFrameworkCore.Migrations;

namespace Googlekeep.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_checklist_Note_NoteID",
                table: "checklist");

            migrationBuilder.DropForeignKey(
                name: "FK_label_Note_NoteID",
                table: "label");

            migrationBuilder.AlterColumn<int>(
                name: "NoteID",
                table: "label",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "NoteID",
                table: "checklist",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_checklist_Note_NoteID",
                table: "checklist",
                column: "NoteID",
                principalTable: "Note",
                principalColumn: "NoteID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_label_Note_NoteID",
                table: "label",
                column: "NoteID",
                principalTable: "Note",
                principalColumn: "NoteID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_checklist_Note_NoteID",
                table: "checklist");

            migrationBuilder.DropForeignKey(
                name: "FK_label_Note_NoteID",
                table: "label");

            migrationBuilder.AlterColumn<int>(
                name: "NoteID",
                table: "label",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoteID",
                table: "checklist",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_checklist_Note_NoteID",
                table: "checklist",
                column: "NoteID",
                principalTable: "Note",
                principalColumn: "NoteID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_label_Note_NoteID",
                table: "label",
                column: "NoteID",
                principalTable: "Note",
                principalColumn: "NoteID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
