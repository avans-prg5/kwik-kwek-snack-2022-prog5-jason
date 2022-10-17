using KwikKwekSnack.Domain;
using System.Collections.Generic;

namespace KwikKwekSnack.Models
{
    public class SnackOrderViewModel
    {
        public SnackOrder SnackOrder { get; set; }
        public int OrderId { get; set; }
        public Snack Snack { get; set; }
        public List<Snack> AllSnacks { get; set; }
    }
}
