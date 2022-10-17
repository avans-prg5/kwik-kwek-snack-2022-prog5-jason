using System.ComponentModel.DataAnnotations;

namespace KwikKwekSnack.Domain
{
    public class DrinkOrderExtra
    {
        [Key]
        public int DrinkOrderId { get; set; }
        [Key]
        public int ExtraId { get; set; }        
        public DrinkOrder DrinkOrder { get; set; }
        public Extra Extra { get; set; }

    }
}
