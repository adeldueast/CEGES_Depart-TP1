using CEGES_DataAccess.Persistence;
using CEGES_DataAccess.Repository.IRepository;
using CEGES_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_DataAccess.Repository
{
    public class RapportRepository : Repository<Rapport>, IRapportRepository
    {
        private readonly ApplicationDbContext _db;

        public RapportRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Rapport rapport)
        {
            _db.Update(rapport);
        }
    }
}
