using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FiveSys.Retaguarda.Infrastructure.Data.Admin.Migrations
{
    public partial class RetaguardaAdminDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Industrias",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false),
                    RamoId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industrias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Industrias_Ramos_RamoId",
                        column: x => x.RamoId,
                        principalTable: "Ramos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Industrias_Id",
                table: "Industrias",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Industrias_RamoId",
                table: "Industrias",
                column: "RamoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Industrias");
        }
    }
}
