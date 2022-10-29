using CEGES_Models;
using CEGES_MVC.ViewModels.EquipementVMs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CEGES_MVC.ViewModels.RapportVMs
{
    public class RapportDetailsVM
    {
        public int Id { get; set; } = 0;

        public DateTime DateDebut { get; set; }

        public string EntrepriseNom { get; set; }

        public int EntrepriseId { get; set; }

        public Dictionary <string, List<EquipementAvecMesure>> GroupedEquipements { get; set; }


    }
}
