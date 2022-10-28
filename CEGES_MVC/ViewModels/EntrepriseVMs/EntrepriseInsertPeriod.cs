using CEGES_Models;
using CEGES_MVC.ViewModels.GroupeVMs;
using System.Collections.Generic;

namespace CEGES_MVC.ViewModels.EntrepriseVMs
{
    public class EntrepriseInsertPeriod
    {
            public int Id { get; set; } = 0;

            public string Nom { get; set; }

        public IEnumerable<GroupeDetailsVM> Groupes { get; set; }

    }
}
