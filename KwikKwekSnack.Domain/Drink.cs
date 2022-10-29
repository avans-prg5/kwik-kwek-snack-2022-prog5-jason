using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace KwikKwekSnack.Domain
{
    public class Drink
    {
        public string ImageURL { get; set; }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }        
        public string Description { get; set; }
        [Required]
        [DisplayName("Minimal price")] 
        public double MinimalPrice { get; set; }       
        public virtual ICollection<DrinkExtra> AvailableExtras { get; set; }
        public bool Active { get; set; }
    }
}
