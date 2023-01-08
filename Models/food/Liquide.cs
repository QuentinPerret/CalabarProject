using System.ComponentModel.DataAnnotations;
namespace Models;

public abstract class Liquide : Consommable
{
    [Range(0, double.MaxValue, ErrorMessage = "Veuillez rentrer un nombre")]
    public double TailleBouteilleLitre { get; set; }
}