using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tweeter.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MakeCommentnullableinRetweetcontentnullableinTweetandmakeRetweetLikableandRetweetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RetweetId",
                table: "TweetHashtags",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Retweets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "RetweetId",
                table: "Retweets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RetweetId",
                table: "Replies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RetweetId",
                table: "Mentions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RetweetId",
                table: "Likes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TweetHashtags_RetweetId",
                table: "TweetHashtags",
                column: "RetweetId");

            migrationBuilder.CreateIndex(
                name: "IX_Retweets_RetweetId",
                table: "Retweets",
                column: "RetweetId");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_RetweetId",
                table: "Replies",
                column: "RetweetId");

            migrationBuilder.CreateIndex(
                name: "IX_Mentions_RetweetId",
                table: "Mentions",
                column: "RetweetId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_RetweetId",
                table: "Likes",
                column: "RetweetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Retweets_RetweetId",
                table: "Likes",
                column: "RetweetId",
                principalTable: "Retweets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mentions_Retweets_RetweetId",
                table: "Mentions",
                column: "RetweetId",
                principalTable: "Retweets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Retweets_RetweetId",
                table: "Replies",
                column: "RetweetId",
                principalTable: "Retweets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Retweets_Retweets_RetweetId",
                table: "Retweets",
                column: "RetweetId",
                principalTable: "Retweets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TweetHashtags_Retweets_RetweetId",
                table: "TweetHashtags",
                column: "RetweetId",
                principalTable: "Retweets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Retweets_RetweetId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Mentions_Retweets_RetweetId",
                table: "Mentions");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Retweets_RetweetId",
                table: "Replies");

            migrationBuilder.DropForeignKey(
                name: "FK_Retweets_Retweets_RetweetId",
                table: "Retweets");

            migrationBuilder.DropForeignKey(
                name: "FK_TweetHashtags_Retweets_RetweetId",
                table: "TweetHashtags");

            migrationBuilder.DropIndex(
                name: "IX_TweetHashtags_RetweetId",
                table: "TweetHashtags");

            migrationBuilder.DropIndex(
                name: "IX_Retweets_RetweetId",
                table: "Retweets");

            migrationBuilder.DropIndex(
                name: "IX_Replies_RetweetId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Mentions_RetweetId",
                table: "Mentions");

            migrationBuilder.DropIndex(
                name: "IX_Likes_RetweetId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "RetweetId",
                table: "TweetHashtags");

            migrationBuilder.DropColumn(
                name: "RetweetId",
                table: "Retweets");

            migrationBuilder.DropColumn(
                name: "RetweetId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "RetweetId",
                table: "Mentions");

            migrationBuilder.DropColumn(
                name: "RetweetId",
                table: "Likes");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Retweets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
