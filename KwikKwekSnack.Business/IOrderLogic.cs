using KwikKwekSnack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KwikKwekSnack.Business
{
    public interface IOrderLogic
    {
        public double CalculateSnackOrderPrice(PartialSnackOrder snackOrder);
        public double CalculateDrinkOrderPrice(PartialDrinkOrder drinkOrder, double sizeMultiplier);
        public double CalculateTotalOrderPrice(OrderViewModel viewModel);
    }
}
