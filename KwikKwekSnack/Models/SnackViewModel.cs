using KwikKwekSnack.Domain;
using System;
using System.Collections.Generic;

namespace KwikKwekSnack.Models
{
    public class SnackViewModel
    {
        private string formattedPrice;
        public Snack Snack { get; set; }
        public List<int> AvailableExtras { get; set; }
        public List<AssignedExtra> AssignedExtras { get; set; }
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
        public string GetFormattedPrice()
        {
            return formattedPrice;
        }
    }    
}
