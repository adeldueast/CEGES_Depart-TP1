using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_Models
{
    public class Groupe
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public int EntrepriseId { get; set; }
        public Entreprise Entreprise { get; set; }

        public List<Equipement> Equipements { get; set; }
    }
}
