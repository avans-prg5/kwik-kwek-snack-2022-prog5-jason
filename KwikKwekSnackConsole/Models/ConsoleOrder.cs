using KwikKwekSnack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KwikKwekSnackConsole.Models
{
    public class ConsoleOrder
    {
        public int OrderNumber { get; set; }
        public DateTime OrderCreated { get; set; }
        public OrderStatusType Status { get; set; }
    }
}
