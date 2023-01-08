namespace Models;
public class UserAsso
{
    public int UserId { get; set; }
    public MembreAssociationClub User { get; set; } = null!;
    public int AssoId { get; set; }
    public Association Asso { get; set; } = null!;
}