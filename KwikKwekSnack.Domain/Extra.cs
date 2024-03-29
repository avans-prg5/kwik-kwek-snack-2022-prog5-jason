﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KwikKwekSnack.Domain
{
    public class Extra
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        [DisplayName("Naam")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Prijs")]
        public double Price { get; set; }
        [DisplayName("Actief")]
        public bool Active { get; set; }
        public virtual ICollection<DrinkExtra> ExtraOfDrink { get; set; }
        public virtual ICollection<SnackExtra> ExtraOfSnack { get; set; }
        public virtual ICollection<DrinkOrderExtra> ExtraOfDrinkOrder { get; set; }
        public virtual ICollection<SnackOrderExtra> ExtraOfSnackOrder { get; set; }
    }
}
