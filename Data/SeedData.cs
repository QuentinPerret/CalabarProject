using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using Data;

namespace Models;

public class SeedData
{
    public static void Init()
    {
        using (var context = new CalabarContext())
        {

            if (context.Database.EnsureDeleted())
            {
                Console.WriteLine("\n*** The Database has been deleted ***\n");
                Console.WriteLine("*** Start Database Creation ***\n");
                context.Database.EnsureCreated();
                Console.WriteLine("Database Created");
            }

            // Ajout d'utilisateur
            UtilisateurCommun uCommun = new UtilisateurCommun
            {
                Nom = "Commun",
                Prenom = "Utilisateur",
                Email = "user@ensc.fr",
                Password = "password"
            };
            Administrateur jMeunier = new Administrateur
            {
                Nom = "Meunier",
                Prenom = "Juliette",
                Email = "jmeunier001@ensc.fr",
                Password = "password",
            };
            Administrateur qPerret = new Administrateur
            {
                Nom = "Perret",
                Prenom = "Quentin",
                Email = "qperret@ensc.fr",
                Password = "password",
                Associations = new List<Association> { }
            };
            Collaborateur aPorte = new Collaborateur
            {
                Nom = "Porte",
                Prenom = "Antoine",
                Email = "aporte005@ensc.fr",
                Password = "password",
                Associations = new List<Association> { }
            };
            Collaborateur mTraversier = new Collaborateur
            {
                Nom = "Traversier",
                Prenom = "Mathys",
                Email = "mtraversier@ensc.fr",
                Password = "password",
                Associations = new List<Association> { }
            };


            // Ajout Association
            Bureau BDE = new Bureau
            {
                Nom = "BDE",
                Email = "bde@ensc.fr",
                MembreAssociationClubs = new List<MembreAssociationClub> { mTraversier, aPorte, jMeunier, qPerret },
                Clubs = new List<Club> { }
            };
            Bureau BDF = new Bureau
            {
                Nom = "BDF",
                Email = "bdf@ensc.fr"
            };
            Club Oenologie = new Club
            {
                Nom = "Club d'œnologie",
                Email = "cluboenologie@ensc.fr",
                MembreAssociationClubs = new List<MembreAssociationClub> { jMeunier, qPerret, aPorte },
                Bureau = BDE
            };

            //Ajout Fournisseur
            Supermarche Metro = new Supermarche
            {
                Nom = "Métro",
                Siret = "123658966",
                Adresse = "99 , Une rue que  je ne  connais pas , 33050 Bordeaux Lac",
                Email = "contact@metro.fr",
                Telephone = "0652659984"
            };
            Brasserie VandB = new Brasserie
            {
                Nom = "V&B",
                Siret = "123658966",
                Adresse = "666 , La rue des fils , 33400 Merignac",
                Email = "contact@v&b.fr",
                Telephone = "0952654585"
            };
            Chateau bourgail = new Chateau
            {
                Nom = "Chateau du Bourgail",
                Siret = "123456789",
                Adresse = "Dans les vignes quelque part",
                Email = "contact@lagrange.fr",
                Telephone = "0612345678"
            };

            //Ajout Consommables
            Soft S1 = new Soft
            {
                Nom = "Limonade",
                Quantite = 25,
                PrixAchat = 2,
                PrixVente = 5,
                Association = BDE,
                TailleBouteilleLitre = 0.75
            };

            Biere B1 = new Biere
            {
                Nom = "Cuvée des trools",
                TypeDeBiere = typeDeBiere.Blonde,
                Degre = 7.8,
                PrixAchat = 1.23,
                PrixVente = 2,
                Quantite = 3,
                Brasserie = VandB,
                Association = BDE
            };

            Vin grave1 = new Vin
            {
                Nom = "Chateau du Bourgail",
                PrixAchat = 6,
                PrixVente = 9,
                Quantite = 6,
                Chateau = bourgail,
                Millesime = 2020,
                TailleBouteilleLitre = 0.75,
                Association = Oenologie
            };

            Nourriture N1 = new Nourriture
            {
                Nom = "Chips",
                Quantite = 25,
                PrixAchat = 2,
                PrixVente = 5,
                Association = BDE
            };

            //Ajout Facture
            Facture F1 = new Facture
            {
                Numero = 12553,
                PrixHT = 300,
                PrixTTC = 346,
                Date = new DateTime(2023, 1, 1),
                Fournisseur = Metro,
                Association = BDE
            };

            // Vérifie si la DB existe déjà
            if (context.Utilisateurs.Any())
            {
                // DB déjà existante
            }
            else
            {
                context.Utilisateurs.AddRange(
                    jMeunier,
                    qPerret,
                    aPorte,
                    mTraversier,
                    uCommun
                );
                Console.Write("Elements Added in Utilisateur\n");
            }

            if (context.Associations.Any())
            {
                // DB déjà existante
                Console.Write("There is already element in Association\n");
            }
            else
            {
                context.Associations.AddRange(
                    BDE,
                    BDF,
                    Oenologie
                );
                Console.Write("Elements Added in Association\n");
            }

            if (context.Fournisseurs.Any())
            {
                // DB déjà existante
                Console.Write("There is already element in Fournisseur\n");
            }
            else
            {
                context.Fournisseurs.AddRange(
                    Metro,
                    VandB,
                    bourgail
                );
                Console.Write("Elements Added in Fournisseur\n");
            }



            if (!context.Consommables.Any())
            {
                context.Consommables.AddRange(
                    S1, B1, grave1, N1
                );
                Console.Write("Elements Added in Consommable\n");
            }
            else { Console.Write("There is already element in Consommable\n"); }

            if (!context.Factures.Any())
            {
                context.Factures.AddRange(
                    F1
                );
                Console.Write("Elements Added in Facture\n");
            }
            else { Console.Write("There is already element in Facture\n"); }

            context.SaveChanges();
            Console.WriteLine("Database Filled");

            Console.WriteLine("\n*** End Database Creation ***");

        }
    }
}