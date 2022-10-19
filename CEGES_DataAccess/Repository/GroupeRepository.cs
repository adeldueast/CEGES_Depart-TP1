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
    public class GroupeRepository : Repository<Groupe>, IGroupRepository
    {

        private readonly ApplicationDbContext _db;
        public GroupeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Groupe> GetByIdIncludeEquipements(int id)
        {
            return await _db.Groupes.Include(g => g.Equipements).SingleOrDefaultAsync(g => g.Id == id);
        }

        public void Update(Groupe groupe)
        {
           _db.Update(groupe);
        }
    }
}
