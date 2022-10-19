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

        public EquipementController(IGroupeService groupeService, IEquipementService equipementService)
        {
            _groupeService = groupeService;
            _equipementService = equipementService;
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
                    equipementVM = new EquipementConstantVM() { Type = "Constant"};
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
                if (vm.Id == 0)
                {
                    //Ajouter l'équipement
                    Equipement equipement = new EquipementConstant();
                    //pass groupId and equipement to add
                    var id = await _equipementService.Add(3, equipement);
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
