using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StartApp.EF.Migrations
{
    public partial class emp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    label = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelationshipStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialty",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    label = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialty", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelationshipStatusid = table.Column<int>(type: "int", nullable: false),
                    Nomberenfant = table.Column<int>(type: "int", nullable: false),
                    Num_Cnss = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dateemboche = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dateend = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salaire = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Specialtyid = table.Column<int>(type: "int", nullable: false),
                    ContractPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractTypeid = table.Column<int>(type: "int", nullable: false),
                    CartecnssPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarteCinPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_ContractType_ContractTypeid",
                        column: x => x.ContractTypeid,
                        principalTable: "ContractType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_RelationshipStatus_RelationshipStatusid",
                        column: x => x.RelationshipStatusid,
                        principalTable: "RelationshipStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Specialty_Specialtyid",
                        column: x => x.Specialtyid,
                        principalTable: "Specialty",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ContractTypeid",
                table: "Employees",
                column: "ContractTypeid");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RelationshipStatusid",
                table: "Employees",
                column: "RelationshipStatusid");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Specialtyid",
                table: "Employees",
                column: "Specialtyid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ContractType");

            migrationBuilder.DropTable(
                name: "RelationshipStatus");

            migrationBuilder.DropTable(
                name: "Specialty");
        }
    }
}
