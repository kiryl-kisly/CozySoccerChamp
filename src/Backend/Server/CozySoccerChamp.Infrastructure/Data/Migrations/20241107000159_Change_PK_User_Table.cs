using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozySoccerChamp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Change_PK_User_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Predictions_Users_UserId",
                table: "Predictions");

            migrationBuilder.DropIndex(
                name: "IX_Predictions_UserId",
                table: "Predictions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Predictions");

            migrationBuilder.AddForeignKey(
                name: "FK_Predictions_Users_TelegramUserId",
                table: "Predictions",
                column: "TelegramUserId",
                principalTable: "Users",
                principalColumn: "TelegramUserId",
                onDelete: ReferentialAction.Cascade);
            
            migrationBuilder.Sql("SELECT setval('Users_Id_seq', (SELECT MAX(Id) FROM Users));");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Predictions_Users_TelegramUserId",
                table: "Predictions");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Predictions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_UserId",
                table: "Predictions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Predictions_Users_UserId",
                table: "Predictions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
