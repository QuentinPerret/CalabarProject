using System.ComponentModel.DataAnnotations;
namespace Models;

public class Club : Association
{
    public Bureau Bureau { get; set; } = null!;
    public int BureauId { get; set; }
    [StringLength(100, MinimumLength = 3)]
    public override string Nom { get; set; } = null!;
    [StringLength(100, MinimumLength = 3)]
    public override string Email { get; set; } = null!;

    public Club() { }

    public Club(ClubDTO clubDTO)
    {
        Id = clubDTO.Id;
        Nom = clubDTO.Nom;
        Email = clubDTO.Email;
        BureauId = clubDTO.BureauId;
    }

    public Club(ClubViewModel club)
    {
        Id = club.Id;
        Nom = club.Nom;
        Email = club.Email;
        BureauId = club.BureauId;
    }
}
