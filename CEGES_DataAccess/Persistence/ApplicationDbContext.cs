using CEGES_Models;
using CEGES_Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;


namespace CEGES_DataAccess.Persistence
{
    public class ApplicationDbContext : DbContext
    {

        //Constructor with DbContextOptions and the context class itself
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Entreprise> Entreprises { get; set; }
        public DbSet<Groupe> Groupes { get; set; }
        public DbSet<Rapport> Rapports { get; set; }

        //public DbSet<EquipementRapport> EquipementRapports { get; set; }



        //Ce n'est pas nécessaire d'utiliser les db sets suivants (pour l'instant).
        //Pour l'instant, le repo pattern utilise Equipements (classe générique abstraite) pour add/delete,
        //cependant, si l'on devais fetch seulement un type d'équipement, il serait peut-etre mieux d'implementer 
        //un repository (DbSet) pour chaque type d'équipement, sinon j'imagine qu'il faudrais filtrer par type à chaque requête.


        //public DbSet<EquipementConstant> EquipementConstants { get; set; }
        //public DbSet<EquipementLineaire> EquipementLineaires { get; set; }
        //public DbSet<EquipementRelatif> EquipementRelatifs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EquipementRapport>()
                .HasKey(er => new { er.EquipementId, er.RapportId });

            //Microsoft TPH doc: https://learn.microsoft.com/en-us/ef/core/modeling/inheritance#table-per-hierarchy-and-discriminator-configuration
            //LearnEntityFrameworkCore doc: https://www.learnentityframeworkcore.com/inheritance/table-per-hierarchy
            modelBuilder.Entity<Equipement>()
                .ToTable("Equipements")
                .HasDiscriminator(e => e.Type)
                .HasValue<EquipementConstant>(EquipementTypes.Constant)
                .HasValue<EquipementLineaire>(EquipementTypes.Lineaire)
                .HasValue<EquipementRelatif>(EquipementTypes.Relatif);

            modelBuilder.Entity<Equipement>()
                 .Property(e => e.Type)
                 .HasColumnName(nameof(Equipement.Type));
        }
    }
}
