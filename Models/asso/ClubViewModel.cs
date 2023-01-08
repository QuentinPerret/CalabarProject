using Microsoft.EntityFrameworkCore;

namespace Models;

public class ClubViewModel
{
    public List<Bureau> B { get; set; } = null!;
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int BureauId { get; set; }

    public ClubViewModel() { }
    public ClubViewModel(DbContext context, Club C)
    {
        Id = C.Id;
        Nom = C.Nom;
        Email = C.Email;
        BureauId = C.BureauId;

        B = context.Set<Bureau>().ToList();
    }
    public ClubViewModel(DbContext context)
    {
        B = context.Set<Bureau>().ToList();
    }

}