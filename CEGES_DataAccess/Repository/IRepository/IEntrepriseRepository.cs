using CEGES_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_DataAccess.Repository.IRepository
{
    
    public interface IEntrepriseRepository : IRepository<Entreprise>
    {
        void Update(Entreprise entreprise);

        Task<List<Entreprise>> GetAllWithGroupesWithEquipements();

        Task<Entreprise> GetByIdWithGroupesWithEquipements(int id);
    }
}
