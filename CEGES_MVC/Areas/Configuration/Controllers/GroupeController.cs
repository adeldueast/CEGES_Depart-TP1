using AutoMapper;
using CEGES.Models;
using CEGES_Services.Interfaces;
using CEGES_Services.ViewModels.GroupeVMs;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CEGES_MVC.Areas.Configuration.Controllers
{
    [Area("Configuration")]
    public class GroupeController : Controller
    {
        private readonly IGroupeService _groupeService;

        private readonly IEntrepriseService _entrepriseService;

        private readonly IMapper _mapper;

        public GroupeController(IGroupeService groupeService, IEntrepriseService entrepriseService, IMapper mapper)
        {
            _groupeService = groupeService;
            _entrepriseService = entrepriseService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Details(int id)
        {
            var groupe = await _groupeService.GetById(id);

            var dest = _mapper.Map<GroupeDetailsVM>(groupe);

            return View(dest);
        }

        public async Task<IActionResult> Insert(int id)
        {
            //il faut valider que lentreprise existe pour lui assigner un nouveau groupe
            var entreprise = await _entrepriseService.GetById(id);

            return View("Upsert", new GroupeUpsertVM() { EntrepriseId = entreprise.Id, EntrepriseNom = entreprise.Nom });

        }

        public async Task<IActionResult> Update(int groupeId)
        {
            //il faut valider que le groupe existe avant de le modifier
            var groupe = await _groupeService.GetById(groupeId);

            return View("Upsert", new GroupeUpsertVM() { Id = groupeId, EntrepriseId = groupe.Entreprise.Id, EntrepriseNom = groupe.Entreprise.Nom });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(GroupeUpsertVM vm)
        {

            if (ModelState.IsValid)
            {

                var id = vm.Id == 0
                    ? await _groupeService.Add(vm.EntrepriseId, vm.Nom)
                    : await _groupeService.Update(vm.Id, vm.Nom);

                return RedirectToAction(nameof(Details), new { id = id });
            }
            return View(vm);
        }
    }
}
