using CEGES_DataAccess.Repository.IRepository;
using CEGES_Models;
using CEGES_Models.Exceptions;
using CEGES_MVC.Interfaces;


namespace CEGES_MVC.Implementation
{
    public class GroupService : IGroupeService
    {

        private readonly IUnitOfWork _uow;

        public GroupService(IUnitOfWork uow) => _uow = uow;

        public async Task<Groupe> GetById(int id)
        {
            var groupe = await _uow.Groupes.GetByIdWithEntrepriseWithEquipements(id);

            if (groupe == null)
            {
                throw new NotFoundException("Groupe", id);
            }

            return groupe;
        }

        public async Task<int> Add(int entrepriseId, string nom)
        {


            var groupe = new Groupe()
            {
                Nom = nom,
                EntrepriseId = entrepriseId,
            };

            _uow.Groupes.Add(groupe);

            await _uow.SaveChangesAsync();

            return groupe.Id;
        }

        public async Task<int> Update(int id, string nom)
        {

            var groupe = await _uow.Groupes.GetAsync(id);

            if (groupe == null)
            {
                throw new NotFoundException("Groupe", id);
            }

            groupe.Nom = nom;

            _uow.Groupes.Update(groupe);

            await _uow.SaveChangesAsync();

            return groupe.Id;
        }

        
    }
}
