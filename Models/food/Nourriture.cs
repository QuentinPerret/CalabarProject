using System.ComponentModel.DataAnnotations;
namespace Models;

public class Nourriture : Consommable
{
    public Nourriture() { }
    public Nourriture(NourritureViewModel model)
    {
        this.Id = model.Id;
        this.Nom = model.Nom;
        this.Quantite = model.Quantite;
        this.PrixAchat = model.PrixAchat;
        this.PrixVente = model.PrixVente;
        this.AssociationId = model.AssociationId;
    }

    public Nourriture(NourritureDTO nourritureDTO)
    {
        Id = nourritureDTO.Id;
        Nom = nourritureDTO.Nom;
        Quantite = nourritureDTO.Quantite;
        PrixAchat = nourritureDTO.PrixAchat;
        PrixVente = nourritureDTO.PrixVente;
        AssociationId = nourritureDTO.AssociationId;
    }
}
