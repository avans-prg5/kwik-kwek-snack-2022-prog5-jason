﻿using KwikKwekSnack.Domain;
using KwikKwekSnack.Domain.Repositories;
using KwikKwekSnack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KwikKwekSnackWeb.Controllers
{
    public class DrinkController : Controller
    {
        readonly IDrinkRepo repo;
        readonly IExtraRepo extraRepo;
        public DrinkController(IDrinkRepo injectedDrinkRepository, IExtraRepo injectedExtraRepository)
        {
            repo = injectedDrinkRepository;
            extraRepo = injectedExtraRepository;
        }
        
        public ActionResult Index()
        {
            var model = repo.GetAllActive();
            return View(model);
        }
        
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var drink = repo.Get(id.Value);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        
        public ActionResult Create()
        {
            var viewModel = new DrinkViewModel();
            PopulateAllExtras(ref viewModel);
            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DrinkViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {                    
                    var newDrink = repo.Create(viewModel.Drink, viewModel.AvailableExtras);
                    return View("Details", newDrink);
                }
                return View(viewModel);
            }
            catch
            {
                return View(viewModel);
            }
        }      
        
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            try
            {
                var viewModel = CreateEditViewModel(id.Value);
                return View(viewModel);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        private DrinkViewModel CreateEditViewModel(int id)
        {
            var viewModel = new DrinkViewModel();
            var drink = repo.Get(id);
            viewModel.Drink = drink;
            viewModel.SetFormattedPrice(drink.MinimalPrice);
            PopulateAssignedExtras(ref viewModel);
            return viewModel;
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DrinkViewModel viewModel)
        {
            if (id != viewModel.Drink.Id)
            {
                return NotFound();
            }
           
            if (EditDrink(viewModel))
            {
                var updatedDrink = repo.Get(viewModel.Drink.Id);
                return RedirectToAction("Details", updatedDrink);
            }
            return View(viewModel);           
        }

        private bool EditDrink(DrinkViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repo.Update(viewModel.Drink, viewModel.AvailableExtras);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }           
        }
        
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            try
            {
                var model = repo.Get(id.Value);
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Drink model)
        {
            if (DeleteDrink(model))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home");
        }

        private bool DeleteDrink(Drink model)
        {
            try
            {
                if (repo.MakeInactive(model.Id))
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private void PopulateAssignedExtras(ref DrinkViewModel viewModel)
        {
            var allExtras = extraRepo.GetAllActive();
            var drinkExtraIds = viewModel.Drink.AvailableExtras.Select(d => d.ExtraId);
            viewModel.AssignedExtras = new List<AssignedExtra>();
            foreach(var extra in allExtras)
            {
                viewModel.AssignedExtras.Add(new AssignedExtra()
                {
                    ExtraId = extra.Id,
                    Name = extra.Name,
                    Price = extra.Price,
                    Assigned = drinkExtraIds.Contains(extra.Id)
                });                
            }
        }

        private void PopulateAllExtras(ref DrinkViewModel viewModel)
        {
            var allExtras = extraRepo.GetAllActive();
            viewModel.AssignedExtras = new List<AssignedExtra>();
            foreach (Extra extra in allExtras)
            {
                viewModel.AssignedExtras.Add(new AssignedExtra()
                {
                    ExtraId = extra.Id,
                    Name = extra.Name,
                    Price = extra.Price,
                    Assigned = false
                });
            }            
        }

    }
}
