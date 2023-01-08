using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Models;

namespace Data;

public class CalabarContext : DbContext
{
    public DbSet<Utilisateur> Utilisateurs { get; set; } = null!;
    public DbSet<Consommable> Consommables { get; set; } = null!;
    public DbSet<Association> Associations { get; set; } = null!;
    public DbSet<Fournisseur> Fournisseurs { get; set; } = null!;
    public DbSet<Facture> Factures { get; set; } = null!;
    public string DbPath { get; private set; }


    public CalabarContext()
    {
        // Path to SQLite database file
        DbPath = " EFCalabar.db";
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        //Entity in Consommable Table
        modelBuilder.Entity<Liquide>().ToTable("Liquides");
        modelBuilder.Entity<Biere>().ToTable("Bieres");
        modelBuilder.Entity<Vin>().ToTable("Vins");
        modelBuilder.Entity<Soft>().ToTable("Softs");
        modelBuilder.Entity<Nourriture>().ToTable("Nourritures");

        //Entity in Utilisateur Table
        modelBuilder.Entity<Administrateur>().ToTable("Administrateurs");
        modelBuilder.Entity<Collaborateur>().ToTable("Collaborateurs");
        modelBuilder.Entity<MembreAssociationClub>().ToTable("MembreAssociationClubs");
        modelBuilder.Entity<UtilisateurCommun>().ToTable("UtilisateurCommuns");

        //Entity in Fournisseur Table
        modelBuilder.Entity<Chateau>().ToTable("Chateaux");
        modelBuilder.Entity<Brasserie>().ToTable("Brasseries");
        modelBuilder.Entity<Supermarche>().ToTable("Supermarches");

        //Entity in Association Table
        modelBuilder.Entity<Bureau>().ToTable("Bureaux");
        modelBuilder.Entity<Club>().ToTable("Clubs");

        //Entity relations (create list relations)
        modelBuilder.Entity<Association>().HasMany(a => a.MembreAssociationClubs).WithMany(a => a.Associations).UsingEntity(j => j.ToTable("UserAssos"));
        modelBuilder.Entity<Club>().HasOne(c => c.Bureau).WithMany(b => b.Clubs).HasForeignKey(c => c.BureauId);
        modelBuilder.Entity<Facture>().HasOne(f => f.Association).WithMany(a => a.Factures).HasForeignKey(f => f.AssociationId);
        modelBuilder.Entity<Facture>().HasOne(f => f.Fournisseur).WithMany(f => f.Factures).HasForeignKey(f => f.FournisseurId);
        // modelBuilder.Entity<Association>().HasOne(f => f.President).WithMany(u => u.Preside).HasForeignKey(f => f.PresidentId);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableIdentifier = StoreObjectIdentifier.Create(entityType, StoreObjectType.Table);

            Console.WriteLine($"{entityType.DisplayName()}\t{tableIdentifier}");
            Console.WriteLine("Property\tColumn");

            foreach (var property in entityType.GetProperties())
            {
                var columnName = property.GetColumnName(tableIdentifier!.Value);
                Console.WriteLine($"{property.Name,-10}\t{columnName}");
            }

            foreach (var skipNavigation in entityType.GetSkipNavigations())
            {
                Console.WriteLine(entityType.DisplayName() + "." + skipNavigation.Name);
            }

            Console.WriteLine();
        }
    }

    // The following configures EF to create a SQLite database file locally
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Use SQLite as database
        options.UseSqlite($"Data Source={DbPath}");
        options.EnableSensitiveDataLogging();
        // Optional: log SQL queries to console
        // options.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
    }
}
