using KwikKwekSnack.Domain;
using KwikKwekSnack.Domain.Repositories;
using KwikKwekSnackConsole.Views;
using Microsoft.Extensions.Configuration;


namespace KwikKwekSnackConsole.Controllers
{
    public class OrderApp
    {
        const int TICK_DURATION_FALLBACK = 5;
        
        private readonly IOrderRepo repo;
        private readonly IConfigurationRoot config;
        private readonly int tickDuration;
        private Order? currentOrder;
        private Queue<Order> queue;
        private bool appRunning;
        private readonly PeriodicTimer timer;
        private readonly OrderView view;

        public OrderApp(IOrderRepo orderRepo, IConfigurationRoot config)
        {
            this.config = config;
            repo = orderRepo;
            tickDuration = SetTickDuration();
            timer = new PeriodicTimer(TimeSpan.FromSeconds(tickDuration));
            queue = new Queue<Order>();
            view = new OrderView();
            appRunning = true;            
        }      

        private int SetTickDuration()
        {
            var tick = config.GetSection("AppSettings")["TickDuration"];
            try
            {
                return Int32.Parse(tick);
            }
            catch(Exception ex)
            {                
                view.ShowAppSettingsErrorMessage(ex);              
                return TICK_DURATION_FALLBACK;                
            }
        }
        public void Run()
        {
            view.ShowStartupMessage();
            EnqueueNewOrders();
            currentOrder = queue.Dequeue();            

            while (appRunning)
            {
                WorkOnCurrentOrder();
                if(IsQueueEmpty())
                {
                    appRunning = false;
                }
                view.Update(currentOrder, queue);
                System.Threading.Thread.Sleep(tickDuration * 1000);
            }           
        }
        
        private void WorkOnCurrentOrder()
        {
            if(currentOrder == null)
            {
                return;
            }
            if (currentOrder.Status < OrderStatusType.Ready)
            {
                HandleOrder(currentOrder);
            }
            else
            {
                currentOrder = queue.Dequeue();
            }
        }

        private bool IsQueueEmpty()
        {
            if(currentOrder == null)
            {
                return queue.Count <= 0;
            }
            return queue.Count <= 0 && currentOrder.Status == OrderStatusType.Ready;
        }

        private void EnqueueNewOrders()
        {
            var orders = repo.GetAll().Where(o => o.Status != OrderStatusType.OrderCompleted).OrderBy(o => o.CreatedDateTime);
            foreach(var order in orders)
            {
                queue.Enqueue(order);
            }
            if(orders.Count() == 0)
            {
                view.ShowEmptyQueueMessage();
            }
        }

        private void HandleOrder(Order order)
        {
            try
            {
                order.Status++;
                repo.Update(order);
            }
            catch(Exception ex)
            {
                view.ShowExceptionMessage(ex);
            }            
        }
    }
}
