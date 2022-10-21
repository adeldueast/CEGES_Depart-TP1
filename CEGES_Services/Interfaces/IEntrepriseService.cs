using CEGES_Models;

namespace CEGES_MVC.Interfaces
{
    public interface IEntrepriseService
    {

        Task<IEnumerable<Entreprise>> GetAll();


        Task<Entreprise> GetById(int id);

        Task<int> Add(string nom);

        Task<int> Update(int id, string nom);

    }
}
