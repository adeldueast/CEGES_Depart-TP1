using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IEntrepriseRepository Entreprises { get; }
        IGroupRepository Groupes { get; }
        IEquipementRepository Equipements { get; }
        IRapportRepository Rapports { get; }


        Task SaveChangesAsync();
    }
}
