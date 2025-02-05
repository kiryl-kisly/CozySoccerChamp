using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozySoccerChamp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialValueToUserProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO ""UserProfiles"" (""TelegramUserId"", ""IsEnabledNotification"", ""LastNotified"")
                SELECT ""TelegramUserId"", TRUE, NULL FROM ""Users"" 
                WHERE ""TelegramUserId"" NOT IN (SELECT ""TelegramUserId"" FROM ""UserProfiles"");
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
