using System.ComponentModel.DataAnnotations;
namespace Models;
public abstract class Consommable
{

    public int Id { get; set; }

    [StringLength(100, MinimumLength = 3)]
    public string Nom { get; set; } = null!;
    public int Quantite { get; set; }
    public double PrixAchat { get; set; }
    public double PrixVente { get; set; }
    public int AssociationId { get; set; }
    public Association Association { get; set; } = null!;
    public List<Facture>? Factures { get; set; }

}