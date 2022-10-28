using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_Models
{
    public class EntrepriseRapports
    {
        public Entreprise Entreprise { get; set; }

        public IEnumerable<Rapport> Rapports{ get; set; }
    }


}
