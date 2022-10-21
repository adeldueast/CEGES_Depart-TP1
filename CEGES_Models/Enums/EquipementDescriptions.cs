using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEGES_Models.Enums
{
    public class EquipementDescriptions
    {

        public const string Constant = "Certains équipements ont des émissions constantes peu importe leur utilisation.\r\n\r\nPar exemple, un bureau administratif ou un système d’éclairage utilisera toujours la même quantité d’énergie par jour ou par mois.\r\n\r\nLes émissions sont calculées en multipliant la quantité (en tonnes par jour) par la durée de la période à calculer.";

        public const string Lineaire = "Certains équipements ont des émissions linéaires qui sont directement proportionnelles à leur utilisation.\r\n\r\nPar exemple, un camion produira toujours la même quantité d’émissions par kilomètre parcouru.\r\n\r\nLes émissions sont calculées en multipliant le facteur de conversion par l’utilisation de l’équipement durant la période.";

        public const string Relatif = "Certains équipements ont des émissions proportionnelles à l’intensité de leur utilisation.\r\n\r\nPar exemple, une aluminerie produira toujours une certaine quantité d’émissions même sans être utilisée (à 0% d’intensité), et les émissions augmenteront ensuite proportionnellement jusqu’à l’utilisation maximum de l’aluminerie (à 100% d’intensité).\r\n\r\nLes émissions (en tonnes par jour) sont calculées en faisant une interpolation (une règle de trois) entre l’émission à intensité nulle et l’émission à intensité maximale selon l’intensité moyenne durant la période. Cette émission moyenne est ensuite multipliée par la durée de la période.";


    }
}
