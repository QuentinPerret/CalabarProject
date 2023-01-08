using Microsoft.EntityFrameworkCore;

namespace Models;

public class FactureViewModel
{
    public List<Association> A { get; set; } = null!;
    public List<Fournisseur> F { get; set; } = null!;
    public int Id { get; set; }
    public int Numero { get; set; }
    public double PrixHT { get; set; }
    public double PrixTTC { get; set; }
    public DateTime Date { get; set; }
    public int FournisseurId { get; set; }
    public int AssociationId { get; set; }

    public FactureViewModel() { }
    public FactureViewModel(DbContext context, Facture facture)
    {
        Id = facture.Id;
        Numero = facture.Numero;
        PrixHT = facture.PrixHT;
        PrixTTC = facture.PrixTTC;
        Date = facture.Date;
        FournisseurId = facture.FournisseurId;
        AssociationId = facture.AssociationId;

        A = context.Set<Association>().ToList();
        F = context.Set<Fournisseur>().ToList();
    }
    public FactureViewModel(DbContext context)
    {
        A = context.Set<Association>().ToList();
        F = context.Set<Fournisseur>().ToList();
    }

}