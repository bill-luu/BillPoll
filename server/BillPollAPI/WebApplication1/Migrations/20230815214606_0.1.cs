using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class _01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Option_Polls_PollId",
                table: "Option");

            migrationBuilder.AlterColumn<string>(
                name: "PollId",
                table: "Option",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Option_Polls_PollId",
                table: "Option",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Option_Polls_PollId",
                table: "Option");

            migrationBuilder.AlterColumn<string>(
                name: "PollId",
                table: "Option",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Option_Polls_PollId",
                table: "Option",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id");
        }
    }
}
