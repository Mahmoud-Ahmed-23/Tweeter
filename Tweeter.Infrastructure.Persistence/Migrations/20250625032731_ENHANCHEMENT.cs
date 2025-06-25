using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tweeter.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ENHANCHEMENT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TweetHashtags_Tweets_Id",
                table: "TweetHashtags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TweetHashtags",
                table: "TweetHashtags");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TweetHashtags");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TweetHashtags",
                table: "TweetHashtags",
                columns: new[] { "TweetId", "HashtagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TweetHashtags_Tweets_TweetId",
                table: "TweetHashtags",
                column: "TweetId",
                principalTable: "Tweets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TweetHashtags_Tweets_TweetId",
                table: "TweetHashtags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TweetHashtags",
                table: "TweetHashtags");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TweetHashtags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TweetHashtags",
                table: "TweetHashtags",
                columns: new[] { "Id", "TweetId", "HashtagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TweetHashtags_Tweets_Id",
                table: "TweetHashtags",
                column: "Id",
                principalTable: "Tweets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
