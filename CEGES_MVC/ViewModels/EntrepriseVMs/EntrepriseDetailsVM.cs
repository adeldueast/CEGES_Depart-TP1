using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CEGES_MVC.ViewModels.GroupeVMs;

namespace CEGES_MVC.ViewModels.EntrepriseVM
{
    public class EntrepriseDetailsVM
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public List<GroupeSummaryVM> Groupes { get; set; }
    }
}
