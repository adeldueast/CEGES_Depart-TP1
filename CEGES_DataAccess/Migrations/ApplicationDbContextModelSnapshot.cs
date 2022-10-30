﻿// <auto-generated />
using System;
using CEGES_DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CEGES_DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CEGES_Models.Entreprise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Entreprises");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nom = "Ubisoft"
                        });
                });

            modelBuilder.Entity("CEGES_Models.Equipement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("GroupeId")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Type");

                    b.HasKey("Id");

                    b.HasIndex("GroupeId");

                    b.ToTable("Equipements", (string)null);

                    b.HasDiscriminator<string>("Type").HasValue("Equipement");
                });

            modelBuilder.Entity("CEGES_Models.EquipementRapport", b =>
                {
                    b.Property<int>("EquipementId")
                        .HasColumnType("int");

                    b.Property<int>("RapportId")
                        .HasColumnType("int");

                    b.Property<int>("Mesure")
                        .HasColumnType("int");

                    b.HasKey("EquipementId", "RapportId");

                    b.HasIndex("RapportId");

                    b.ToTable("EquipementRapport");
                });

            modelBuilder.Entity("CEGES_Models.Groupe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("EntrepriseId")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EntrepriseId");

                    b.ToTable("Groupes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EntrepriseId = 1,
                            Nom = "Groupe 1"
                        },
                        new
                        {
                            Id = 2,
                            EntrepriseId = 1,
                            Nom = "Groupe 2"
                        });
                });

            modelBuilder.Entity("CEGES_Models.Rapport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateDebut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateFin")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Rapports");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateDebut = new DateTime(2022, 10, 29, 16, 25, 36, 449, DateTimeKind.Local).AddTicks(6058),
                            DateFin = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("CEGES_Models.EquipementConstant", b =>
                {
                    b.HasBaseType("CEGES_Models.Equipement");

                    b.Property<float>("Quantite")
                        .HasColumnType("real");

                    b.HasDiscriminator().HasValue("Constant");
                });

            modelBuilder.Entity("CEGES_Models.EquipementLineaire", b =>
                {
                    b.HasBaseType("CEGES_Models.Equipement");

                    b.Property<float>("FacteurConversion")
                        .HasColumnType("real");

                    b.Property<string>("UniteMesure")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Lineaire");
                });

            modelBuilder.Entity("CEGES_Models.EquipementRelatif", b =>
                {
                    b.HasBaseType("CEGES_Models.Equipement");

                    b.Property<float>("IntensiteMax")
                        .HasColumnType("real");

                    b.Property<float>("IntensiteZero")
                        .HasColumnType("real");

                    b.HasDiscriminator().HasValue("Relatif");
                });

            modelBuilder.Entity("CEGES_Models.Equipement", b =>
                {
                    b.HasOne("CEGES_Models.Groupe", "Groupe")
                        .WithMany("Equipements")
                        .HasForeignKey("GroupeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Groupe");
                });

            modelBuilder.Entity("CEGES_Models.EquipementRapport", b =>
                {
                    b.HasOne("CEGES_Models.Equipement", "Equipement")
                        .WithMany("Rapports")
                        .HasForeignKey("EquipementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CEGES_Models.Rapport", "Rapport")
                        .WithMany("Equipements")
                        .HasForeignKey("RapportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipement");

                    b.Navigation("Rapport");
                });

            modelBuilder.Entity("CEGES_Models.Groupe", b =>
                {
                    b.HasOne("CEGES_Models.Entreprise", "Entreprise")
                        .WithMany("Groupes")
                        .HasForeignKey("EntrepriseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entreprise");
                });

            modelBuilder.Entity("CEGES_Models.Entreprise", b =>
                {
                    b.Navigation("Groupes");
                });

            modelBuilder.Entity("CEGES_Models.Equipement", b =>
                {
                    b.Navigation("Rapports");
                });

            modelBuilder.Entity("CEGES_Models.Groupe", b =>
                {
                    b.Navigation("Equipements");
                });

            modelBuilder.Entity("CEGES_Models.Rapport", b =>
                {
                    b.Navigation("Equipements");
                });
#pragma warning restore 612, 618
        }
    }
}
