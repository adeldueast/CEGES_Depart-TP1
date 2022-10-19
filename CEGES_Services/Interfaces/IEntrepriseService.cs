using CEGES_Models;
using CEGES_Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_Services.Interfaces
{
    public interface IEntrepriseService
    {

        Task<IEnumerable<Entreprise>> GetAllSummaries();


        Task<Entreprise> GetById(int id);

        Task<int> Add(string nom);

        Task<int> Update(int id, string nom);

    }
}
