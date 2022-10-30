using AutoMapper;
using CEGES_Models;
using CEGES_Models.Enums;
using CEGES_MVC.Interfaces;
using CEGES_MVC.ViewModels.EquipementVMs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CEGES_MVC.Areas.Configuration.Controllers
{
    [Area("Configuration")]
    public class EquipementController : Controller
    {

        private readonly IGroupeService _groupeService;
        private readonly IEquipementService _equipementService;
        private readonly IMapper _mapper;

        public EquipementController(IGroupeService groupeService, IEquipementService equipementService, IMapper mapper)
        {
            _groupeService = groupeService;
            _equipementService = equipementService;
            _mapper = mapper;
        }

        public async Task<IActionResult> InsertType(int id)
        {
            //Fecht le groupe avec Id , et throw error s'il n'existe pas. Ne renvoie rien à la Vue.
            var groupe = await _groupeService.GetById(id);

            return View();
        }

        public IActionResult Insert(int id, [FromQuery] string type)
        {

            //TODO:Check that id is an existing entity (groupe)

            //Ici, pas le choix de faire un switch case pour determiner le type d'equipement. On ne px pas utiliser le mapper
            //il est important de specifier le groupe Id de l'equipement que nous allons creer afin de creer une relation
            EquipementVM equipementVM;
            switch (type)
            {
                case EquipementTypes.Constant:
                    equipementVM = new EquipementConstantVM() { GroupeId = id };
                    break;
                case EquipementTypes.Lineaire:
                    equipementVM = new EquipementLineaireVM() { GroupeId = id };
                    break;
                case EquipementTypes.Relatif:
                    equipementVM = new EquipementRelatifVM() { GroupeId = id };
                    break;
                default:
                    return NotFound();
            }

            return View("Upsert", equipementVM);
        }




        public async Task<IActionResult> Update(int id)
        {
            var equipement = await _equipementService.GetById(id);
            //Contrairement au Insert, nous ne devons pas specifier le Id du groupe, car nous allons updater un equipement déja existant, ayant déja un groupe.
            //Deux méthode pour mapper, soit manuellement (1) ou en utilisant les profiles AutoMapper (2)


            //Methode (1)
            ////EquipementVM equipementVM;
            //switch (equipement.Type)
            //{
            //    case TypeEquipmentEnumeration.Constant:
            //        equipementVM = _mapper.Map<EquipementConstantVM>(equipement);
            //        break;
            //    case TypeEquipmentEnumeration.Lineaire:
            //        equipementVM = _mapper.Map<EquipementLineaireVM>(equipement);
            //        break;
            //    case TypeEquipmentEnumeration.Relatif:
            //        equipementVM = _mapper.Map<EquipementRelatifVM>(equipement);
            //        break;
            //    default:
            //        return NotFound();
            //}

            //Methode (2)
            //Mapp Domain entité vers VM
            var equipementVM = _mapper.Map(equipement, equipement.GetType(), typeof(EquipementVM)) as EquipementVM;



            return View("Upsert", equipementVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(EquipementVM vm)
        {
            if (ModelState.IsValid)
            {


                //map VM to Domain Entité utilisant _mapper 
                var equipement = _mapper.Map(vm, vm.GetType(), typeof(Equipement)) as Equipement;
                //declenche une erreur de foreign key constraint sans sa, manque de temps pour trouver l'origine de lerreur,donc  cela suffit
                equipement.Groupe = null;


                var id = vm.Id == 0
                    ? await _equipementService.Add(equipement)
                    : await _equipementService.Update(equipement);

                return RedirectToAction(nameof(Details), new { id = id });
            }

            return View(vm);

        }


        public async Task<IActionResult> Details(int id)
        {

            //Fetch lentité et throw erreur s'il n'existe pas
            var equipement = await _equipementService.GetById(id);
            //map Domain entité vers VM pour la view
            var vm = _mapper.Map(equipement, equipement.GetType(), typeof(EquipementVM)) as EquipementVM;
            return View(vm);
        }
    }
}
