using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projetquentinjuliette.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionAppartenanceAsso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fournisseurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Siret = table.Column<string>(type: "TEXT", nullable: false),
                    Adresse = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Telephone = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fournisseurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Prenom = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brasseries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brasseries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brasseries_Fournisseurs_Id",
                        column: x => x.Id,
                        principalTable: "Fournisseurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chateaux",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chateaux", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chateaux_Fournisseurs_Id",
                        column: x => x.Id,
                        principalTable: "Fournisseurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supermarches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supermarches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supermarches_Fournisseurs_Id",
                        column: x => x.Id,
                        principalTable: "Fournisseurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MembreAssociationClubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembreAssociationClubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembreAssociationClubs_Utilisateurs_Id",
                        column: x => x.Id,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UtilisateurCommuns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EstUnEtudiant = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilisateurCommuns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UtilisateurCommuns_Utilisateurs_Id",
                        column: x => x.Id,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Administrateurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrateurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administrateurs_MembreAssociationClubs_Id",
                        column: x => x.Id,
                        principalTable: "MembreAssociationClubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Associations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PresidentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Associations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Associations_MembreAssociationClubs_PresidentId",
                        column: x => x.PresidentId,
                        principalTable: "MembreAssociationClubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Collaborateurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborateurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collaborateurs_MembreAssociationClubs_Id",
                        column: x => x.Id,
                        principalTable: "MembreAssociationClubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bureaux",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bureaux", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bureaux_Associations_Id",
                        column: x => x.Id,
                        principalTable: "Associations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consommables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Quantite = table.Column<int>(type: "INTEGER", nullable: false),
                    PrixAchat = table.Column<double>(type: "REAL", nullable: false),
                    PrixVente = table.Column<double>(type: "REAL", nullable: false),
                    AssociationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consommables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consommables_Associations_AssociationId",
                        column: x => x.AssociationId,
                        principalTable: "Associations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DependDeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IdBureau = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clubs_Associations_Id",
                        column: x => x.Id,
                        principalTable: "Associations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clubs_Bureaux_DependDeId",
                        column: x => x.DependDeId,
                        principalTable: "Bureaux",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bieres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeDeBiere = table.Column<int>(type: "INTEGER", nullable: false),
                    Degre = table.Column<double>(type: "REAL", nullable: false),
                    BrasserieId = table.Column<int>(type: "INTEGER", nullable: false),
                    TailleBouteilleLitre = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bieres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bieres_Brasseries_BrasserieId",
                        column: x => x.BrasserieId,
                        principalTable: "Brasseries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bieres_Consommables_Id",
                        column: x => x.Id,
                        principalTable: "Consommables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Factures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    PrixHT = table.Column<double>(type: "REAL", nullable: false),
                    PrixTTC = table.Column<double>(type: "REAL", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FournisseurId = table.Column<int>(type: "INTEGER", nullable: false),
                    ConsommableId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Factures_Consommables_ConsommableId",
                        column: x => x.ConsommableId,
                        principalTable: "Consommables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Factures_Fournisseurs_FournisseurId",
                        column: x => x.FournisseurId,
                        principalTable: "Fournisseurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nourritures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nourritures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nourritures_Consommables_Id",
                        column: x => x.Id,
                        principalTable: "Consommables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Softs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TailleBouteilleLitre = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Softs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Softs_Consommables_Id",
                        column: x => x.Id,
                        principalTable: "Consommables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChateauId = table.Column<int>(type: "INTEGER", nullable: false),
                    Millesime = table.Column<int>(type: "INTEGER", nullable: false),
                    TailleBouteilleLitre = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vins_Chateaux_ChateauId",
                        column: x => x.ChateauId,
                        principalTable: "Chateaux",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vins_Consommables_Id",
                        column: x => x.Id,
                        principalTable: "Consommables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Associations_PresidentId",
                table: "Associations",
                column: "PresidentId");

            migrationBuilder.CreateIndex(
                name: "IX_Bieres_BrasserieId",
                table: "Bieres",
                column: "BrasserieId");

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_DependDeId",
                table: "Clubs",
                column: "DependDeId");

            migrationBuilder.CreateIndex(
                name: "IX_Consommables_AssociationId",
                table: "Consommables",
                column: "AssociationId");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_ConsommableId",
                table: "Factures",
                column: "ConsommableId");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_FournisseurId",
                table: "Factures",
                column: "FournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_Vins_ChateauId",
                table: "Vins",
                column: "ChateauId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrateurs");

            migrationBuilder.DropTable(
                name: "Bieres");

            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DropTable(
                name: "Collaborateurs");

            migrationBuilder.DropTable(
                name: "Factures");

            migrationBuilder.DropTable(
                name: "Nourritures");

            migrationBuilder.DropTable(
                name: "Softs");

            migrationBuilder.DropTable(
                name: "Supermarches");

            migrationBuilder.DropTable(
                name: "UtilisateurCommuns");

            migrationBuilder.DropTable(
                name: "Vins");

            migrationBuilder.DropTable(
                name: "Brasseries");

            migrationBuilder.DropTable(
                name: "Bureaux");

            migrationBuilder.DropTable(
                name: "Chateaux");

            migrationBuilder.DropTable(
                name: "Consommables");

            migrationBuilder.DropTable(
                name: "Fournisseurs");

            migrationBuilder.DropTable(
                name: "Associations");

            migrationBuilder.DropTable(
                name: "MembreAssociationClubs");

            migrationBuilder.DropTable(
                name: "Utilisateurs");
        }
    }
}
