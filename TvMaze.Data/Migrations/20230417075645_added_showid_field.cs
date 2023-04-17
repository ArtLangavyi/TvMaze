using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TvMaze.Data.Migrations
{
    /// <inheritdoc />
    public partial class added_showid_field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShowId",
                table: "Show",
                type: "int",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowId",
                table: "Show");
        }
    }
}
