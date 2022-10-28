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

        //These next queries are the new SPECIAL queries
        //Special in the sens that they dont just return a domain entity, like others
        //but return a custom result using .Select 
        Task<IEnumerable<EntrepriseRapports>> GetByIdWithPeriods(int id);

        Task<IEnumerable<EntrepriseRapportsCount>> GetAllWithPeriodsCount();

    }
}
