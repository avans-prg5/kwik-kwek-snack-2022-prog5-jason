using KwikKwekSnack.Models;

namespace KwikKwekSnackWeb.Models
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
    }
}
