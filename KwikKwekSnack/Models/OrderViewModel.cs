using KwikKwekSnack.Domain;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace KwikKwekSnack.Models
{
    public class OrderViewModel
    {
        private string formattedPrice;
        public Order Order { get; set; }
        public List<PartialDrinkOrder> DrinkOrders { get; set; }
        public List<PartialSnackOrder> SnackOrders { get; set; }
        public List<Drink> AllDrinks { get; set; }
        public List<Snack> AllSnacks { get; set; }
        public OrderDeliveryType DeliveryType { get; set; }

        public string GetFormattedPrice()
        {
            return formattedPrice;
        }

        public void SetFormattedPrice(double value)
        {
            string priceString = "€";
            value = Math.Round(value, 2);
            priceString += value.ToString();
            if (value == Math.Round(value, 1) && value != Math.Round(value, 0))
            {
                priceString += "0";
            }
            formattedPrice = priceString;
        }
    }   

    public class PartialDrinkOrder
    {
        private string formattedOrderCost;
        public string GetFormattedPrice()
        {
            return formattedOrderCost;
        }

        public void SetFormattedPrice(double value)
        {
            string priceString = "€";
            value = Math.Round(value, 2);
            priceString += value.ToString();
            if (value == Math.Round(value, 1) && value != Math.Round(value, 0))
            {
                priceString += "0";
            }
            formattedOrderCost = priceString;
        }
        public Drink Drink { get; set; }        
        public List<AssignedExtra> AvailableExtras { get; set; } //All extras for the drink that a user can choose from.
        public List<int> ChosenExtraIds { get; set; } //The extraIds that the user chose to add to their order (for checkbox). 
        public List<Extra> ChosenExtras { get; set; } //The extras that the user chose to add to their order (for sidebar).
    }

    public class PartialSnackOrder
    {
        private string formattedOrderCost;
        public string GetFormattedPrice()
        {
            return formattedOrderCost;
        }

        public void SetFormattedPrice(double value)
        {
            string priceString = "€";
            value = Math.Round(value, 2);
            priceString += value.ToString();
            if (value == Math.Round(value, 1) && value != Math.Round(value, 0))
            {
                priceString += "0";
            }
            formattedOrderCost = priceString;
        }
        public Snack Snack { get; set; }        
        public List<AssignedExtra> AvailableExtras { get; set; }
        public List<int> ChosenExtraIds { get; set; }
        public List<Extra> ChosenExtras { get; set; }
    }
}
    
    
