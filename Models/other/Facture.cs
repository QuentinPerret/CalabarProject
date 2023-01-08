namespace Models;

public class Facture
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public double PrixHT { get; set; }
    public double PrixTTC { get; set; }
    public DateTime Date { get; set; }
    public int FournisseurId { get; set; }
    public Fournisseur Fournisseur { get; set; } = null!;
    public int AssociationId { get; set; }
    public Association Association { get; set; } = null!;

    public Facture() { }

    public Facture(FactureDTO facture)
    {
        Id = facture.Id;
        Numero = facture.Numero;
        PrixHT = facture.PrixHT;
        PrixTTC = facture.PrixTTC;
        Date = facture.Date;
        FournisseurId = facture.FournisseurId;
        AssociationId = facture.AssociationId;
    }

    public Facture(FactureViewModel facture)
    {
        Id = facture.Id;
        Numero = facture.Numero;
        PrixHT = facture.PrixHT;
        PrixTTC = facture.PrixTTC;
        Date = facture.Date;
        FournisseurId = facture.FournisseurId;
        AssociationId = facture.AssociationId;
    }

}