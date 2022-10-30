using KwikKwekSnack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KwikKwekSnack.Business
{
    public class OrderLogic : IOrderLogic
    {
        public double CalculateSnackOrderPrice(PartialSnackOrder snackOrder)
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

        public double CalculateDrinkOrderPrice(PartialDrinkOrder drinkOrder, double sizeMultiplier)
        {
            double price = 0;
            price += drinkOrder.Drink.MinimalPrice;
            price *= sizeMultiplier;
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

        public double CalculateTotalOrderPrice(OrderViewModel viewModel)
        {
            double price = 0;
            foreach (var snackOrder in viewModel.SnackOrders)
            {
                price += CalculateSnackOrderPrice(snackOrder);
            }
            foreach (var drinkOrder in viewModel.DrinkOrders)
            {
                var priceMultiplier = GetDrinkSizeMultiplier(drinkOrder);
                price += CalculateDrinkOrderPrice(drinkOrder, priceMultiplier);
            }
            return price;
        }
    }
}
