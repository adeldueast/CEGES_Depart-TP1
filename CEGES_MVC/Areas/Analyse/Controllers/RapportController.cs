
using CEGES.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CEGES_MVC.Areas.Analyse.Controllers
{
    [Area("Analyse")]
    public class RapportController : Controller
    {

        public RapportController()
        {
        }

        public IActionResult IndexAsync()
        {
            return View();
        }

        public async Task<IActionResult> Liste(int id)
        {
            await Task.CompletedTask;
            return View();
        }

        public async Task<IActionResult> Details(int entrepriseId, int RapportId)
        {
            await Task.CompletedTask;
            return View();
        }

        public async Task<IActionResult> Insert(int entrepriseId, DateTime RapportDebut)
        {
            await Task.CompletedTask;
            VM_Vide vm = new VM_Vide();
            return View("Upsert", vm);
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
                return View(nameof(IndexAsync));
            }
        }
    }
}
