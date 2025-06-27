using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tweeter.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSomeEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TweetHashtags",
                table: "TweetHashtags");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TweetHashtags",
                table: "TweetHashtags",
                columns: new[] { "TweetId", "HashtagId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TweetHashtags",
                table: "TweetHashtags");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TweetHashtags",
                table: "TweetHashtags",
                columns: new[] { "Container", "HashtagId" });
        }
    }
}
