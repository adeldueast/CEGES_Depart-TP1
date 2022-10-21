using CEGES_Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_Models
{
    public class EquipementRelatif : Equipement
    {
        public float IntensiteZero { get; set; }

        public float IntensiteMax { get; set; }

        public sealed override   string Description { get; } = EquipementDescriptions.Relatif;
    }
}
