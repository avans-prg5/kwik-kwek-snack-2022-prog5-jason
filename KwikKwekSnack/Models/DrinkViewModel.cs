using KwikKwekSnack.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections;
using System.Drawing;
using System;

namespace KwikKwekSnack.Models
{
    public class DrinkViewModel
    {
        private string formattedPrice;
        public Drink Drink { get; set; }
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
