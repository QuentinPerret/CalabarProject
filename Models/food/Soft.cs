using System.ComponentModel.DataAnnotations;
namespace Models;

public class Soft : Liquide
{
    public Soft() { }
    public Soft(SoftDTO softDTO)
    {
        Id = softDTO.Id;
        Nom = softDTO.Nom;
        Quantite = softDTO.Quantite;
        PrixAchat = softDTO.PrixAchat;
        PrixVente = softDTO.PrixVente;
        AssociationId = softDTO.AssociationId;
        TailleBouteilleLitre = softDTO.TailleBouteilleLitre;
    }

    public Soft(SoftViewModel soft)
    {
        Id = soft.Id;
        Nom = soft.Nom;
        Quantite = soft.Quantite;
        PrixAchat = soft.PrixAchat;
        PrixVente = soft.PrixVente;
        AssociationId = soft.AssociationId;
        TailleBouteilleLitre = soft.TailleBouteilleLitre;
    }
}