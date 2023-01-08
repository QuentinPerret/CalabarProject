using System.ComponentModel.DataAnnotations;
namespace Models;

public class Bureau : Association
{
    public List<Club> Clubs { get; set; } = null!;
    [StringLength(100, MinimumLength = 3)]
    public override string Nom { get; set; } = null!;
    [StringLength(100, MinimumLength = 3)]
    public override string Email { get; set; } = null!;

    public Bureau()
    {
        Clubs = new List<Club>() { };
    }

    public Bureau(BureauDTO bureauDTO)
    {
        Id = bureauDTO.Id;
        Nom = bureauDTO.Nom;
        Email = bureauDTO.Email;
    }
}
