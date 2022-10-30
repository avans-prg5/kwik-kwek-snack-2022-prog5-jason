using KwikKwekSnack.Models;

namespace KwikKwekSnackWeb.Models
{
    public interface IOrderLogic
    {
        public double CalculateSnackOrderPrice(PartialSnackOrder snackOrder);
        public double CalculateDrinkOrderPrice(PartialDrinkOrder drinkOrder, double sizeMultiplier);
    }
}
