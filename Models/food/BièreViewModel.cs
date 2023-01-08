using Microsoft.EntityFrameworkCore;

namespace Models;

public class BiereViewModel
{
    public List<Association> A { get; set; } = null!;
    public List<Brasserie> Brasseries { get; set; } = null!;
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public int Quantite { get; set; }
    public double PrixAchat { get; set; }
    public double PrixVente { get; set; }
    public int AssociationId { get; set; }
    public double TailleBouteilleLitre { get; set; }
    public typeDeBiere TypeDeBiere { get; set; }
    public double Degre { get; set; }
    public int BrasserieId { get; set; }

    public BiereViewModel() { }
    public BiereViewModel(DbContext context, Biere B)
    {
        Id = B.Id;
        Nom = B.Nom;
        Quantite = B.Quantite;
        PrixAchat = B.PrixAchat;
        PrixVente = B.PrixVente;
        AssociationId = B.AssociationId;
        TypeDeBiere = B.TypeDeBiere;
        Degre = B.Degre;
        BrasserieId = B.BrasserieId;
        TailleBouteilleLitre = B.TailleBouteilleLitre;

        A = context.Set<Association>().ToList();
        Brasseries = context.Set<Brasserie>().ToList();
    }
    public BiereViewModel(DbContext context)
    {
        this.A = context.Set<Association>().ToList();
        Brasseries = context.Set<Brasserie>().ToList();
    }

}