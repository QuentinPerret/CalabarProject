using System.ComponentModel.DataAnnotations;
namespace Models;

public enum typeDeBiere
{
    Blonde,
    Brune,
    Rouge,
    Triple,
    Rousse,
    Cidre
}

public class Biere : Liquide
{
    public typeDeBiere TypeDeBiere { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "Veuillez rentrer un nombre")]
    public double Degre { get; set; }
    public Brasserie Brasserie { get; set; } = null!;
    public int BrasserieId { get; set; }


    public Biere() { }
    public Biere(BiereDTO biereDTO)
    {
        Id = biereDTO.Id;
        Nom = biereDTO.Nom;
        TypeDeBiere = biereDTO.TypeDeBiere;
        BrasserieId = biereDTO.BrasserieId;
        Quantite = biereDTO.Quantite;
        PrixAchat = biereDTO.PrixAchat;
        PrixVente = biereDTO.PrixVente;
        AssociationId = biereDTO.AssociationId;
        TailleBouteilleLitre = biereDTO.TailleBouteilleLitre;
    }
    public Biere(BiereViewModel biere)
    {
        Id = biere.Id;
        Nom = biere.Nom;
        TypeDeBiere = biere.TypeDeBiere;
        BrasserieId = biere.BrasserieId;
        Quantite = biere.Quantite;
        PrixAchat = biere.PrixAchat;
        PrixVente = biere.PrixVente;
        AssociationId = biere.AssociationId;
        TailleBouteilleLitre = biere.TailleBouteilleLitre;
    }
}