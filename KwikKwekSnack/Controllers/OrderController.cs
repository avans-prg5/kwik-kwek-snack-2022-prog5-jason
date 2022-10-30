using KwikKwekSnack.Domain;
using KwikKwekSnack.Domain.Repositories;
using KwikKwekSnack.Models;
using KwikKwekSnackWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace KwikKwekSnackWeb.Controllers
{    
    public class OrderController : Controller
    {
        readonly IOrderRepo repo;
        readonly IDrinkRepo drinkRepo;
        readonly ISnackRepo snackRepo;        
        readonly IExtraRepo extraRepo;
        readonly IDrinkSizeRepo sizeRepo;
        readonly IOrderLogic orderLogic;

        static private OrderViewModel orderViewModel;

        public OrderController(IOrderRepo injectedRepository, IDrinkRepo injectedDrinkRepository, ISnackRepo injectedSnackRepository, IExtraRepo injectedExtraRepository, IDrinkSizeRepo injectedDrinkSizeRepo)
        {
           repo = injectedRepository;
           drinkRepo = injectedDrinkRepository;
           snackRepo = injectedSnackRepository;
           extraRepo = injectedExtraRepository;
           sizeRepo = injectedDrinkSizeRepo;
           orderLogic = new OrderLogic();
        }
        public ActionResult CreateSnackOrder()
        {            
            OrderViewModel viewModel = new OrderViewModel();
            if (orderViewModel == null)
            {
                viewModel = InitOrderViewModel();
            }
            else
            {
                viewModel = orderViewModel;
            }

            PopulateSnackList(ref viewModel);
            return View(viewModel);
        }       
        public ActionResult CreateDrinkOrder()
        {            
            if (orderViewModel == null)
            {
                return RedirectToAction("CreateSnackOrder");
            }            

            PopulateDrinkList(ref orderViewModel);
            return View(orderViewModel);
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
            PopulateAvailableSnackExtras(ref snackOrder);
            return View(snackOrder);
        }

        [HttpPost]
        public ActionResult AddSnack(PartialSnackOrder viewModel)
        {
            if (orderViewModel == null)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var snack = snackRepo.Get(viewModel.Snack.Id);
                viewModel.Snack = snack;
                SetChosenSnackExtras(viewModel);
                viewModel.SetFormattedPrice(orderLogic.CalculateSnackOrderPrice(viewModel));
                orderViewModel.SnackOrders.Add(viewModel);
                orderViewModel.SetFormattedPrice(CalculateTotalOrderPrice(orderViewModel));
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("CreateSnackOrder");
        }
        [HttpGet]
        public ActionResult AddDrink(int? drinkId)
        {
            if (orderViewModel == null)
            {
                return RedirectToAction("Create");
            }
            if (!drinkId.HasValue)
            {
                return NotFound();
            }

            var drink = drinkRepo.Get(drinkId.Value);
            var drinkOrder = new PartialDrinkOrder { Drink = drink };
            PopulateDrinkPrices(ref drinkOrder);
            PopulateAvailableDrinkExtras(ref drinkOrder);
            return View(drinkOrder);
        }
        [HttpPost]
        public ActionResult AddDrink(PartialDrinkOrder viewModel)
        {
            if (orderViewModel == null)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var drink = drinkRepo.Get(viewModel.Drink.Id);
                viewModel.Drink = drink;                
                SetChosenDrinkExtras(viewModel);
                double priceMultiplier = GetDrinkSizeMultiplier(viewModel);
                viewModel.SetFormattedPrice(orderLogic.CalculateDrinkOrderPrice(viewModel, priceMultiplier));
                orderViewModel.DrinkOrders.Add(viewModel);
                orderViewModel.SetFormattedPrice(CalculateTotalOrderPrice(orderViewModel));
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("CreateDrinkOrder");
        }
        public ActionResult RemoveSnackOrder(int snackOrderIndex)
        {
            TempData["OrderPageUrl"] = Request.Headers["Referer"].ToString();
            try
            {
                var snackOrderToRemove = orderViewModel.SnackOrders[snackOrderIndex];
                orderViewModel.SnackOrders.Remove(snackOrderToRemove);
                orderViewModel.SetFormattedPrice(CalculateTotalOrderPrice(orderViewModel));
            }
            catch
            {
                return Redirect(TempData["OrderPageUrl"].ToString());
            }
            return Redirect(TempData["OrderPageUrl"].ToString());
        }
        public ActionResult RemoveDrinkOrder(int drinkOrderIndex)
        {
            TempData["OrderPageUrl"] = Request.Headers["Referer"].ToString();
            try
            {
                var drinkOrderToRemove = orderViewModel.DrinkOrders[drinkOrderIndex];
                orderViewModel.DrinkOrders.Remove(drinkOrderToRemove);
                orderViewModel.SetFormattedPrice(CalculateTotalOrderPrice(orderViewModel));
            }
            catch
            {
                return Redirect(TempData["OrderPageUrl"].ToString());
            }
            return Redirect(TempData["OrderPageUrl"].ToString());
        }
        [HttpGet]
        public ActionResult Overview()
        {
            return View(orderViewModel);
        }
        [HttpPost]
        public ActionResult Overview(OrderViewModel viewModel)
        {
            if(orderViewModel == null)
            {
                return RedirectToAction("CreateSnackOrder");
            }
            bool emptyOrder = orderViewModel.SnackOrders.Count == 0 && orderViewModel.DrinkOrders.Count == 0;

            if (emptyOrder)
            {
                ModelState.AddModelError(string.Empty, "Kan geen lege order aanmaken.");
                return View(orderViewModel);
            }

            try
            {
                var order = PostOrderToDatabase();
                viewModel.Order = order;
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Fout bij het plaatsen van uw order.");
                return View(orderViewModel);
            }
            
            
            orderViewModel = CreateNewOrderViewModel();
            return View("Success", viewModel);
        }
        private Order PostOrderToDatabase()
        {
            var snackOrders = CreateSnackOrderListFromViewModel(orderViewModel);
            var drinkOrders = CreateDrinkOrderListFromViewModel(orderViewModel);
            var order = repo.Create(orderViewModel.Order, snackOrders, drinkOrders, orderViewModel.DeliveryType);
            return order;
        }
        private List<SnackOrder> CreateSnackOrderListFromViewModel(OrderViewModel viewModel)
        {
            List<SnackOrder> snackOrders = new List<SnackOrder>();
            if(viewModel.SnackOrders == null)
            {
                return snackOrders;
            }

            foreach(var snackOrderViewModel in viewModel.SnackOrders)
            {
                if(snackOrderViewModel.ChosenExtraIds == null)
                {
                    snackOrderViewModel.ChosenExtraIds = new List<int>();
                }
                SnackOrder snackOrder = new SnackOrder();
                snackOrder.Snack = snackOrderViewModel.Snack;
                snackOrder.ChosenExtras = new List<SnackOrderExtra>();
                foreach(var id in snackOrderViewModel.ChosenExtraIds)
                {
                    var extra = extraRepo.Get(id);
                    SnackOrderExtra snackOrderExtra = new SnackOrderExtra();
                    snackOrderExtra.SnackOrder = snackOrder;
                    snackOrderExtra.Extra = extra;        

                    snackOrder.ChosenExtras.Add(snackOrderExtra);
                }                
                snackOrders.Add(snackOrder);
            }
            return snackOrders;
        }
        private List<DrinkOrder> CreateDrinkOrderListFromViewModel(OrderViewModel viewModel)
        {
            List<DrinkOrder> drinkOrders = new List<DrinkOrder>();
            if (viewModel.DrinkOrders == null)
            {
                return drinkOrders;
            }

            foreach (var drinkOrderViewModel in viewModel.DrinkOrders)
            {
                if (drinkOrderViewModel.ChosenExtraIds == null)
                {
                    drinkOrderViewModel.ChosenExtraIds = new List<int>();
                }
                DrinkOrder drinkOrder = new DrinkOrder();
                drinkOrder.Drink = drinkOrderViewModel.Drink;
                drinkOrder.ChosenExtras = new List<DrinkOrderExtra>();
                drinkOrder.DrinkSize = sizeRepo.Get((int)drinkOrderViewModel.DrinkSize + 1);
                foreach (var id in drinkOrderViewModel.ChosenExtraIds)
                {
                    var extra = extraRepo.Get(id);
                    DrinkOrderExtra drinkOrderExtra = new DrinkOrderExtra();
                    drinkOrderExtra.DrinkOrder = drinkOrder;
                    drinkOrderExtra.Extra = extra;

                    drinkOrder.ChosenExtras.Add(drinkOrderExtra);
                }
                drinkOrders.Add(drinkOrder);
            }
            return drinkOrders;
        }
        private void SetChosenSnackExtras(PartialSnackOrder viewModel)
        {
            if (viewModel.ChosenExtraIds == null)
            {
                viewModel.ChosenExtraIds = new List<int>();
                viewModel.ChosenExtras = new List<Extra>();
            }
            else
            {
                SetSnackExtrasFromIds(viewModel, viewModel.ChosenExtraIds);
            }
        }
        private double GetDrinkSizeMultiplier(PartialDrinkOrder viewModel)
        {
            try
            {
                var drinkSize = (int)viewModel.DrinkSize;
                var multiplier = sizeRepo.GetAll()[drinkSize].PriceMultiplier;
                return multiplier;
            }
            catch
            {
                return 1;
            }
        }
        private void SetChosenDrinkExtras(PartialDrinkOrder viewModel)
        {
            if (viewModel.ChosenExtraIds == null)
            {
                viewModel.ChosenExtraIds = new List<int>();
                viewModel.ChosenExtras = new List<Extra>();
            }
            else
            {
                SetDrinkExtrasFromIds(viewModel, viewModel.ChosenExtraIds);
            }
        }
        private void SetSnackExtrasFromIds(PartialSnackOrder snackOrder, List<int> extraIds)
        {
            snackOrder.ChosenExtras = new List<Extra>();
            foreach (var extraId in extraIds)
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
        private void SetDrinkExtrasFromIds(PartialDrinkOrder drinkOrder, List<int> extraIds)
        {
            drinkOrder.ChosenExtras = new List<Extra>();
            foreach (var extraId in extraIds)
            {
                try
                {
                    Extra extra = extraRepo.Get(extraId);
                    drinkOrder.ChosenExtras.Add(extra);
                }
                catch
                {
                    continue;
                }
            }
        }        
        private double CalculateTotalOrderPrice(OrderViewModel viewModel)
        {
            double price = 0;
            foreach(var snackOrder in viewModel.SnackOrders)
            {
                price += orderLogic.CalculateSnackOrderPrice(snackOrder);
            }
            foreach (var drinkOrder in viewModel.DrinkOrders)
            {
                var priceMultiplier = GetDrinkSizeMultiplier(drinkOrder);
                price += orderLogic.CalculateDrinkOrderPrice(drinkOrder, priceMultiplier);
            }
            return price;
        }
        private void PopulateSnackList(ref OrderViewModel viewModel)
        {
            var allSnacks = snackRepo.GetAllActive();
            viewModel.AllSnacks = new List<Snack>();
            foreach (Snack snack in allSnacks)
            {
                viewModel.AllSnacks.Add(snack);
            }
        }
        private void PopulateDrinkList(ref OrderViewModel viewModel)
        {
            var allDrinks = drinkRepo.GetAllActive();
            viewModel.AllDrinks = new List<Drink>();
            foreach (Drink drink in allDrinks)
            {
                viewModel.AllDrinks.Add(drink);
            }
        }
        private void PopulateAvailableSnackExtras(ref PartialSnackOrder viewModel)
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
        private void PopulateAvailableDrinkExtras(ref PartialDrinkOrder viewModel)
        {
            var allExtrasOfDrink = drinkRepo.GetExtras(viewModel.Drink.Id);
            viewModel.AvailableExtras = new List<AssignedExtra>();

            foreach (Extra extra in allExtrasOfDrink)
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
        private void PopulateDrinkPrices(ref PartialDrinkOrder viewModel)
        {
            var drinkSizes = sizeRepo.GetAll();
            viewModel.FormattedPrices = new List<string>();
            foreach (var size in drinkSizes)
            {
                double mult = size.PriceMultiplier;
                viewModel.SetFormattedPrice(viewModel.Drink.MinimalPrice * mult);
                viewModel.FormattedPrices.Add(viewModel.GetFormattedPrice());
            }
        }
        private OrderViewModel InitOrderViewModel()
        {
            OrderViewModel viewModel = CreateNewOrderViewModel();
            orderViewModel = viewModel;
            return viewModel;
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
    }
}
