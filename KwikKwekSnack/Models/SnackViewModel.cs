using KwikKwekSnack.Domain;
using System.Collections.Generic;

namespace KwikKwekSnack.Models
{
    public class SnackViewModel
    {
        public Snack Snack { get; set; }
        public List<int> AvailableExtras { get; set; }
        public List<AssignedExtra> AssignedExtras { get; set; }
    }    
}
