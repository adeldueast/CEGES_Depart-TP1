using CEGES_Models;
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

        public DbSet<EquipementConstant> EquipementConstants { get; set; }
        public DbSet<EquipementLineaire> EquipementLineaires { get; set; }
        public DbSet<EquipementRelatif> EquipementRelatifs { get; set; }

         
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipement>()
                .ToTable("Equipements")
                .HasDiscriminator(e => e.Type)
                .HasValue<EquipementConstant>("Constant")
                .HasValue<EquipementLineaire>("Lineaire")
                .HasValue<EquipementRelatif>("Relatif");

            modelBuilder.Entity<Equipement>()
                 .Property(e => e.Type)
                 .HasColumnName("Type");
        }
    }
}
