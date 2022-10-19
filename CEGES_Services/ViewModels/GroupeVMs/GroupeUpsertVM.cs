using CEGES_Models;

namespace CEGES_Services.ViewModels.GroupeVMs
{
    public class GroupeUpsertVM
    {
        public int Id { get; set; } = 0;

        public string Nom { get; set; }

        public int EntrepriseId { get; set; }

        public string EntrepriseNom { get; set; }



    }
}
