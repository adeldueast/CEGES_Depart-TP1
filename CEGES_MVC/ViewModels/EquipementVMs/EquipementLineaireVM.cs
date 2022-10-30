using CEGES_Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_MVC.ViewModels.EquipementVMs
{
    public class EquipementLineaireVM : EquipementVM
    {
        [DisplayName("Unité de mesure")]
        public string UniteMesure { get; set; }

        [DisplayName("Facteur de conversion")]
        public float FacteurConversion { get; set; }



        public EquipementLineaireVM() : base(EquipementTypes.Lineaire,EquipementDescriptions.Lineaire)
        {

        }

        public override decimal CalculateMensuel(int mesure)
        {
            return (decimal)(mesure * FacteurConversion);
        }
    }
}
