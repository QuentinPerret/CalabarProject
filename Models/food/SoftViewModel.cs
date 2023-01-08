using Microsoft.EntityFrameworkCore;

namespace Models;

public class SoftViewModel
{
    public List<Association> A { get; set; } = null!;
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public int Quantite { get; set; }
    public double PrixAchat { get; set; }
    public double PrixVente { get; set; }
    public int AssociationId { get; set; }
    public double TailleBouteilleLitre { get; set; }

    public SoftViewModel() { }
    public SoftViewModel(DbContext context, Soft S)
    {
        Id = S.Id;
        Nom = S.Nom;
        Quantite = S.Quantite;
        PrixAchat = S.PrixAchat;
        PrixVente = S.PrixVente;
        AssociationId = S.AssociationId;
        TailleBouteilleLitre = S.TailleBouteilleLitre;

        A = context.Set<Association>().ToList();
    }
    public SoftViewModel(DbContext context)
    {
        this.A = context.Set<Association>().ToList();
    }

}