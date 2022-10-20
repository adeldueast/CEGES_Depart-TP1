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
            CreateMap<Equipement, EquipementVM>()
                   .Include<EquipementConstant, EquipementConstantVM>()
                   .Include<EquipementLineaire, EquipementLineaireVM>()
                   .Include<EquipementRelatif, EquipementRelatifVM>()
                   .ReverseMap();

            CreateMap<EquipementConstant, EquipementConstantVM>().ReverseMap();
            CreateMap<EquipementLineaire, EquipementLineaireVM>().ReverseMap();
            CreateMap<EquipementRelatif, EquipementRelatifVM>().ReverseMap();



        }
    }
}
