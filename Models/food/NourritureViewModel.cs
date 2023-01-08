using Microsoft.EntityFrameworkCore;

namespace Models;

public class NourritureViewModel
{
    public List<Association> A { get; set; } = null!;
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public int Quantite { get; set; }
    public double PrixAchat { get; set; }
    public double PrixVente { get; set; }
    public int AssociationId { get; set; }

    public NourritureViewModel() { }
    public NourritureViewModel(DbContext context, Nourriture N)
    {
        Id = N.Id;
        Nom = N.Nom;
        Quantite = N.Quantite;
        PrixAchat = N.PrixAchat;
        PrixVente = N.PrixVente;
        AssociationId = N.AssociationId;

        A = context.Set<Association>().ToList();
    }
    public NourritureViewModel(DbContext context)
    {
        this.A = context.Set<Association>().ToList();
    }

}