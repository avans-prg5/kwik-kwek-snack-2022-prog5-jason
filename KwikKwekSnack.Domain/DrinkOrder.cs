using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KwikKwekSnack.Domain
{
    public class DrinkOrder
    {
        [Key]
        public int DrinkOrderId { get; set; }
        [Required]
        public Drink Drink { get; set; }
        [Required]
        public DrinkSize DrinkSize { get; set; }
        [DisplayName("Drink options")]        
        public virtual ICollection<DrinkOrderExtra> ChosenExtras { get; set; }
        public int OrderId { get; set; }

    }
}
