using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_Models
{
    public class EquipementRapport
    {
        public int EquipementId { get; set; }
        public Equipement Equipement { get; set; }


        public int RapportId { get; set; }
        public Rapport Rapport { get; set; }

        public int Mesure { get; set; }
    }
}
