using System.ComponentModel.DataAnnotations;
namespace Models;

public abstract class Utilisateur{
    public int Id{get;set;}

    [StringLength(100, MinimumLength = 3)]
    public abstract string Nom{get;set;}

    [StringLength(100, MinimumLength = 3)]
    public abstract string Prenom{get;set;}

    [StringLength(100, MinimumLength = 5)]
    public abstract string Email{get;set;}
    
    [StringLength(100, MinimumLength = 7)]
    public abstract string Password{get;set;}
}