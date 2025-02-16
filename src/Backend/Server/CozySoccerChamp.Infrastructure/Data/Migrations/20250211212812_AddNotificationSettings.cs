using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CozySoccerChamp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_TelegramUserId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "IsEnabledNotification",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "LastNotified",
                table: "UserProfiles");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserProfiles_TelegramUserId",
                table: "UserProfiles",
                column: "TelegramUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "NotificationSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TelegramUserId = table.Column<long>(type: "bigint", nullable: false),
                    IsAnnouncementEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    IsReminderEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    ReminderInterval = table.Column<int>(type: "integer", nullable: false),
                    IsForceNotificationEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LastReminderNotified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationSettings_UserProfiles_TelegramUserId",
                        column: x => x.TelegramUserId,
                        principalTable: "UserProfiles",
                        principalColumn: "TelegramUserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSettings_TelegramUserId",
                table: "NotificationSettings",
                column: "TelegramUserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationSettings");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserProfiles_TelegramUserId",
                table: "UserProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserProfiles");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabledNotification",
                table: "UserProfiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastNotified",
                table: "UserProfiles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles",
                column: "TelegramUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_TelegramUserId",
                table: "UserProfiles",
                column: "TelegramUserId",
                unique: true);
        }
    }
}
