using CEGES_Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_MVC.ViewModels.EquipementVMs
{
    public class EquipementRelatifVM : EquipementVM
    {

        [DisplayName("Minimum")]
        public float IntensiteZero { get; set; }

        [DisplayName("Maximum")]
        public float IntensiteMax { get; set; }


        public int Mesure { get; set; }


        public EquipementRelatifVM() : base(EquipementTypes.Relatif, EquipementDescriptions.Lineaire)
        {

        }

        public override decimal CalculateMensuel(int mesure)
        {
            return (decimal)((IntensiteZero + (IntensiteMax - IntensiteZero) * mesure) * 30);

            //( (0.07 tonne/jour + (2.42 tonnes/jour – 0.07 tonne/jour) × 72%) × 30 jours/mois)
        }
    }
}
