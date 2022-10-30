using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CEGES_Models.Enums;

namespace CEGES_Models
{
    public abstract class Equipement
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Type { get; set; }

        public int GroupeId { get; set; }

        public Groupe Groupe { get; set; }

        public List<EquipementRapport> Rapports { get; set; }


        [NotMapped]
        public abstract string Description { get; }



    }

}
