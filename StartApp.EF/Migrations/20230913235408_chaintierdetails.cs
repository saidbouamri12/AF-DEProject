using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StartApp.EF.Migrations
{
    public partial class chaintierdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChantierDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Userid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Chantierid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChantierDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChantierDetails_AspNetUsers_Userid",
                        column: x => x.Userid,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChantierDetails_Chantiers_Chantierid",
                        column: x => x.Chantierid,
                        principalTable: "Chantiers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChantierDetails_Chantierid",
                table: "ChantierDetails",
                column: "Chantierid");

            migrationBuilder.CreateIndex(
                name: "IX_ChantierDetails_Userid",
                table: "ChantierDetails",
                column: "Userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChantierDetails");
        }
    }
}
