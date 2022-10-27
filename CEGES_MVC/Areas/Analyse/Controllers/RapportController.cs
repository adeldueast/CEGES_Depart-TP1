
using CEGES_DataAccess.Repository.IRepository;
using CEGES_MVC.Interfaces;
using CEGES_MVC.Models;
using CEGES_Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CEGES_Models;

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
            //var rapports = await _uow.Rapports.GetAllAsync();//r => r.EntrepriseId == entreprise.Id


            IEnumerable<Rapport> rapports = new List<Rapport>
            {
                new Rapport
                {
                    Id = 1,
                    EntrepriseId = 1,
                    DateDebut = new DateTime(2020,11,1)
                },

                 new Rapport
                {
                    Id = 2,
                    EntrepriseId = 1,
                    DateDebut = new DateTime(2022,10,1)
                },
            };

            var rapports_queue = new Queue<Rapport>(rapports);

            var startDate = new DateTime(2020, 11, 1);
            var endDate = new DateTime(2022, 10, 1);
            var datesGroupedByYear = GenerateMonthsBetween(startDate, endDate, rapports_queue);


            return View(datesGroupedByYear);
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

        public IEnumerable<IGrouping<string, (DateTime, int?)>> GenerateMonthsBetween(DateTime from, DateTime end, Queue<Rapport> rapports)
        {

            ICollection<(DateTime, int?)> dates = new List<(DateTime, int?)>();



            while (from <= end)
            {

                rapports.TryPeek(out var rapport);

                if (rapport != null && rapport.DateDebut.Year == from.Year && rapport.DateDebut.Month == from.Month)
                {
                    dates.Add((from, rapport.Id));
                    rapports.Dequeue();
                }
                else
                {
                    dates.Add((from, null));
                }


                // pull out month and year
                from = from.AddMonths(1);
            };

            return dates.ToLookup(date => date.Item1.Year.ToString());
        }
    }
}
