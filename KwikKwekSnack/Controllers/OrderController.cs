using KwikKwekSnack.Domain.Repositories;
using KwikKwekSnack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KwikKwekSnackWeb.Controllers
{
    public class OrderController : Controller
    {
        IOrderRepo repo;
        IDrinkRepo drinkRepo;
        ISnackRepo snackRepo;        
        //IExtraRepo extraRepo;
        public OrderController(IOrderRepo injectedRepository, IDrinkRepo injectedDrinkRepository, ISnackRepo injectedSnackRepository)
        {
           repo = injectedRepository;
           drinkRepo = injectedDrinkRepository;
           snackRepo = injectedSnackRepository;
        }
        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View(new OrderViewModel());
        }

        // POST: OrderController/Create
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
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateSnackOrder()
        {
            var viewModel = new OrderViewModel();
            viewModel.AllSnacks = snackRepo.GetAll();            
            return View(viewModel);            
        }

        [HttpPost]
        public ActionResult CreateSnackOrder(OrderViewModel viewModel)
        {



            return View();
        }
        [HttpGet]
        public ActionResult CreateDrinkOrder()
        {
            var model = drinkRepo.GetAll();
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateDrinkOrder(OrderViewModel viewModel)
        {
            return RedirectToAction("CreateSnackOrder");
        }
        public ActionResult FinalizeOrder()
        {
            return View();
        }


        public ActionResult Details(int id)
        {
            return View();
        }
       

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
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

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
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
