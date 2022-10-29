using KwikKwekSnack.Domain;
using KwikKwekSnack.Domain.Repositories;
using KwikKwekSnack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace KwikKwekSnackWeb.Controllers
{    
    public class OrderController : Controller
    {
        IOrderRepo repo;
        IDrinkRepo drinkRepo;
        ISnackRepo snackRepo;        
        IExtraRepo extraRepo;

        static private OrderViewModel orderViewModel;

        public OrderController(IOrderRepo injectedRepository, IDrinkRepo injectedDrinkRepository, ISnackRepo injectedSnackRepository, IExtraRepo injectedExtraRepository)
        {
           repo = injectedRepository;
           drinkRepo = injectedDrinkRepository;
           snackRepo = injectedSnackRepository;
           extraRepo = injectedExtraRepository;
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
                viewModel.OrderCost = CalculateSnackOrderPrice(viewModel);
                orderViewModel.SnackOrders.Add(viewModel);
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
                viewModel.OrderCost = CalculateDrinkOrderPrice(viewModel);
                orderViewModel.DrinkOrders.Add(viewModel);
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
            var snackOrders = CreateSnackOrderListFromViewModel(orderViewModel);
            var drinkOrders = CreateDrinkOrderListFromViewModel(orderViewModel);
            var order = repo.Create(orderViewModel.Order, snackOrders, drinkOrders, orderViewModel.DeliveryType);     
            
            orderViewModel = CreateNewOrderViewModel();
            return View("Success");
        }

        public List<SnackOrder> CreateSnackOrderListFromViewModel(OrderViewModel viewModel)
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

        public List<DrinkOrder> CreateDrinkOrderListFromViewModel(OrderViewModel viewModel)
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

        private double CalculateSnackOrderPrice(PartialSnackOrder snackOrder)
        {
            double price = 0;
            price += snackOrder.Snack.StandardPrice;
            if (snackOrder.ChosenExtras != null)
            {
                foreach (var extra in snackOrder.ChosenExtras)
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

        private double CalculateDrinkOrderPrice(PartialDrinkOrder drinkOrder)
        {
            double price = 0;
            price += drinkOrder.Drink.MinimalPrice;
            if (drinkOrder.ChosenExtras != null)
            {
                foreach (var extra in drinkOrder.ChosenExtras)
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

        private void PopulateSnackList(ref OrderViewModel viewModel)
        {
            var allSnacks = snackRepo.GetAll();
            viewModel.AllSnacks = new List<Snack>();
            foreach (Snack snack in allSnacks)
            {
                viewModel.AllSnacks.Add(snack);
            }
        }

        private void PopulateDrinkList(ref OrderViewModel viewModel)
        {
            var allDrinks = drinkRepo.GetAll();
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
