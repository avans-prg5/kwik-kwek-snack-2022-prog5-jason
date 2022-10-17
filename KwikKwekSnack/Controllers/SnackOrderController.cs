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

        private static int? currentOrderId;
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
        public ActionResult Create(int? orderId)
        {    
            Order order = new Order();
            OrderViewModel viewModel = new OrderViewModel();
            if (orderId.HasValue)
            {
                currentOrderId = orderId.Value;
                order = orderRepo.Get(orderId.Value);                
                viewModel.Order = order;
                PopulateOrderList(ref viewModel);
                PopulateSnackList(ref viewModel);                
                return View(viewModel);
            }
            else if(currentOrderId.HasValue)
            {                
                order = orderRepo.Get(currentOrderId.Value);                
                viewModel.Order = order;
                PopulateOrderList(ref viewModel);
                PopulateSnackList(ref viewModel);
                return View(viewModel);
            }
            CleanUpUnusedOrders();
            order.Status = OrderStatusType.NotCreated;
            order = orderRepo.Create(order);            
            viewModel.Order = order;
            viewModel.SnackOrders = new List<PartialSnackOrder>();
            viewModel.DrinkOrders = new List<PartialDrinkOrder>();
            currentOrderId = order.Id;
            PopulateSnackList(ref viewModel);            
            return View(viewModel);
        }
    

        // POST: SnackOrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            //Next button
            return RedirectToAction("DrinkOrderController", "Create", currentOrderId);
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
            if(!snackId.HasValue)
            {
                return NotFound();
            }
            if(!currentOrderId.HasValue)
            {
                return RedirectToAction("Create");
            }            
            var snack = snackRepo.Get(snackId.Value);                   
            OrderViewModel viewModel = new OrderViewModel();
            viewModel.Order = orderRepo.Get(currentOrderId.Value);
            viewModel.CurrentSnackOrder = new PartialSnackOrder { Snack = snack};
            PopulateAvailableExtras(ref viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddSnack(int SnackOrderId, OrderViewModel viewModel)
        {
            if(!currentOrderId.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var snackOrder = new SnackOrder { Snack = viewModel.CurrentSnackOrder.Snack };
                if(viewModel.CurrentSnackOrder.ChosenExtraIds == null)
                {
                    viewModel.CurrentSnackOrder.ChosenExtraIds = new List<int>();
                }
                snackOrder = snackOrderRepo.Create(snackOrder, viewModel.CurrentSnackOrder.ChosenExtraIds); //TODO: werkt niet!
                viewModel.SnackOrders.Add(viewModel.CurrentSnackOrder);
                orderRepo.AddSnackOrder(snackOrder, currentOrderId.Value); 
            }
            catch
            {

            }                  


            return RedirectToAction("Create",viewModel);
        }

        private void PopulateSnackList(ref OrderViewModel viewModel)
        {
            var allSnacks = snackRepo.GetAll();
            viewModel.AllSnacks = new List<Snack>();
            //var allExtras = extraRepo.GetAll();
            foreach(Snack snack in allSnacks)
            {                
                viewModel.AllSnacks.Add(snack);                                
            }            
        }

        private void PopulateOrderList(ref OrderViewModel viewModel)
        {
            PopulateSnackOrderList(ref viewModel);
            PopulateDrinkOrderList(ref viewModel);                   
        }

        private void PopulateSnackOrderList(ref OrderViewModel viewModel)
        {
            var allSnackOrders = orderRepo.GetSnackOrders(currentOrderId.Value);
            viewModel.SnackOrders = new List<PartialSnackOrder>();

            foreach (SnackOrder snackOrder in allSnackOrders)
            {
                var snackOrderChosenExtras = new List<Extra>();
                double extrasCost = 0;
                if (snackOrder.ChosenExtras == null)
                {
                    snackOrder.ChosenExtras = new List<SnackOrderExtra>();
                }
                foreach (var extra in snackOrder.ChosenExtras)
                {
                    snackOrderChosenExtras.Add(new Extra()
                    {
                        Id = extra.ExtraId,
                        Name = extra.Extra.Name,
                        Price = extra.Extra.Price,
                    });
                    extrasCost += extra.Extra.Price;
                }

                viewModel.SnackOrders.Add(new PartialSnackOrder()
                {
                    Snack = snackOrder.Snack,
                    ChosenExtras = snackOrderChosenExtras,
                    OrderCost = snackOrder.Snack.StandardPrice + extrasCost
                });
            }
        }
        private void PopulateDrinkOrderList(ref OrderViewModel viewModel)
        {
            var allDrinkOrders = orderRepo.GetDrinkOrders(currentOrderId.Value);
            viewModel.DrinkOrders = new List<PartialDrinkOrder>();
        }

        private void PopulateAvailableExtras(ref OrderViewModel viewModel)
        {
            var allExtrasOfSnack = snackRepo.GetExtras(viewModel.CurrentSnackOrder.Snack.Id);
            viewModel.CurrentSnackOrder.AvailableExtras = new List<AssignedExtra>();

            foreach (Extra extra in allExtrasOfSnack)
            {                
                viewModel.CurrentSnackOrder.AvailableExtras.Add(new AssignedExtra()
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
