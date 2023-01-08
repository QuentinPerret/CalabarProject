namespace Models;


public class BiereDTO{
    public int Id{get;set;}
    public string Nom{get;set;}=null!;
    public typeDeBiere TypeDeBiere{get;set;}
    public double Degre{get;set;}
    public int BrasserieId{get;set;}

    public int Quantite{get;set;}
    public double PrixAchat{get;set;}
    public double PrixVente{get;set;}
    public int AssociationId{get;set;}
    public double TailleBouteilleLitre {get;set;}
}