using KwikKwekSnack.Domain;
using KwikKwekSnack.Domain.Repositories;
using KwikKwekSnack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KwikKwekSnackWeb.Controllers
{
    public class ExtraController : Controller
    {

        readonly IExtraRepo repo;

        public ExtraController(IExtraRepo injectedRepository)
        {
            repo = injectedRepository;
        }
        public ActionResult Index() => View(repo.GetAllActive());
        
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var extra = repo.Get(id.Value);
            if (extra == null)
            {
                return NotFound();
            }

            return View(extra);
        }
        
        public ActionResult Create() => View(new Extra());        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Extra model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newExtra = repo.Create(model);
                    return View("Details", newExtra);
                }
                return View(model);
            }
            catch
            {
                return View(model);
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
                var model = repo.Get(id.Value); ;                
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Extra model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var updatedExtra = repo.Update(model);
                    return View("Details", updatedExtra);
                }
                return View(model);
            }
            catch
            {
                return View(model);
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
        public ActionResult Delete(Extra model)
        {
            try
            {
                if (repo.MakeInactive(model.Id))
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
