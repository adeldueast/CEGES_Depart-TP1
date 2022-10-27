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

        public async Task<List<Entreprise>> GetAllWithGroupesWithEquipements()
        {
            return await _db.Entreprises.Include(e => e.Groupes).ThenInclude(g => g.Equipements).ToListAsync();
        }

        public async Task<object> GetAllWithPeriods()
        {

            var entrepriseWithRapports = await _db.Entreprises.Select(e => new
            {
                Entreprise = e,
                RapportsCount = e.Groupes.SelectMany(g => g.Equipements).SelectMany(e => e.Rapports).ToList().Count

            }).ToListAsync();

            //var rapportEntreprise = _db.Rapports.Select(r => new { Rapport = r, Entreprise = r.Equipements.Select(y => y.Groupe.Entreprise).ToList() }).ToList();

            return entrepriseWithRapports;

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
