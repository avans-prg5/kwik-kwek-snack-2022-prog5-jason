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
        IExtraRepo extraRepo;

        public OrderController(IOrderRepo injectedRepository, IDrinkRepo injectedDrinkRepository, ISnackRepo injectedSnackRepository, IExtraRepo injectedExtraRepository)
        {
           repo = injectedRepository;
           drinkRepo = injectedDrinkRepository;
           snackRepo = injectedSnackRepository;
           extraRepo = injectedExtraRepository;
        }
        
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
       
        public ActionResult FinalizeOrder()
        {
            return View();
        }
        
        public ActionResult Delete(int id)
        {
            return View();
        }
        
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
