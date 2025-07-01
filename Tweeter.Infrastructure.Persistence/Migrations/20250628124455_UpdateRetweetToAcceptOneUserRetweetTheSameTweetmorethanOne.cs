using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tweeter.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRetweetToAcceptOneUserRetweetTheSameTweetmorethanOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Retweets_UserId_OriginalTweetId",
                table: "Retweets");

            migrationBuilder.CreateIndex(
                name: "IX_Retweets_UserId",
                table: "Retweets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Retweets_UserId",
                table: "Retweets");

            migrationBuilder.CreateIndex(
                name: "IX_Retweets_UserId_OriginalTweetId",
                table: "Retweets",
                columns: new[] { "UserId", "OriginalTweetId" },
                unique: true);
        }
    }
}
