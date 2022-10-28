using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KwikKwekSnack.Domain.Repositories
{
    public class OrderRepoSql : IOrderRepo
    {
        readonly KwikKwekSnackContext ctx;
        public OrderRepoSql(KwikKwekSnackContext context)
        {
            ctx = context;
        }

        public SnackOrder CreateSnackOrder(SnackOrder snackOrder, List<int> extras)
        {
            if (extras == null || extras.Count <= 0)
            {
                snackOrder.ChosenExtras = new List<SnackOrderExtra>();
                ctx.SnackOrders.Add(snackOrder);
                ctx.SaveChanges();
                return snackOrder;
            }

            snackOrder.ChosenExtras = new List<SnackOrderExtra>();
            foreach (var extra in ctx.Extras)
            {
                if (extras.Contains(extra.Id))
                {
                    snackOrder.ChosenExtras.Add(new SnackOrderExtra { SnackOrderId = snackOrder.SnackOrderId, ExtraId = extra.Id });
                }
            }

            ctx.SnackOrders.Add(snackOrder);
            ctx.SaveChanges();
            return snackOrder;
        }

        public DrinkOrder CreateDrinkOrder(DrinkOrder drinkOrder, List<int> extras)
        {
            if (extras == null)
            {
                drinkOrder.ChosenExtras = new List<DrinkOrderExtra>();
                ctx.DrinkOrders.Add(drinkOrder);
                ctx.SaveChanges();
                return drinkOrder;
            }

            drinkOrder.ChosenExtras = new List<DrinkOrderExtra>();
            foreach (var extra in ctx.Extras)
            {
                if (extras.Contains(extra.Id))
                {
                    drinkOrder.ChosenExtras.Add(new DrinkOrderExtra { DrinkOrderId = drinkOrder.DrinkOrderId, ExtraId = extra.Id });
                }
            }

            ctx.DrinkOrders.Add(drinkOrder);
            ctx.SaveChanges();
            return drinkOrder;
        }

        public Order Create(Order order, List<SnackOrder> snackOrders, List<DrinkOrder> drinkOrders, OrderDeliveryType deliveryType)
        {
            order.SnackOrders = snackOrders;
            order.DrinkOrders = drinkOrders;
            order.CreatedDateTime = DateTime.Now;
            order.DeliveryType = deliveryType;

            if(order.SnackOrders.Count > 0 || order.DrinkOrders.Count > 0)
            {
                order.Status = OrderStatusType.OrderCreated;
                ctx.Orders.Add(order);
                ctx.SaveChanges();
            }
            
            return order;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Order Get(int id)
        {
            return ctx.Orders.FirstOrDefault(o => o.Id == id);
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Order Update(Order order)
        {
            throw new NotImplementedException();
        }

        public List<SnackOrder> GetSnackOrders(int id)
        {
            var order = Get(id);
            if(order.SnackOrders == null)
            {
                return new List<SnackOrder>();
            }
            return Get(id).SnackOrders.ToList();
        }

        public List<DrinkOrder> GetDrinkOrders(int id)
        {
            var order = Get(id);
            if (order.DrinkOrders == null)
            {
                return new List<DrinkOrder>();
            }
            return Get(id).DrinkOrders.ToList();
        }

        public void CleanUpUnused()
        {
            var unusedOrders = ctx.Orders.Where(o => o.Status == OrderStatusType.NotCreated).Include(s => s.SnackOrders).Include(d => d.DrinkOrders);
            //include snackorderextras??
            foreach(var order in unusedOrders)
            {
                ctx.Orders.Remove(order);
            }
            
            ctx.SaveChanges();
        }

        public Order Create(Order order, List<SnackOrder> snackOrders, List<DrinkOrder> drinkOrders)
        {
            throw new NotImplementedException();
        }
    }
}
