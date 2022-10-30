﻿using KwikKwekSnack.Domain;
using KwikKwekSnack.Domain.Repositories;
using KwikKwekSnack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KwikKwekSnackWeb.Controllers
{
    public class SnackController : Controller
    {
        readonly ISnackRepo repo;
        readonly IExtraRepo extraRepo;        
        
        public SnackController(ISnackRepo injectedSnackRepository, IExtraRepo injectedExtraRepo)
        {
            repo = injectedSnackRepository;
            extraRepo = injectedExtraRepo;
        }

        public ActionResult Index()
        {
            var model = repo.GetAllActive();
            return View(model);            
        }        

        public ActionResult Details(int? id)
        {
           if(!id.HasValue)
           {
                return NotFound();
           }

           var snack = repo.Get(id.Value);
           if(snack == null)
           {
                return NotFound();
           }

            return View(snack);
        }        

        public ActionResult Create()
        {
            var viewModel = new SnackViewModel();            
            PopulateAllExtras(ref viewModel);
            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SnackViewModel viewModel)
        {
            try
            {
                viewModel.AssignedExtras = new List<AssignedExtra>();
                if (ModelState.IsValid)
                {
                    viewModel.Snack.StandardPrice = Math.Round(viewModel.Snack.StandardPrice, 2);
                    var newSnack = repo.Create(viewModel.Snack, viewModel.AvailableExtras);
                    return RedirectToAction("Details", newSnack);
                }                
            }
            catch
            {
                return View(viewModel);
            }
            return View(viewModel);
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

        private SnackViewModel CreateEditViewModel(int id)
        {
            var viewModel = new SnackViewModel();
            var snack = repo.Get(id);
            viewModel.Snack = snack;
            viewModel.SetFormattedPrice(snack.StandardPrice);
            PopulateAssignedExtras(ref viewModel);
            return viewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SnackViewModel viewModel)
        {
            if(id != viewModel.Snack.Id)
            {
                return NotFound();
            }

            if (EditSnack(viewModel))
            {
                var updatedSnack = repo.Get(viewModel.Snack.Id);
                return RedirectToAction("Details", updatedSnack);
            }
            return View(viewModel);
        }

        private bool EditSnack(SnackViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    viewModel.Snack.StandardPrice = Math.Round(viewModel.Snack.StandardPrice, 2);
                    repo.Update(viewModel.Snack, viewModel.AvailableExtras);
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
            if(!id.HasValue)
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
        public ActionResult Delete(Snack model)
        {
            if (DeleteSnack(model))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home");
        }
        private bool DeleteSnack(Snack model)
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
        private void PopulateAssignedExtras(ref SnackViewModel viewModel)
        {
            var allExtras = extraRepo.GetAllActive();
            var snackExtraIds = viewModel.Snack.AvailableExtras.Select(d => d.ExtraId);
            viewModel.AssignedExtras = new List<AssignedExtra>();
            foreach (var extra in allExtras)
            {
                viewModel.AssignedExtras.Add(new AssignedExtra()
                {
                    ExtraId = extra.Id,
                    Name = extra.Name,
                    Price = extra.Price,
                    Assigned = snackExtraIds.Contains(extra.Id)
                });
            }
        }
        private void PopulateAllExtras(ref SnackViewModel viewModel)
        {
            var allExtras = extraRepo.GetAllActive();
            viewModel.AssignedExtras = new List<AssignedExtra>();
            viewModel.AvailableExtras = new List<int>();
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
