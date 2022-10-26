
using CEGES_DataAccess.Repository.IRepository;
using CEGES_MVC.Interfaces;
using CEGES_MVC.Models;
using CEGES_Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace CEGES_MVC.Areas.Analyse.Controllers
{
    [Area("Analyse")]
    public class RapportController : Controller
    {
        private readonly IRapportService _rapportService;
        private readonly IEntrepriseService _entrepriseService;

        private readonly IUnitOfWork _uow;

        
        public RapportController(IRapportService rapportService, IUnitOfWork uow, IEntrepriseService entrepriseService)
        {
            _uow = uow;
            _rapportService = rapportService;
            _entrepriseService = entrepriseService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Liste(int id)
        {
            //check if entreprise existe
            var entreprise = await _entrepriseService.GetById(id);


            //get tout les rapports de l'entreprise...
            var rapports = await _uow.Rapports.GetAllAsync();//r => r.EntrepriseId == entreprise.Id

            var vm = rapports.Select(r => new
            {
                Id = r.Id,
                DateDebut = r.DateDebut
                //EntrepriseId = entreprise.Id
            });;

            var startDate = new DateTime(2020, 11, 1);
            var endDate = new DateTime(2022, 10, 1);
            var months = GenerateMonthsBetween(startDate, endDate);

            foreach (var rapport in vm)
            {
                months[rapport.DateDebut.Date] = rapport.Id;
            }


            var a = months.GroupBy(m => m.Key.Date.Year);

            return View(months.GroupBy(m => m.Key.Date.Year));
        }

        public async Task<IActionResult> Insert(int entrepriseId, DateTime RapportDebut)
        {
            await Task.CompletedTask;
            VM_Vide vm = new VM_Vide();
            return View("Upsert", vm);
        }

        public async Task<IActionResult> Details(int entrepriseId, int RapportId)
        {
            //fetch entreprise
            //
            await Task.CompletedTask;
            return View();
        }

        public async Task<IActionResult> Update(int entrepriseId, int RapportId)
        {
            await Task.CompletedTask;
            VM_Vide vm = new VM_Vide();
            return View("Upsert", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(VM_Vide vm)
        {
            await Task.CompletedTask;
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Details));
            }
            if (vm.Id > 0)
            {
                return View(nameof(Liste), new { id = vm.Id });
            }
            else
            {
                return View(nameof(Index));
            }
        }

        public IDictionary<DateTime, int?> GenerateMonthsBetween(DateTime from, DateTime end)
        {
            IDictionary<DateTime, int?> dates = new Dictionary<DateTime, int?>() { { from, null } };

            while (from <= end)
            {
                // pull out month and year
                from = from.AddMonths(1);

                dates.Add(from, null);
            };

            return dates;
        }
    }
}
