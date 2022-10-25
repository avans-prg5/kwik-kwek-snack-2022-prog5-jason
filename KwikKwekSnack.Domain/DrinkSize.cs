using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KwikKwekSnack.Domain
{
    public class DrinkSize
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 1)]
        public string ShortName { get; set; }
        public string FullName { get; set; }
        [Required]
        [DefaultValue(1.00)]
        public double PriceMultiplier { get; set; } //e.g. 1.25
    }
}
