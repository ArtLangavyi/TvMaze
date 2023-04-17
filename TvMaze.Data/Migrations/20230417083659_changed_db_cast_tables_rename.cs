using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TvMaze.Data.Migrations
{
    /// <inheritdoc />
    public partial class changed_db_cast_tables_rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cast_Show_ShowId",
                table: "Cast");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowCastPersoneRelation_CastPersone_Cascade",
                table: "ShowCastRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowCastPersoneRelation_Show_Cascade",
                table: "ShowCastRelation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShowCastRelation",
                table: "ShowCastRelation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cast",
                table: "Cast");

            migrationBuilder.RenameTable(
                name: "ShowCastRelation",
                newName: "ShowCastPersoneRelation");

            migrationBuilder.RenameTable(
                name: "Cast",
                newName: "CastPersone");

            migrationBuilder.RenameIndex(
                name: "IX_ShowCastRelation_ShowId_CastPersone_id",
                table: "ShowCastPersoneRelation",
                newName: "IX_ShowCastPersoneRelation_ShowId_CastPersone_id");

            migrationBuilder.RenameIndex(
                name: "IX_ShowCastRelation_ShowId",
                table: "ShowCastPersoneRelation",
                newName: "IX_ShowCastPersoneRelation_ShowId");

            migrationBuilder.RenameIndex(
                name: "IX_ShowCastRelation_CastPersoneId",
                table: "ShowCastPersoneRelation",
                newName: "IX_ShowCastPersoneRelation_CastPersoneId");

            migrationBuilder.RenameIndex(
                name: "IX_Cast_ShowId",
                table: "CastPersone",
                newName: "IX_CastPersone_ShowId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShowCastPersoneRelation",
                table: "ShowCastPersoneRelation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CastPersone",
                table: "CastPersone",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CastPersone_Show_ShowId",
                table: "CastPersone",
                column: "ShowId",
                principalTable: "Show",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowCastPersoneRelation_CastPersone_Cascade_Delete",
                table: "ShowCastPersoneRelation",
                column: "ShowId",
                principalTable: "CastPersone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShowCastPersoneRelation_Show_Cascade_Delete",
                table: "ShowCastPersoneRelation",
                column: "CastPersoneId",
                principalTable: "Show",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CastPersone_Show_ShowId",
                table: "CastPersone");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowCastPersoneRelation_CastPersone_Cascade_Delete",
                table: "ShowCastPersoneRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowCastPersoneRelation_Show_Cascade_Delete",
                table: "ShowCastPersoneRelation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShowCastPersoneRelation",
                table: "ShowCastPersoneRelation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CastPersone",
                table: "CastPersone");

            migrationBuilder.RenameTable(
                name: "ShowCastPersoneRelation",
                newName: "ShowCastRelation");

            migrationBuilder.RenameTable(
                name: "CastPersone",
                newName: "Cast");

            migrationBuilder.RenameIndex(
                name: "IX_ShowCastPersoneRelation_ShowId_CastPersone_id",
                table: "ShowCastRelation",
                newName: "IX_ShowCastRelation_ShowId_CastPersone_id");

            migrationBuilder.RenameIndex(
                name: "IX_ShowCastPersoneRelation_ShowId",
                table: "ShowCastRelation",
                newName: "IX_ShowCastRelation_ShowId");

            migrationBuilder.RenameIndex(
                name: "IX_ShowCastPersoneRelation_CastPersoneId",
                table: "ShowCastRelation",
                newName: "IX_ShowCastRelation_CastPersoneId");

            migrationBuilder.RenameIndex(
                name: "IX_CastPersone_ShowId",
                table: "Cast",
                newName: "IX_Cast_ShowId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShowCastRelation",
                table: "ShowCastRelation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cast",
                table: "Cast",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cast_Show_ShowId",
                table: "Cast",
                column: "ShowId",
                principalTable: "Show",
                principalColumn: "Id");

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
    }
}
