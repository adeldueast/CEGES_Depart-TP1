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


            modelBuilder.Entity<Entreprise>().HasData(new Entreprise
            {
                Id = 1,
                Nom = "Ubisoft"
            });


           
            modelBuilder.Entity<Groupe>().HasData(
               new Groupe
               {
                   Id = 1,
                   Nom = "Groupe 1",
                   EntrepriseId = 1
               },
               new Groupe
               {
                   Id = 2,
                   Nom = "Groupe 2",
                   EntrepriseId = 1

               });

            //modelBuilder.Entity<Equipement>().HasData(
            //  new EquipementConstant
            //  {
            //      Id = 1,
            //      Type = EquipementTypes.Constant,
            //      GroupeId = 1,
            //      Nom = "equ constant"
            //  },
            //  new EquipementLineaire
            //  {
            //      Id = 2,
            //      Type = EquipementTypes.Lineaire,
            //      GroupeId = 1,
            //      Nom = "equ lineaire",
                  


            //  }, new EquipementRelatif
            //  {
            //      Id = 3,
            //      Type = EquipementTypes.Relatif,
            //      GroupeId = 1,
            //      Nom = "equ relatif"

            //  });


            //modelBuilder.Entity<EquipementRapport>().HasData(
            //  new EquipementRapport
            //  {
            //      EquipementId = 1,
            //      RapportId = 1,
            //      Mesure = 10

            //  },
            //  new EquipementRapport
            //  {
            //      EquipementId = 2,
            //      RapportId = 1,
            //      Mesure = 10

            //  }, new EquipementRapport
            //  {
            //      EquipementId = 3,
            //      RapportId = 1,
            //      Mesure = 10

            //  });

            modelBuilder.Entity<Rapport>().HasData(new Rapport
            {
                Id = 1,
                DateDebut = DateTime.Now,
            });



        }
    }
}
