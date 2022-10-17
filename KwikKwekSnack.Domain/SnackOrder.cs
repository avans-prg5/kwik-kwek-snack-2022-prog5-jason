using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KwikKwekSnack.Domain
{
    public class SnackOrder
    {
        [Key]
        public int SnackOrderId { get; set; }
        [Required]
        public Snack Snack { get; set; }
        public virtual ICollection<SnackOrderExtra> ChosenExtras { get; set; }
    }
}
