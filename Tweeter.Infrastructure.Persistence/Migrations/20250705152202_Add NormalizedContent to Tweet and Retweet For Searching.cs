using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tweeter.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNormalizedContenttoTweetandRetweetForSearching : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RetweetId",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedContent",
                table: "Tweets",
                type: "nvarchar(max)",
                nullable: true,
                computedColumnSql: "UPPER(Content)",
                stored: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedComment",
                table: "Retweets",
                type: "nvarchar(max)",
                nullable: true,
                computedColumnSql: "UPPER(Comment)",
                stored: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_RetweetId",
                table: "Notifications",
                column: "RetweetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Retweets_RetweetId",
                table: "Notifications",
                column: "RetweetId",
                principalTable: "Retweets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Retweets_RetweetId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_RetweetId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "NormalizedContent",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "NormalizedComment",
                table: "Retweets");

            migrationBuilder.DropColumn(
                name: "RetweetId",
                table: "Notifications");
        }
    }
}
