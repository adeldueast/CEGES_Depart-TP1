using CEGES_DataAccess.Persistence;
using CEGES_DataAccess.Repository.IRepository;
using CEGES_Models;
using CEGES_Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_DataAccess.Repository
{
    public class EntrepriseRepository : Repository<Entreprise>, IEntrepriseRepository
    {
        private readonly ApplicationDbContext _db;

        public EntrepriseRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Entreprise>> GetAllWithGroupesWithEquipementsWithRapports()
        {
            return await _db.Entreprises
                .Include(e => e.Groupes)
                .ThenInclude(g => g.Equipements)
                .ThenInclude(e => e.Rapports)
                .ToListAsync();
        }

        public async Task<EntrepriseRapports> GetByIdWithRapports(int id)
        {


            //// entreprise's reports
            //var rapports = await _db.Rapports
            //    .Where(r => r.Equipements.Any(e => e.Equipement.Groupe.EntrepriseId == id))
            //    .ToListAsync();


            //// entreprise AND its reports (both combined)
            //var EntrepriseAndItsRapports = await _db.Rapports
            //    .Where(r => r.Equipements.Any(e => e.Equipement.Groupe.EntrepriseId == id))
            //    .ToListAsync().Result
            //    .Select(r=> )


            var entrepriseWithRapports = await _db.Entreprises
                .Where(e => e.Id == id)
                .Select(e => new EntrepriseRapports
                {
                    Entreprise = e,
                    Rapports = e.Groupes
                    .SelectMany(g => g.Equipements)
                    .SelectMany(e => e.Rapports)
                    .Select(r => r.Rapport)
                    .ToList()


                }).FirstOrDefaultAsync();

            //TODO: p-t override le rapport Equal/IComparer pour comparer l'entite par Id.. mais pour linstant load tout les rapports en memoire 
            entrepriseWithRapports.Rapports = entrepriseWithRapports.Rapports
                .DistinctBy(r => r.Id)
                .OrderBy(r => r.DateDebut);

            return entrepriseWithRapports;
        }

        public async Task<IEnumerable<EntrepriseRapportsCount>> GetAllWithRapportsCount()
        {

            var entrepriseWithRapportsCount = await _db.Entreprises
                .Select(e => new EntrepriseRapportsCount
                {
                    Entreprise = e,
                    RapportsCount = e.Groupes
                    .SelectMany(g => g.Equipements)
                    .SelectMany(e => e.Rapports)
                    .Select(e => e.RapportId)
                    .Distinct()
                    .Count()

                }).ToListAsync();


            return entrepriseWithRapportsCount;

        }

        public async Task<Entreprise> GetByIdWithGroupesWithEquipements(int id)
        {
            var entreprise =  await _db.Entreprises.Include(e => e.Groupes).ThenInclude(g => g.Equipements).SingleOrDefaultAsync(x => x.Id == id);
            if (entreprise is null) throw new NotFoundException(nameof(Entreprise), id);

            return entreprise;
        }

        public void Update(Entreprise entreprise)
        {
            _db.Update(entreprise);
        }
    }
}
