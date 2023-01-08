namespace Models;

public class FactureDTO
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public double PrixHT { get; set; }
    public double PrixTTC { get; set; }
    public DateTime Date { get; set; }
    public int FournisseurId { get; set; }
    public int AssociationId { get; set; }

}