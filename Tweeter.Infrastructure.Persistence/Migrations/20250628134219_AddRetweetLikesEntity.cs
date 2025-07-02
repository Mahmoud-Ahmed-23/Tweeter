using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tweeter.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRetweetLikesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Retweets_RetweetId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Retweets_Retweets_RetweetId",
                table: "Retweets");

            migrationBuilder.DropIndex(
                name: "IX_Retweets_RetweetId",
                table: "Retweets");

            migrationBuilder.DropIndex(
                name: "IX_Likes_RetweetId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "RetweetId",
                table: "Retweets");

            migrationBuilder.DropColumn(
                name: "RetweetId",
                table: "Likes");

            migrationBuilder.CreateTable(
                name: "RetweetLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RetweetId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetweetLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetweetLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RetweetLikes_Retweets_RetweetId",
                        column: x => x.RetweetId,
                        principalTable: "Retweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RetweetLikes_RetweetId",
                table: "RetweetLikes",
                column: "RetweetId");

            migrationBuilder.CreateIndex(
                name: "IX_RetweetLikes_UserId_RetweetId",
                table: "RetweetLikes",
                columns: new[] { "UserId", "RetweetId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RetweetLikes");

            migrationBuilder.AddColumn<int>(
                name: "RetweetId",
                table: "Retweets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RetweetId",
                table: "Likes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Retweets_RetweetId",
                table: "Retweets",
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
                name: "FK_Retweets_Retweets_RetweetId",
                table: "Retweets",
                column: "RetweetId",
                principalTable: "Retweets",
                principalColumn: "Id");
        }
    }
}
