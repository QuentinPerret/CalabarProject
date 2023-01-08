using System.ComponentModel.DataAnnotations;
namespace Models;

public class SoftDTO
{
    public int Id { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Veuillez rentrer un nombre entier")]
    public int Quantite { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Veuillez rentrer un nombre")]
    public double PrixAchat { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Veuillez rentrer un nombre")]
    public double PrixVente { get; set; }
    public int AssociationId { get; set; }
    public double TailleBouteilleLitre { get; set; }
    public string Nom { get; set; } = null!;
}