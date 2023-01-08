using System.ComponentModel.DataAnnotations;
namespace Models;

public class NourritureDTO
{
    public int Id { get; set; }
    public int Quantite { get; set; }
    public double PrixAchat { get; set; }
    public double PrixVente { get; set; }
    public int AssociationId { get; set; }
    public string Nom { get; set; } = null!;
}