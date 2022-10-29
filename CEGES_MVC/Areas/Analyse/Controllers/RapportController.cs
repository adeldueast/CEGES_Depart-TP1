
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
using AutoMapper;
using CEGES_DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using CEGES_Models.Exceptions;
using CEGES_MVC.ViewModels.RapportVMs;
using CEGES_MVC.ViewModels.EquipementVMs;

namespace CEGES_MVC.Areas.Analyse.Controllers
{
    [Area("Analyse")]
    public class RapportController : Controller
    {
        private readonly IRapportService _rapportService;
        private readonly IEntrepriseService _entrepriseService;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public RapportController(IRapportService rapportService, IUnitOfWork uow, IEntrepriseService entrepriseService, IMapper mapper, ApplicationDbContext context)
        {
            _uow = uow;
            _rapportService = rapportService;
            _entrepriseService = entrepriseService;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var entrepriseRapportsCountVM = await _uow.Entreprises.GetAllWithPeriodsCount();
            return View(entrepriseRapportsCountVM);
        }

        public async Task<IActionResult> Liste(int id)
        {


            //get tout les rapports de l'entreprise...
            var entrepriseRapports = await _uow.Entreprises.GetByIdWithPeriods(id);

            var fakeEntrepriseRapports = new EntrepriseRapports
            {
                Entreprise = new() { Id = 1 },
                Rapports = new List<Rapport>()
                {
                    new Rapport
                    {
                        Id = 4,
                        DateDebut = new DateTime(2020,11,1)
                    }, new Rapport
                    {
                        Id = 2,
                        DateDebut = new DateTime(2022,10,1)
                    }
                }
            };

            var startDate = new DateTime(2020, 11, 1);
            var endDate = new DateTime(2022, 10, 1);

            //liste de date

            var datesGroupedByYear = GenerateMonthsBetween(startDate, endDate, entrepriseRapports.Rapports);

            ViewData["entrepriseId"] = entrepriseRapports.Entreprise.Id;

            return View(datesGroupedByYear);
        }

        public async Task<IActionResult> Insert(int entrepriseId, DateTime RapportDebut)
        {

            //ici le rapport nexiste pas, on fetch simplement l'entrepris en son entier (entreprise>groupes>equipements)
            //Fetch all groups inluding their
            var entreprise = await _entrepriseService.GetByIdWithGroupesWithEquipements(entrepriseId);

            if (entreprise is null) throw new NotFoundException(nameof(Entreprise), entrepriseId);


            var groupedEquipements2 = entreprise.Groupes.SelectMany(g => g.Equipements)
                .Select(e => new EquipementAvecMesure
                {
                    GroupeNom = e.Groupe.Nom,
                    EquipementVM = _mapper.Map(e, e.GetType(), typeof(EquipementVM)) as EquipementVM,
                    Mesure = 0
                })
                .GroupBy(e => e.GroupeNom).ToDictionary(g => g.Key, g => g.ToList());

            var VM = new RapportDetailsVM
            {
                DateDebut = RapportDebut,
                EntrepriseNom = entreprise.Nom,
                EntrepriseId = entrepriseId,
                GroupedEquipements = groupedEquipements2,
            };


            ViewData["DateDebut"] = RapportDebut;
            return View("Upsert", VM);
        }

        public async Task<IActionResult> Details(int entrepriseId, int RapportId)
        {

            var entreprise = await _entrepriseService.GetById(entrepriseId);


            var rapport = await _context.Rapports
                .Include(r => r.Equipements)
                .ThenInclude(e => e.Equipement)
                .ThenInclude(e => e.Groupe)
                .SingleOrDefaultAsync(r => r.Id == RapportId);

            if (rapport == null) throw new NotFoundException(nameof(Rapport), RapportId);

            //var vm = _mapper.Map(equipement, equipement.GetType(), typeof(EquipementVM)) as EquipementVM;
            //var groupedEquipements = rapport.Equipements
            //    .Select(e => _mapper.Map(e, e.GetType(), typeof(EquipementVM)) as EquipementVM)
            //    .GroupBy(e => e.GroupeNom);

            var groupedEquipements2 = rapport.Equipements
                .Select(er => new EquipementAvecMesure
                {
                    GroupeNom = er.Equipement.Groupe.Nom,
                    EquipementVM = _mapper.Map(er.Equipement, er.Equipement.GetType(), typeof(EquipementVM)) as EquipementVM,
                    Mesure = er.Mesure
                })
               .GroupBy(e => e.GroupeNom).ToDictionary(g => g.Key, g => g.ToList());

            var VM = new RapportDetailsVM
            {
                Id = RapportId,
                DateDebut = rapport.DateDebut,
                EntrepriseNom = entreprise.Nom,
                EntrepriseId = entrepriseId,
                GroupedEquipements = groupedEquipements2,
            };

            //map domain to vm
            //var vm = _mapper.Map<EntrepriseInsertPeriod>(entrepriseGroupesAndEquipementsAndRapports);
            ViewData["DateDebut"] = rapport.DateDebut;


            return View(VM);
        }

        public async Task<IActionResult> Update(int entrepriseId, int RapportId)
        {
            var entreprise = await _entrepriseService.GetById(entrepriseId);


            var rapport = await _context.Rapports
                .Include(r => r.Equipements)
                .ThenInclude(e => e.Equipement)
                .ThenInclude(e => e.Groupe)
                .SingleOrDefaultAsync(r => r.Id == RapportId);

            if (rapport == null) throw new NotFoundException(nameof(Rapport), RapportId);

            var groupedEquipements2 = rapport.Equipements
               .Select(er => new EquipementAvecMesure
               {
                   GroupeNom = er.Equipement.Groupe.Nom,
                   EquipementVM = _mapper.Map(er.Equipement, er.Equipement.GetType(), typeof(EquipementVM)) as EquipementVM,
                   Mesure = er.Mesure
               })
               .GroupBy(e => e.GroupeNom).ToDictionary(g => g.Key, g => g.ToList());

            var VM = new RapportDetailsVM
            {
                Id = RapportId,
                DateDebut = rapport.DateDebut,
                EntrepriseNom = entreprise.Nom,
                EntrepriseId = entrepriseId,
                GroupedEquipements = groupedEquipements2,
            };

            ViewData["DateDebut"] = rapport.DateDebut;

            return View("Upsert", VM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(RapportDetailsVM VM)
        {


            if (ModelState.IsValid)
            {
                var equipments = VM.GroupedEquipements
              .SelectMany(ge => ge.Value)
              .Select(em => new EquipementRapport
              {
                  EquipementId = em.EquipementVM.Id,
                  Mesure = em.Mesure,

              });

                //create new rapport
                Rapport rapport = new Rapport()
                {
                    DateDebut = VM.DateDebut,
                    Equipements = equipments.ToList()
                };
                if (VM.Id == 0)
                {

                    _context.Rapports.Add(rapport);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { entrepriseId = VM.EntrepriseId, RapportId = rapport.Id });

                }
                else
                {
                    var rapportDomain = await _context.Rapports.Include(r => r.Equipements).SingleAsync(r=>r.Id == VM.Id);
                    rapportDomain.Equipements = rapport.Equipements;

                    _context.Rapports.Update(rapportDomain);
                    await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { entrepriseId = VM.EntrepriseId, RapportId = rapportDomain.Id });

                }
            }
            return View(VM);
            //if (VM.Id > 0)
            //{
            //    return View(nameof(Liste), new { id = VM.Id });
            //}
            //else
            //{
            //    return View(nameof(Index));
            //}
        }

        public ILookup<string, (DateTime, int?)> GenerateMonthsBetween(DateTime from, DateTime end, IEnumerable<Rapport> rapports)
        {

            ICollection<(DateTime, int?)> dates = new List<(DateTime, int?)>();


            Queue<Rapport> _rapports = new Queue<Rapport>(rapports);
            while (from <= end)
            {

                _rapports.TryPeek(out var rapport);

                if (rapport != null && rapport.DateDebut.Year == from.Year && rapport.DateDebut.Month == from.Month)
                {
                    dates.Add((from, rapport.Id));
                    _rapports.Dequeue();
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
