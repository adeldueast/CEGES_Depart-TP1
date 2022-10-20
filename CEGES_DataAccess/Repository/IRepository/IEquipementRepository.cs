using CEGES_Models;


namespace CEGES_DataAccess.Repository.IRepository
{
    public  interface IEquipementRepository : IRepository<Equipement>
    {
        void Update(Equipement equipement);
    }
}
