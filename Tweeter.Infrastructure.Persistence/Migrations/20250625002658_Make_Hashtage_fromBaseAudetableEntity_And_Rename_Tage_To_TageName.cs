using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tweeter.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Make_Hashtage_fromBaseAudetableEntity_And_Rename_Tage_To_TageName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TweetHashtags",
                table: "TweetHashtags");

            migrationBuilder.RenameColumn(
                name: "Tag",
                table: "Hashtags",
                newName: "TagName");

            migrationBuilder.RenameIndex(
                name: "IX_Hashtags_Tag",
                table: "Hashtags",
                newName: "IX_Hashtags_TagName");

            migrationBuilder.AddColumn<int>(
                name: "TweetId",
                table: "TweetHashtags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Hashtags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Hashtags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinDate",
                table: "Hashtags",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Hashtags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "Hashtags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TweetHashtags",
                table: "TweetHashtags",
                columns: new[] { "Id", "TweetId", "HashtagId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TweetHashtags",
                table: "TweetHashtags");

            migrationBuilder.DropColumn(
                name: "TweetId",
                table: "TweetHashtags");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Hashtags");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Hashtags");

            migrationBuilder.DropColumn(
                name: "JoinDate",
                table: "Hashtags");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Hashtags");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "Hashtags");

            migrationBuilder.RenameColumn(
                name: "TagName",
                table: "Hashtags",
                newName: "Tag");

            migrationBuilder.RenameIndex(
                name: "IX_Hashtags_TagName",
                table: "Hashtags",
                newName: "IX_Hashtags_Tag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TweetHashtags",
                table: "TweetHashtags",
                columns: new[] { "Id", "HashtagId" });
        }
    }
}
