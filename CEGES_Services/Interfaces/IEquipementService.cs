using CEGES_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_MVC.Interfaces
{
    public interface IEquipementService
    {

        Task<Equipement> GetById(int id);

        Task<int> Add(Equipement equipement);

        Task<int> Update(Equipement equipement);

    }
}
