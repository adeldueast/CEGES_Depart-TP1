using CEGES_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_DataAccess.Repository.IRepository
{
    public  interface IGroupRepository : IRepository<Groupe>
    {
        Task<Groupe> GetByIdIncludeEntrepriseAndEquipements(int id); 
        void Update(Groupe groupe);
    }
}
