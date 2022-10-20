using CEGES_DataAccess.Persistence;
using CEGES_DataAccess.Repository.IRepository;

namespace CEGES_DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Entreprises = new EntrepriseRepository(_db);
            Groupes = new GroupeRepository(_db);
            Equipements = new EquipementRepository(_db);
            Rapports = new RapportRepository(_db);
        }




        public IEntrepriseRepository Entreprises { get; private set; }
        public IGroupRepository Groupes { get; private set; }
        public IEquipementRepository Equipements { get; private set; }
        public IRapportRepository Rapports { get; private set; }

        public void Dispose() => _db.Dispose();


        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}
