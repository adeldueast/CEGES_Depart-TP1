using CEGES_Models;
using CEGES_Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_MVC.ViewModels.EquipementVMs
{
    public class EquipementConstantVM : EquipementVM
    {

   

        [DisplayName("Émissions")]
        public float Quantite { get; set; }


        public EquipementConstantVM() : base(EquipementTypes.Constant, EquipementDescriptions.Constant)
        {

        }

    }
}
