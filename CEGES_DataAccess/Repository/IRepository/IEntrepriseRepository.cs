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

        Task<List<Entreprise>> GetAllWithGroupesWithEquipementsWithRapports();

        Task<Entreprise> GetByIdWithGroupesWithEquipements(int id);

        //These next queries are the new SPECIAL queries
        //Special in the sens that they dont just return a domain entity, like others
        //but return a custom result using .Select 
        Task<EntrepriseRapports> GetByIdWithRapports(int id);

        Task<IEnumerable<EntrepriseRapportsCount>> GetAllWithRapportsCount();

    }
}
