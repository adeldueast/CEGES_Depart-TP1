using AutoMapper;
using CEGES.Models;
using CEGES_Models;
using CEGES_Models.Enums;
using CEGES_Services.Interfaces;
using CEGES_Services.ViewModels.EquipementVMs;
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
            var groupe = await _groupeService.GetById(id);

            return View();
        }

        public IActionResult Insert(int id, [FromQuery] string type)
        {
            EquipementVM equipementVM;
            switch (type)
            {
                case TypeEquipmentEnumeration.Constant:
                    equipementVM = new EquipementConstantVM();
                    break;
                case TypeEquipmentEnumeration.Lineaire:
                    equipementVM = new EquipementLineaireVM();
                    break;
                case TypeEquipmentEnumeration.Relatif:
                    equipementVM = new EquipementRelatifVM();
                    break;
                default:
                    return NotFound();
            }

            return View("Upsert", equipementVM);
        }




        public async Task<IActionResult> Update(int id)
        {
            var equipement = await _equipementService.GetById(id);
            EquipementVM equipementVM;
            switch (equipement.Type)
            {
                case TypeEquipmentEnumeration.Constant:
                    equipementVM = new EquipementConstantVM();
                    break;
                case TypeEquipmentEnumeration.Lineaire:
                    equipementVM = new EquipementLineaireVM();
                    break;
                case TypeEquipmentEnumeration.Relatif:
                    equipementVM = new EquipementRelatifVM();
                    break;
                default:
                    return NotFound();
            }


            return View("Upsert", equipementVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(EquipementVM vm)
        {
            if (ModelState.IsValid)
            {
                //map vm to domain object
                var  equipement = (Equipement)_mapper.Map(vm, vm.GetType(), typeof(EquipementVM));


                var id = vm.Id == 0
                    ? await _equipementService.Add(3, equipement)
                    : await _equipementService.Add(3, equipement);

                if (vm.Id == 0)
                {
                    //Ajouter l'équipement
                    //pass groupId and equipement to add
                }
                else
                {
                    // Mettre à jour l'équipement
                }
                return RedirectToAction(nameof(Details), new { id = vm.Id });
            }
            return View(vm);

        }


        public async Task<IActionResult> Details(int id)
        {
            await Task.CompletedTask;
            return View();
        }
    }
}
