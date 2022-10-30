using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KwikKwekSnack.Domain
{
    public class Snack
    {
        [DisplayName("AfbeeldingsURL")]
        public string ImageURL { get; set; }
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Naam")]
        public string Name { get; set; }
        [DisplayName("Beschrijving")]
        public string Description { get; set; }
        
        [Required]
        [DisplayName("Standaard prijs")]
        public double StandardPrice { get; set; }
        public virtual ICollection<SnackExtra> AvailableExtras { get; set; }
        [DisplayName("Actief")]
        public bool Active { get; set; }
    }
}
