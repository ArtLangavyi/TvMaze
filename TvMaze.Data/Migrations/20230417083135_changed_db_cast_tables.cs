using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TvMaze.Data.Migrations
{
    /// <inheritdoc />
    public partial class changed_db_cast_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowCastRelation_Cast_Cascade",
                table: "ShowCastRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowCastRelation_Show_Cascade",
                table: "ShowCastRelation");

            migrationBuilder.RenameColumn(
                name: "CastId",
                table: "ShowCastRelation",
                newName: "CastPersoneId");

            migrationBuilder.RenameIndex(
                name: "IX_ShowCastRelation_ShowId_Cast_id",
                table: "ShowCastRelation",
                newName: "IX_ShowCastRelation_ShowId_CastPersone_id");

            migrationBuilder.RenameIndex(
                name: "IX_ShowCastRelation_CastId",
                table: "ShowCastRelation",
                newName: "IX_ShowCastRelation_CastPersoneId");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Cast",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ShowCastPersoneRelation_CastPersone_Cascade",
                table: "ShowCastRelation",
                column: "ShowId",
                principalTable: "Cast",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShowCastPersoneRelation_Show_Cascade",
                table: "ShowCastRelation",
                column: "CastPersoneId",
                principalTable: "Show",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowCastPersoneRelation_CastPersone_Cascade",
                table: "ShowCastRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowCastPersoneRelation_Show_Cascade",
                table: "ShowCastRelation");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Cast");

            migrationBuilder.RenameColumn(
                name: "CastPersoneId",
                table: "ShowCastRelation",
                newName: "CastId");

            migrationBuilder.RenameIndex(
                name: "IX_ShowCastRelation_ShowId_CastPersone_id",
                table: "ShowCastRelation",
                newName: "IX_ShowCastRelation_ShowId_Cast_id");

            migrationBuilder.RenameIndex(
                name: "IX_ShowCastRelation_CastPersoneId",
                table: "ShowCastRelation",
                newName: "IX_ShowCastRelation_CastId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowCastRelation_Cast_Cascade",
                table: "ShowCastRelation",
                column: "ShowId",
                principalTable: "Cast",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShowCastRelation_Show_Cascade",
                table: "ShowCastRelation",
                column: "CastId",
                principalTable: "Show",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
