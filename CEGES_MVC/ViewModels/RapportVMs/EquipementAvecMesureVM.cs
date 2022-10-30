using CEGES_MVC.ViewModels.EquipementVMs;

namespace CEGES_MVC.ViewModels.RapportVMs
{
    public class EquipementAvecMesureVM
    {
        public string GroupeNom { get; set; }

        public int Mesure { get; set; }

        public EquipementVM EquipementVM { get; set; }
     
    }
}
