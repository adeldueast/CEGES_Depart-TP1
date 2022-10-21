using CEGES_Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_Models
{
    public class EquipementLineaire : Equipement
    {
        public string UniteMesure { get; set; }

        public int FacteurConversion { get; set; }

        public sealed override string Description { get; } = EquipementDescriptions.Lineaire;
    }
}
