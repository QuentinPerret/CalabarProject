using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projetquentinjuliette.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionAppartenanceAssoNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAsso",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    AssoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAsso", x => new { x.UserId, x.AssoId });
                    table.ForeignKey(
                        name: "FK_UserAsso_Associations_AssoId",
                        column: x => x.AssoId,
                        principalTable: "Associations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAsso_MembreAssociationClubs_UserId",
                        column: x => x.UserId,
                        principalTable: "MembreAssociationClubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAsso_AssoId",
                table: "UserAsso",
                column: "AssoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAsso");
        }
    }
}
