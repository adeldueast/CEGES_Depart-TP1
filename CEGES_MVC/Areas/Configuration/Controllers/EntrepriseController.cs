﻿using CEGES.Models;
using CEGES_Models;
using CEGES_Services.Interfaces;
using CEGES_Services.ViewModels;
using CEGES_Services.ViewModels.EntrepriseVM;
using CEGES_Services.ViewModels.GroupeVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CEGES_MVC.Areas.Configuration.Controllers
{
    [Area("Configuration")]
    public class EntrepriseController : Controller
    {

        private readonly IEntrepriseService _entrepriseService;

        public EntrepriseController(IEntrepriseService entrepriseService) => this._entrepriseService = entrepriseService;

        public async Task<IActionResult> Index()
        {
            var entreprises = await _entrepriseService.GetAllSummaries();

            //TODO: map dm to vm
            var vm = entreprises.Select(x => new EntrepriseSummaryVM
            {
                Id = x.Id,
                Nom = x.Nom,
                GroupesCount = x.Groupes.Count,
                EquipementsCount = x.Groupes.SelectMany(x => x.Equipements).Count()

            }).ToList();

            return View(vm);
        }

        public async Task<IActionResult> Details(int id)
        {

            var entreprise = await _entrepriseService.GetById(id);


            var vm = new EntrepriseDetailsVM()
            {
                Id = entreprise.Id,
                Nom = entreprise.Nom,
                Groupes = entreprise.Groupes.Select(g => new GroupeSummaryVM
                {
                    Id = g.Id,
                    Nom = g.Nom,
                    EquipementCount = g.Equipements.Count

                }).ToList()
            };



            return View(vm);
        }

        public async Task<IActionResult> Upsert(int? id)
        {

            if (id == null)
            {
                // Créer une nouvelle entreprise
                return View(new EntrepriseUpsertVM());
            }

            // Récupérer l'entreprise existante
            var entreprise = await _entrepriseService.GetById(id.Value);

            EntrepriseUpsertVM entrepriseVM = new()
            {
                Id = entreprise.Id,
                Nom = entreprise.Nom
            };

            return View(entrepriseVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(EntrepriseUpsertVM vm)
        {

            if (ModelState.IsValid)
            {

                var upsertedEntreprise = vm.Id == 0
                    ? await _entrepriseService.Add(vm.Nom)
                    : await _entrepriseService.Update(vm.Id, vm.Nom);

                return RedirectToAction(nameof(Details), new { id = upsertedEntreprise });
            }

            return View(vm);
        }
    }
}
