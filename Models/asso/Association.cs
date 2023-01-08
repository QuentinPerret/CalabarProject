using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace Models;

public abstract class Association
{
    public int Id { get; set; }
    [StringLength(100, MinimumLength = 3)]
    public abstract string Nom { get; set; }
    [StringLength(100, MinimumLength = 3)]
    public abstract string Email { get; set; }
    public List<MembreAssociationClub> MembreAssociationClubs { get; set; } = null!;
    public List<Facture> Factures { get; set; } = null!;

    public Association()
    {
        MembreAssociationClubs = new List<MembreAssociationClub> { };
        Factures = new List<Facture> { };
    }
}
