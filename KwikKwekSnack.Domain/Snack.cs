using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KwikKwekSnack.Domain
{
    public class Snack
    {
        public string ImageURL { get; set; }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        [Required]
        public double StandardPrice { get; set; }
        public virtual ICollection<SnackExtra> AvailableExtras { get; set; }
        public bool Active { get; set; }
    }
}
