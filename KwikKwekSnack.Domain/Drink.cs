using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace KwikKwekSnack.Domain
{
    public class Drink
    {
        [DisplayName("AfbeeldingsURL")]
        public string ImageURL { get; set; }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        [DisplayName("Naam")]
        public string Name { get; set; }
        [DisplayName("Beschrijving")]
        public string Description { get; set; }
        [Required]
        [DisplayName("Minimale prijs")] 
        public double MinimalPrice { get; set; }       
        public virtual ICollection<DrinkExtra> AvailableExtras { get; set; }
        [DisplayName("Actief")]
        public bool Active { get; set; }
    }
}
