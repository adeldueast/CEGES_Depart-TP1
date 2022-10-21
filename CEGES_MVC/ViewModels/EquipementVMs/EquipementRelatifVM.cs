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

        public EquipementRelatifVM() : base(TypeEquipmentEnumeration.Relatif)
        {

        }

    }
}
