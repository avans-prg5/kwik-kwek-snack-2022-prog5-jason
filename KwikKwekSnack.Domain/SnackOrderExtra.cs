using System.ComponentModel.DataAnnotations;

namespace KwikKwekSnack.Domain
{
    public class SnackOrderExtra
    {
        [Key]
        public int SnackOrderId { get; set; }
        [Key]
        public int ExtraId { get; set; }
        public SnackOrder SnackOrder { get; set; }
        public Extra Extra { get; set; }
    }
}
