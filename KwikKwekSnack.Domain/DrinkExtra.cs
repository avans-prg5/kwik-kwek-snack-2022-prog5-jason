using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KwikKwekSnack.Domain
{
    public class DrinkExtra
    {
        [Key]
        public int DrinkId { get; set; }
        [Key]
        public int ExtraId { get; set; }
        public Drink Drink { get; set; }
        public Extra Extra { get; set; }        
    }
}
