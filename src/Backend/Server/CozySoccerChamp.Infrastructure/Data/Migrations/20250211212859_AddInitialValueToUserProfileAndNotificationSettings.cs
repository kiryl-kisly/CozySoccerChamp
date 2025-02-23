using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozySoccerChamp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialValueToUserProfileAndNotificationSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Добавляем UserProfile для существующих пользователей
            migrationBuilder.Sql(@"
                INSERT INTO ""UserProfiles"" (""TelegramUserId"")
                SELECT ""TelegramUserId"" FROM ""Users""
                WHERE ""TelegramUserId"" NOT IN (SELECT ""TelegramUserId"" FROM ""UserProfiles"");
            ");

            // Добавляем NotificationSettings для существующих пользователей
            migrationBuilder.Sql(@"
                INSERT INTO ""NotificationSettings"" (""TelegramUserId"", ""IsAnnouncementEnabled"", ""IsReminderEnabled"", ""ReminderInterval"", ""IsForceNotificationEnabled"", ""LastReminderNotified"")
                SELECT ""TelegramUserId"", TRUE, TRUE, 60, FALSE, NULL FROM ""Users""
                WHERE ""TelegramUserId"" NOT IN (SELECT ""TelegramUserId"" FROM ""NotificationSettings"");
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM ""UserProfiles"";");
            migrationBuilder.Sql(@"DELETE FROM ""NotificationSettings"";");
        }
    }
}
