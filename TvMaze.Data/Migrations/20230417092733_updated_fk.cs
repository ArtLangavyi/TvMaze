﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TvMaze.Data.Migrations
{
    /// <inheritdoc />
    public partial class updated_fk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowCastPersoneRelation_CastPersone_Cascade_Delete",
                table: "ShowCastPersoneRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowCastPersoneRelation_Show_Cascade_Delete",
                table: "ShowCastPersoneRelation");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowCastPersoneRelation_CastPersone_Cascade_Delete",
                table: "ShowCastPersoneRelation",
                column: "ShowId",
                principalTable: "Show",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShowCastPersoneRelation_Show_Cascade_Delete",
                table: "ShowCastPersoneRelation",
                column: "CastPersoneId",
                principalTable: "CastPersone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowCastPersoneRelation_CastPersone_Cascade_Delete",
                table: "ShowCastPersoneRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowCastPersoneRelation_Show_Cascade_Delete",
                table: "ShowCastPersoneRelation");

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
    }
}
