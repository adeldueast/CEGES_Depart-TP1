
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
            var entrepriseRapportsCount = await _uow.Entreprises.GetAllWithRapportsCount();

            var vm = _mapper.Map<IEnumerable<EntrepriseRapportsCount>, IEnumerable<EntrepriseRapportsCountVM>>(entrepriseRapportsCount);

            return View(vm);
        }

        public async Task<IActionResult> Liste(int id)
        {


            //get tout les rapports de l'entreprise...
            var entrepriseRapports = await _uow.Entreprises.GetByIdWithRapports(id);
            var startDate = new DateTime(2020, 11, 1);
            var endDate = new DateTime(2022, 10, 1);

            //liste de dates 
            var datesGroupedByYear = GenerateMonthsBetween(startDate, endDate, entrepriseRapports.Rapports);

            ViewData["entrepriseId"] = entrepriseRapports.Entreprise.Id;
            return View(datesGroupedByYear);
        }

        public async Task<IActionResult> Insert(int entrepriseId, DateTime RapportDebut)
        {

            //ici le rapport nexiste pas, on fetch simplement l'entrepris en son entier (entreprise>groupes>equipements)
            var entreprise = await _entrepriseService.GetByIdWithGroupesWithEquipements(entrepriseId);


            //liste d'equipements de l'entreprise groupé par groupe
            var groupedEquipements = entreprise.Groupes.SelectMany(g => g.Equipements)
                .Select(e => new EquipementAvecMesureVM
                {
                    GroupeNom = e.Groupe.Nom,
                    EquipementVM = _mapper.Map(e, e.GetType(), typeof(EquipementVM)) as EquipementVM,
                    Mesure = 0
                })
                .GroupBy(e => e.GroupeNom).ToDictionary(g => g.Key, g => g.ToList());


            //ici c'est moin compliquer de juste creer la vm manuellement, aulieu d'utiliser le mapper..
            var VM = new RapportDetailsVM
            {
                DateDebut = RapportDebut,
                EntrepriseNom = entreprise.Nom,
                EntrepriseId = entrepriseId,
                GroupedEquipements = groupedEquipements,
            };


            ViewData["DateDebut"] = RapportDebut;
            return View("Upsert", VM);
        }

        public async Task<IActionResult> Details(int entrepriseId, int RapportId)
        {

            //TODO: Mauvais query, rien nous garantis que le rapportId appartient au given entrepriseId
            //A modifier dans le future!!

            //fetch entreprises et throw erreur si nexiste pas
            var entreprise = await _entrepriseService.GetById(entrepriseId);

            //fetch les rapports
            var rapport = await _uow.Rapports.GetByIdWithEquipementsWithGroupes(RapportId);
            if (rapport == null) throw new NotFoundException(nameof(Rapport), RapportId);


            var groupedEquipements = rapport.Equipements
                .Select(er => new EquipementAvecMesureVM
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
                GroupedEquipements = groupedEquipements,
            };


            ViewData["DateDebut"] = rapport.DateDebut;
            return View(VM);
        }

        public async Task<IActionResult> Update(int entrepriseId, int RapportId)
        {
            //TODO: Mauvais query, rien nous garantis que le rapportId appartient au given entrepriseId
            //A modifier dans le future!!

            //fetch entreprises et throw erreur si nexiste pas
            var entreprise = await _entrepriseService.GetById(entrepriseId);

            //fetch les rapports
            var rapport = await _uow.Rapports.GetByIdWithEquipementsWithGroupes(RapportId);
            if (rapport == null) throw new NotFoundException(nameof(Rapport), RapportId);

            var groupedEquipements = rapport.Equipements
               .Select(er => new EquipementAvecMesureVM
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
                GroupedEquipements = groupedEquipements,
            };

            ViewData["DateDebut"] = rapport.DateDebut;
            return View("Upsert", VM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(RapportDetailsVM VM)
        {
            if (VM.GroupedEquipements is null || VM.GroupedEquipements.Count! < 0)
                throw new Exception("Veuillez creer des equipements avant de creer un rapport");

            if (ModelState.IsValid)
            {
                //flatten le dictionnaire et map la liste d'equipmentsGroupedAvecMesureVM en 
                //EquipementRapport domain object pour etre ajouter a la database
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
                    //On ajoute un rapport
                    _context.Rapports.Add(rapport);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Details), new { entrepriseId = VM.EntrepriseId, RapportId = rapport.Id });

                }
                else
                {
                    //On update un rapport
                    var rapportDomain = await _context.Rapports.Include(r => r.Equipements).SingleAsync(r => r.Id == VM.Id);
                    rapportDomain.Equipements = rapport.Equipements;

                    _context.Rapports.Update(rapportDomain);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Details), new { entrepriseId = VM.EntrepriseId, RapportId = rapportDomain.Id });
                }
            }

            //retourn la view si invalide model state
            return View(VM);


        }

        public ILookup<string, (DateTime, int?)> GenerateMonthsBetween(DateTime from, DateTime end, IEnumerable<Rapport> rapports)
        {

            //creer une collection de tuples (date,int?)
            //date : date generee
            //int? : id du rapport existant pour cette date
            ICollection<(DateTime, int?)> dates = new List<(DateTime, int?)>();

            //Creer une queue pour faciliter le processus, lorsque un rapport a ete assigné à une date, on la retire de la queue 
            //et traite les dates restantes 
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
