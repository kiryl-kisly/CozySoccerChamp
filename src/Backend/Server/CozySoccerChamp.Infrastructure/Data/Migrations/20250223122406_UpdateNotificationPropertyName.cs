using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozySoccerChamp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNotificationPropertyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsReminderEnabled",
                table: "NotificationSettings",
                newName: "IsReminder");

            migrationBuilder.RenameColumn(
                name: "IsForceNotificationEnabled",
                table: "NotificationSettings",
                newName: "IsForceReminder");

            migrationBuilder.RenameColumn(
                name: "IsAnnouncementEnabled",
                table: "NotificationSettings",
                newName: "IsAnnouncement");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsReminder",
                table: "NotificationSettings",
                newName: "IsReminderEnabled");

            migrationBuilder.RenameColumn(
                name: "IsForceReminder",
                table: "NotificationSettings",
                newName: "IsForceNotificationEnabled");

            migrationBuilder.RenameColumn(
                name: "IsAnnouncement",
                table: "NotificationSettings",
                newName: "IsAnnouncementEnabled");
        }
    }
}
