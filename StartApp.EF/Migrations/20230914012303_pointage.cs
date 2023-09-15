using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StartApp.EF.Migrations
{
    public partial class pointage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pointage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    periodId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pointage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pointage_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pointage_Periods_periodId",
                        column: x => x.periodId,
                        principalTable: "Periods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pointage_EmpId",
                table: "pointage",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_pointage_periodId",
                table: "pointage",
                column: "periodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pointage");
        }
    }
}
