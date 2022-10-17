using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KwikKwekSnack.Domain
{
    public class SnackExtra
    {
        [Key]
        public int SnackId { get; set; }
        [Key]
        public int ExtraId { get; set; }
        public Snack Snack { get; set; }
        public Extra Extra { get; set; }        
    }
}
