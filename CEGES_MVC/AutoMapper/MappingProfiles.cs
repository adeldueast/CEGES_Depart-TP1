using AutoMapper;
using CEGES_Models;
using CEGES_MVC.ViewModels.EntrepriseVM;
using CEGES_MVC.ViewModels.EquipementVMs;
using CEGES_MVC.ViewModels.GroupeVMs;
using CEGES_MVC.ViewModels.RapportVMs;
using System.Linq;
using System.Net;

namespace CEGES_MVC.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //CreateMap<Source, Destination>();
            #region Entreprise mapping profiles
            CreateMap<Entreprise, EntrepriseSummaryVM>()
                .ForMember(dest => dest.GroupesCount, opt => opt.MapFrom(src => src.Groupes.Count))
                .ForMember(dest => dest.EquipementsCount, opt => opt.MapFrom(src => src.Groupes.SelectMany(x => x.Equipements).Count()));
            CreateMap<Entreprise, EntrepriseDetailsVM>();

            CreateMap<Entreprise, RapportInsertVM>().ReverseMap();


            #endregion

            #region Groupe mapping profiles
            CreateMap<Groupe, GroupeSummaryVM>()
                .ForMember(dest => dest.EquipementCount, opt => opt.MapFrom(src => src.Equipements.Count));
            CreateMap<Groupe, GroupeDetailsVM>()
                .ForMember(dest => dest.EntrepriseNom, opt => opt.MapFrom(src => src.Entreprise.Nom));
            ;
            #endregion

            #region Equipement mapping profiles
            //see docs pour inheritence: https://docs.automapper.org/en/stable/Mapping-inheritance.html
            CreateMap<Equipement, EquipementVM>()
                   .Include<EquipementConstant, EquipementConstantVM>()
                   .Include<EquipementLineaire, EquipementLineaireVM>()
                   .Include<EquipementRelatif, EquipementRelatifVM>()
                   .ForMember(dest => dest.GroupeNom, opt => opt.MapFrom(src => src.Groupe.Nom))
                   .ReverseMap();
            CreateMap<EquipementConstant, EquipementConstantVM>().ReverseMap();
            CreateMap<EquipementLineaire, EquipementLineaireVM>().ReverseMap();
            CreateMap<EquipementRelatif, EquipementRelatifVM>().ReverseMap();


            #endregion

            #region Rapports mapping profiles

            CreateMap<EntrepriseRapportsCount, EntrepriseRapportsCountVM>()
          .ForMember(dest => dest.EntrepriseId, opt => opt.MapFrom(src => src.Entreprise.Id))
          .ForMember(dest => dest.EntrepriseNom, opt => opt.MapFrom(src => src.Entreprise.Nom));

            #endregion

        }
    }
}
