using CEGES_DataAccess.Repository.IRepository;
using CEGES_Models;
using CEGES_Models.Exceptions;
using CEGES_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_Services.Implementation
{
    public class EquipementService : IEquipementService
    {
        private readonly IUnitOfWork _uow;

        public EquipementService(IUnitOfWork uow) => _uow = uow;

        public Task<int> Add(int groupeId, Equipement equipement)
        {
            throw new NotImplementedException();
        }

        public async Task<Equipement> GetById(int id)
        {
            var equipement = await _uow.Equipements.GetAsync(id);

            if(equipement == null)
            {
                throw new NotFoundException("Equipement", id);
            }

            return equipement;
        }
    }
}
