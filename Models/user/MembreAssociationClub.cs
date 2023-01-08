namespace Models;

public abstract class MembreAssociationClub : Utilisateur
{
    public List<Association>? Associations { get; set; }


    public MembreAssociationClub()
    {
        Associations = new List<Association> { };
    }
}
