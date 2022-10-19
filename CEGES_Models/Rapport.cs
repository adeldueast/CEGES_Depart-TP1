using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_Models
{
    public class Rapport
    {
        public int Id { get; set; }

        public DateTime DateDebut { get; set; }

        public DateTime DateFin { get; set; }

        public List<Equipement> Equipements { get; set; }

    }
}
