namespace Models;

public class UtilisateurCommun : Utilisateur{
    public bool? EstUnEtudiant{get;set;}

    public override string Nom{get;set;} = null!;
    public override string Prenom{get;set;} = null!;
    public override string Email{get;set;} = null!;
    public override string Password{get;set;} = null!;
}