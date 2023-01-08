using Microsoft.EntityFrameworkCore;

namespace Models;

public class VinViewModel
{
    public List<Association> A { get; set; } = null!;
    public List<Chateau> C { get; set; } = null!;
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public int Quantite { get; set; }
    public double PrixAchat { get; set; }
    public double PrixVente { get; set; }
    public int AssociationId { get; set; }
    public double TailleBouteilleLitre { get; set; }
    public int Millesime { get; set; }
    public int ChateauId { get; set; }
    public typeDeVin TypeDeVin { get; set; }

    public VinViewModel() { }
    public VinViewModel(DbContext context, Vin V)
    {
        Id = V.Id;
        Nom = V.Nom;
        Quantite = V.Quantite;
        PrixAchat = V.PrixAchat;
        PrixVente = V.PrixVente;
        AssociationId = V.AssociationId;
        Millesime = V.Millesime;
        ChateauId = V.ChateauId;
        TailleBouteilleLitre = V.TailleBouteilleLitre;
        TypeDeVin = V.TypeDeVin;

        A = context.Set<Association>().ToList();
        C = context.Set<Chateau>().ToList();
    }
    public VinViewModel(DbContext context)
    {
        A = context.Set<Association>().ToList();
        C = context.Set<Chateau>().ToList();
    }

}