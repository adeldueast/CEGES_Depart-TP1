using AutoMapper;
using CEGES_Models;
using CEGES_Services.ViewModels.EquipementVMs;
using CEGES_Services.ViewModels.GroupeVMs;
using System.Net;

namespace CEGES_MVC.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //CreateMap<Source, Destination>();
            CreateMap<Groupe, GroupeDetailsVM>();



            //see docs: https://docs.automapper.org/en/stable/Mapping-inheritance.html

            // maybe delete later
            CreateMap<Equipement, EquipementVM>();

            CreateMap<EquipementConstant, EquipementConstantVM>();

            CreateMap<EquipementLineaire, EquipementLineaireVM>();

            CreateMap<EquipementRelatif, EquipementRelatifVM>();



        }
    }
}
