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
        public Drink Drink { get; set; }
        public List<int> AvailableExtras { get; set; }
        public List<AssignedExtra> AssignedExtras { get; set; }              
    }    
}
