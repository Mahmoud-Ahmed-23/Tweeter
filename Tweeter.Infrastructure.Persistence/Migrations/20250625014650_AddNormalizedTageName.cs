using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tweeter.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNormalizedTageName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedTagName",
                table: "Hashtags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizedTagName",
                table: "Hashtags");
        }
    }
}
