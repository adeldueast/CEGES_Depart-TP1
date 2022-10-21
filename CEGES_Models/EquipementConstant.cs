using CEGES_Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_Models
{
    public class EquipementConstant : Equipement
    {
        public float Quantite { get; set; }

        public sealed override string Description { get; } = EquipementDescriptions.Constant;


    }
}
