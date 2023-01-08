namespace Models;

public class VinDTO
{
    public int Id { get; set; }
    public int ChateauId { get; set; }
    public int Millesime { get; set; }
    public string Nom { get; set; } = null!;
    public int Quantite { get; set; }
    public double PrixAchat { get; set; }
    public double PrixVente { get; set; }
    public int AssociationId { get; set; }
    public double TailleBouteilleLitre { get; set; }
    public typeDeVin TypeDeVin { get; set; }

}