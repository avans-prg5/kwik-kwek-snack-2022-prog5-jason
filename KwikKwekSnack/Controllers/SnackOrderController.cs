using KwikKwekSnack.Domain;
using KwikKwekSnack.Domain.Repositories;
using KwikKwekSnack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace KwikKwekSnackWeb.Controllers
{
    public class SnackOrderController : Controller
    {
        readonly ISnackRepo snackRepo;
        readonly IOrderRepo orderRepo;
        readonly ISnackOrderRepo snackOrderRepo;
        readonly IExtraRepo extraRepo;

        private static OrderViewModel orderViewModel;

        
        public SnackOrderController(ISnackRepo injectedSnackRepository, IOrderRepo injectedOrderRepository, IExtraRepo injectedExtraRepository, ISnackOrderRepo injectedSnackOrderRepository)
        {
            snackRepo = injectedSnackRepository;
            orderRepo = injectedOrderRepository;
            extraRepo = injectedExtraRepository;
            snackOrderRepo = injectedSnackOrderRepository;
        }


        // GET: SnackOrderController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SnackOrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SnackOrderController/Create
        public ActionResult Create()
        {
            OrderViewModel viewModel = new OrderViewModel();
            if (orderViewModel == null)
            {
                viewModel = CreateNewOrderViewModel();
                orderViewModel = viewModel;
            }
            else
            {
                viewModel = orderViewModel;
            }                    
            
            PopulateSnackList(ref viewModel);            
            return View(viewModel);
        }
           
        private OrderViewModel CreateNewOrderViewModel()
        {
            OrderViewModel newViewModel = new OrderViewModel();
            Order order = new Order();
            order.Status = OrderStatusType.NotCreated;
            newViewModel.SnackOrders = new List<PartialSnackOrder>();
            newViewModel.DrinkOrders = new List<PartialDrinkOrder>();
            newViewModel.Order = order;
            return newViewModel;
        }

        // POST: SnackOrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel model)
        {
            //Next button, go to drinks
            return RedirectToAction("DrinkOrderController", "Create", orderViewModel);
        }       

        // GET: SnackOrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SnackOrderController/Delete/5
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
        [HttpGet]
        public ActionResult AddSnack(int? snackId)
        {
            if (orderViewModel == null)
            {
                return RedirectToAction("Create");
            }
            if (!snackId.HasValue)
            {
                return NotFound();
            }                        
            var snack = snackRepo.Get(snackId.Value);
            var snackOrder = new PartialSnackOrder { Snack = snack };            
            PopulateAvailableExtras(ref snackOrder);
            return View(snackOrder);
        }

        [HttpPost]
        public ActionResult AddSnack(PartialSnackOrder viewModel)
        {
            if(orderViewModel == null)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var snackOrder = new SnackOrder { Snack = viewModel.Snack };
                if(viewModel.ChosenExtraIds == null)
                {
                    viewModel.ChosenExtraIds = new List<int>();
                    viewModel.ChosenExtras = new List<Extra>();
                }
                else
                {
                    SetExtrasFromIds(viewModel, viewModel.ChosenExtraIds);
                }
                viewModel.OrderCost = CalculateSnackOrderPrice(viewModel);
                orderViewModel.SnackOrders.Add(viewModel);                
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
            
            return RedirectToAction("Create");
        }

        private double CalculateSnackOrderPrice(PartialSnackOrder snackOrder)
        {
            double price = 0;
            price += snackOrder.Snack.StandardPrice;
            if(snackOrder.ChosenExtras!= null)
            {
                foreach(var extra in snackOrder.ChosenExtras)
                {
                    try
                    {
                        price += extra.Price;
                    }
                    catch
                    {
                        continue;
                    }                    
                }
            }
            return price;
        }
        private void SetExtrasFromIds(PartialSnackOrder snackOrder, List<int> extraIds)
        {
            snackOrder.ChosenExtras = new List<Extra>();
            foreach(var extraId in extraIds)
            {
                try
                {
                    Extra extra = extraRepo.Get(extraId);
                    snackOrder.ChosenExtras.Add(extra);
                }
                catch
                {
                    continue;
                }
            }

        }

        private void PopulateSnackList(ref OrderViewModel viewModel)
        {
            var allSnacks = snackRepo.GetAll();
            viewModel.AllSnacks = new List<Snack>();            
            foreach(Snack snack in allSnacks)
            {                
                viewModel.AllSnacks.Add(snack);                                
            }            
        }       

        private void PopulateAvailableExtras(ref PartialSnackOrder viewModel)
        {
            var allExtrasOfSnack = snackRepo.GetExtras(viewModel.Snack.Id);
            viewModel.AvailableExtras = new List<AssignedExtra>();

            foreach (Extra extra in allExtrasOfSnack)
            {                
                viewModel.AvailableExtras.Add(new AssignedExtra()
                {
                    ExtraId = extra.Id,
                    Name = extra.Name,
                    Price = extra.Price,
                    Assigned = false
                });
            }
        }

        private void CleanUpUnusedOrders()
        {
            orderRepo.CleanUpUnused();
        }
    }
}
