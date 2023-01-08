namespace Models;

public abstract class Fournisseur
{
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public string Siret { get; set; } = null!;
    public string Adresse { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Telephone { get; set; } = null!;
    public List<Facture>? Factures { get; set; }

    public Fournisseur()
    {
        Factures = new List<Facture> { };
    }
}