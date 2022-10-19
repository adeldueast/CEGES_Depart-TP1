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

        public async Task<Equipement> GetById(int id)
        {
            var equipement = await _uow.Equipements.GetAsync(id);

            if (equipement == null)
            {
                throw new NotFoundException("Equipement", id);
            }

            return equipement;
        }


        public async Task<int> Add(Equipement equipement)
        {

            _uow.Equipements.Add(equipement);

            await _uow.SaveChangesAsync();

            return equipement.Id;


        }


        public async Task<int> Update(Equipement equipement)
        {
            var equipementDomain = await _uow.Equipements.GetAsync(equipement.Id);

            if (equipementDomain == null)
            {
                throw new NotFoundException("Equipement", equipement.Id);
            }

            equipementDomain.Nom = equipement.Nom;
            equipementDomain.

            //_uow.Equipements.Update()
        }
    }
}
