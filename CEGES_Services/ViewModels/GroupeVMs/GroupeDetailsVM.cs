using CEGES_Models;
using CEGES_Services.ViewModels.EquipementVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_Services.ViewModels.GroupeVMs
{
    public class GroupeDetailsVM
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public List<EquipementVM> Equipements { get; set; } = new();
    }
}
