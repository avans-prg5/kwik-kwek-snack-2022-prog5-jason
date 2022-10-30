using KwikKwekSnack.Domain;
using KwikKwekSnack.Domain.Repositories;
using KwikKwekSnackConsole.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KwikKwekSnackConsole.Controllers
{
    public class OrderApp
    {
        const int TICK_DURATION = 8000;

        private Queue<Order> queue;
        private bool appRunning;
        private IOrderRepo orderRepo;
        private Order? currentOrder;

        public OrderApp(IOrderRepo injectedOrderRepo)
        {
            orderRepo = injectedOrderRepo;
            queue = new Queue<Order>();
            appRunning = true;
            
            Console.WriteLine("Wachten op orders...");
        }
        public void Run()
        {            
            EnqueueNewOrders();
            currentOrder = queue.Dequeue();
            

            while (appRunning)
            {
                if(currentOrder.Status < OrderStatusType.Ready)
                {
                    HandleOrder(currentOrder);
                }
                else
                {
                    currentOrder = queue.Dequeue();
                }
                if(queue.Count <= 0 && currentOrder.Status == OrderStatusType.Ready)
                {
                    appRunning = false;
                }
                ShowOrders();
                System.Threading.Thread.Sleep(TICK_DURATION);
            }           
        }

        private void ShowOrders()
        {
            Console.Clear();
            if (currentOrder != null)
            {
                Console.WriteLine(currentOrder.Id + ": " + currentOrder.Status);
            }            
            foreach (var order in queue)
            {
                Console.WriteLine(order.Id + ": " + order.Status.ToString());
            }
            Console.WriteLine();
        }

        private void EnqueueNewOrders()
        {
            var orders = orderRepo.GetAll().Where(o => o.Status == OrderStatusType.OrderCreated).OrderBy(o => o.CreatedDateTime);
            foreach(var order in orders)
            {
                queue.Enqueue(order);
            }
        }

        private void HandleOrder(Order order)
        {
            order.Status++;
            if(order.Status == OrderStatusType.OrderCompleted)
            {
                CompleteOrder(order);
            }
            //orderRepo.Update(order);
        }

        private void CompleteOrder(Order order)
        {
            
        }       

    }
}
