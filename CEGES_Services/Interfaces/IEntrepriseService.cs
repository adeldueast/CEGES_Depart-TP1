using CEGES_Models;

namespace CEGES_MVC.Interfaces
{
    public interface IEntrepriseService
    {

        Task<IEnumerable<Entreprise>> GetAllWithGroupesWithEquipementsWithRapports();

        Task<object> GetAllWithPeriods();

        Task<Entreprise> GetByIdWithGroupesWithEquipements(int id);

        Task<Entreprise> GetById(int id);

        Task<int> Add(string nom);

        Task<int> Update(int id, string nom);

    }
}
