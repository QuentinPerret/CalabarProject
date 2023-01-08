using System.ComponentModel.DataAnnotations;
namespace Models;
public enum typeDeVin
{
    Rouge,
    Blanc,
    Rosé,
    Autre
}
public class Vin : Liquide
{
    public typeDeVin TypeDeVin { get; set; }
    public Chateau Chateau { get; set; } = null!;
    public int ChateauId { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Veuillez rentrer un nombre entier")]
    public int Millesime { get; set; }


    public Vin() { }
    public Vin(VinDTO vinDTO)
    {
        Id = vinDTO.Id;
        Nom = vinDTO.Nom;
        ChateauId = vinDTO.ChateauId;
        Quantite = vinDTO.Quantite;
        PrixAchat = vinDTO.PrixAchat;
        PrixVente = vinDTO.PrixVente;
        AssociationId = vinDTO.AssociationId;
        TailleBouteilleLitre = vinDTO.TailleBouteilleLitre;
        Millesime = vinDTO.Millesime;
        TypeDeVin = vinDTO.TypeDeVin;
    }

    public Vin(VinViewModel vin)
    {
        Id = vin.Id;
        Nom = vin.Nom;
        ChateauId = vin.ChateauId;
        Quantite = vin.Quantite;
        PrixAchat = vin.PrixAchat;
        PrixVente = vin.PrixVente;
        AssociationId = vin.AssociationId;
        TailleBouteilleLitre = vin.TailleBouteilleLitre;
        Millesime = vin.Millesime;
        TypeDeVin = vin.TypeDeVin;
    }

}