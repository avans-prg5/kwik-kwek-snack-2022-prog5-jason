using KwikKwekSnack.Domain;
using KwikKwekSnackConsole.Models;


namespace KwikKwekSnackConsole.Views
{
	public class OrderView
	{
        private List<OrderViewModelConsole> orderViewModels;
        private OrderViewModelConsole currentOrder;
        private readonly ConsoleLogic consoleLogic;

        public OrderView()
        {
            consoleLogic = new ConsoleLogic();
            orderViewModels = new List<OrderViewModelConsole>();
            currentOrder = new OrderViewModelConsole();
        }

        public void Update(Order currOrder, Queue<Order> queue)
        {
            currentOrder = consoleLogic.ConvertModelToViewModel(currOrder);
            orderViewModels = new List<OrderViewModelConsole>();
            foreach (Order order in queue)
            {
                orderViewModels.Add(consoleLogic.ConvertModelToViewModel(order));
            }
            ShowOrders();
        }        
		private void ShowOrders()
		{
            Console.Clear();
            if (currentOrder != null)
            {
                Console.WriteLine(currentOrder.GetOrderNumber() + ": " + currentOrder.Status);
                if(currentOrder.Status == OrderStatusType.Ready.ToString())
                {
                    ShowOrderCompletionMessage(currentOrder);
                }
            }
            foreach (var order in orderViewModels)
            {
                Console.WriteLine(order.GetOrderNumber() + ": " + order.Status);
            }
            Console.WriteLine();
        }

        public void ShowStartupMessage()
        {
            Console.WriteLine("Wachten op orders...");
        }

        public void ShowExceptionMessage(Exception ex)
        {
            Console.WriteLine(ex.Message);
            System.Threading.Thread.Sleep(10000);
        }
        
        public void ShowEmptyQueueMessage()
        {
            Console.WriteLine("Geen orders om te verwerken");
        }

        private void ShowOrderCompletionMessage(OrderViewModelConsole order)
        {
            Console.WriteLine("Order " + order.Id + " is klaar om opgehaald te worden.");
        }

        public void ShowAppSettingsErrorMessage(Exception ex)
        {
            Console.WriteLine("Fout bij het ophalen van appsettings");
            ShowExceptionMessage(ex);
        }
    }
}
