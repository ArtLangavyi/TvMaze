using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TvMaze.Data.Migrations
{
    /// <inheritdoc />
    public partial class added_show_and_cast_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Show",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(512)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(512)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Show", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cast",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShowId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(512)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(512)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cast", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cast_Show_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Show",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShowCastRelation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowId = table.Column<int>(type: "int", nullable: false),
                    CastId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowCastRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShowCastRelation_Cast_Cascade",
                        column: x => x.ShowId,
                        principalTable: "Cast",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShowCastRelation_Show_Cascade",
                        column: x => x.CastId,
                        principalTable: "Show",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cast_ShowId",
                table: "Cast",
                column: "ShowId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowCastRelation_CastId",
                table: "ShowCastRelation",
                column: "CastId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowCastRelation_ShowId",
                table: "ShowCastRelation",
                column: "ShowId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowCastRelation_ShowId_Cast_id",
                table: "ShowCastRelation",
                columns: new[] { "Id", "ShowId", "CastId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShowCastRelation");

            migrationBuilder.DropTable(
                name: "Cast");

            migrationBuilder.DropTable(
                name: "Show");
        }
    }
}
