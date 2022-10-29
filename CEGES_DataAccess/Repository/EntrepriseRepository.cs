using CEGES_DataAccess.Persistence;
using CEGES_DataAccess.Repository.IRepository;
using CEGES_Models;
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
                .ThenInclude(e=>e.Rapports)
                .ToListAsync();
        }

        public async Task<EntrepriseRapports> GetByIdWithPeriods(int id)
        {

            var entrepriseWithRapports = await _db.Entreprises
                .Where(e => e.Id == id)
                .Select(e => new EntrepriseRapports
                {
                    Entreprise = e,
                    Rapports = e.Groupes
                    .SelectMany(g => g.Equipements)
                    .SelectMany(e => e.Rapports)
                    .Select(r => r.Rapport)
                    .Distinct()
                    .OrderBy(r => r.DateDebut)
                    .ToList()

                }).FirstOrDefaultAsync();


            return entrepriseWithRapports;
        }

        public async Task<IEnumerable<EntrepriseRapportsCount>> GetAllWithPeriodsCount()
        {

            var entrepriseWithRapportsCount = await _db.Entreprises
                .Select(e => new EntrepriseRapportsCount
                {
                    Entreprise = e,
                    RapportsCount = e.Groupes.SelectMany(g => g.Equipements).SelectMany(e => e.Rapports).Count()

                }).ToListAsync();


            return entrepriseWithRapportsCount.ToList();

        }

        public async Task<Entreprise> GetByIdWithGroupesWithEquipements(int id)
        {
            return await _db.Entreprises.Include(e => e.Groupes).ThenInclude(g => g.Equipements).SingleOrDefaultAsync(x => x.Id == id);

        }

        public void Update(Entreprise entreprise)
        {
            _db.Update(entreprise);
        }
    }
}
