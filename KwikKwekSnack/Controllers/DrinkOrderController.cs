using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KwikKwekSnackWeb.Controllers
{
    public class DrinkOrderController : Controller
    {
        // GET: DrinkOrderController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DrinkOrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DrinkOrderController/Create
        public ActionResult Create()
        {
            return RedirectToAction("Index", "Home");
        }

        // POST: DrinkOrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DrinkOrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DrinkOrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DrinkOrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DrinkOrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
