namespace Models;

public class ClubDTO
{
    public int Id { get; set; }
    public int BureauId { get; set; }
    public string Nom { get; set; } = null!;
    public string Email { get; set; } = null!;
}