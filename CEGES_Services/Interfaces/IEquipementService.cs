using CEGES_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_Services.Interfaces
{
    public interface IEquipementService
    {

        Task<Equipement> GetById(int id);

        Task<int> Add(int groupeId, Equipement equipement);

        Task<int> Update(int groupeId, Equipement equipement);


        //Task<int> Update(int id, string nom);
    }
}
