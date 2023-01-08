using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projetquentinjuliette.Migrations
{
    /// <inheritdoc />
    public partial class jointureUserAssoP2P : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Associations_MembreAssociationClubs_PresidentId",
                table: "Associations");

            migrationBuilder.DropTable(
                name: "UserAsso");

            migrationBuilder.DropIndex(
                name: "IX_Associations_PresidentId",
                table: "Associations");

            migrationBuilder.DropColumn(
                name: "PresidentId",
                table: "Associations");

            migrationBuilder.CreateTable(
                name: "UserAssos",
                columns: table => new
                {
                    AssociationsId = table.Column<int>(type: "INTEGER", nullable: false),
                    MembreAssociationClubsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAssos", x => new { x.AssociationsId, x.MembreAssociationClubsId });
                    table.ForeignKey(
                        name: "FK_UserAssos_Associations_AssociationsId",
                        column: x => x.AssociationsId,
                        principalTable: "Associations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAssos_MembreAssociationClubs_MembreAssociationClubsId",
                        column: x => x.MembreAssociationClubsId,
                        principalTable: "MembreAssociationClubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAssos_MembreAssociationClubsId",
                table: "UserAssos",
                column: "MembreAssociationClubsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAssos");

            migrationBuilder.AddColumn<int>(
                name: "PresidentId",
                table: "Associations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_Associations_PresidentId",
                table: "Associations",
                column: "PresidentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAsso_AssoId",
                table: "UserAsso",
                column: "AssoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Associations_MembreAssociationClubs_PresidentId",
                table: "Associations",
                column: "PresidentId",
                principalTable: "MembreAssociationClubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
