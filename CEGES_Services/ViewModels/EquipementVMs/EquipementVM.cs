using CEGES_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_Services.ViewModels.EquipementVMs
{
    //Had to remove abstract so the post works, otherwise i would have to make a custom model binder.. no time for that
    public abstract class EquipementVM
    {

        public int Id { get; set; } = 0;

        public string Nom { get; set; }

        public  string Type { get; set; }

        public int GroupeId { get; set; }

        public EquipementVM(string Type)
        {
            this.Type = Type;
        }
    }
}
