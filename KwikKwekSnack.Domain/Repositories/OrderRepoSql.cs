using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            snackOrder.ChosenExtras = new List<SnackOrderExtra>();
            if (extras == null || extras.Count <= 0)
            {
                Order order = new Order();
                order.SnackOrders = new List<SnackOrder>();
                order.SnackOrders.Add(snackOrder);

                ctx.Orders.Add(order);
                ctx.SaveChanges();
                //ctx.SnackOrders.Add(snackOrder);                
                //ctx.SaveChanges();
                return snackOrder;
            }
            
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

        public Order Create(Order order, List<SnackOrder> snackOrders, List<DrinkOrder> drinkOrders, OrderDeliveryType deliveryType)
        {
            order = CreateEmptyOrder(order);
            order.DeliveryType = deliveryType;            
            ctx.Orders.Add(order);            
            ctx.SaveChanges();

            foreach (var snackOrder in snackOrders)
            {
                AddSnackOrderToOrder(order, snackOrder);
            }

            foreach (var drinkOrder in drinkOrders)
            {
                AddDrinkOrderToOrder(order, drinkOrder);
            }
            
            if (order.SnackOrders.Count > 0 || order.DrinkOrders.Count > 0)
            {
                try
                {
                    order.Status = OrderStatusType.OrderCreated;
                    ctx.Orders.Update(order);
                    ctx.SaveChanges();
                }
                catch
                {
                    Delete(order.Id);
                }                
            }
            else
            {
                Delete(order.Id);
            }

            return order;
        }
        
        private void AddSnackOrderToOrder(Order order, SnackOrder snackOrder)
        {
            List<SnackOrderExtra> snackOrderExtras = new List<SnackOrderExtra>();
            if (snackOrder.ChosenExtras == null)
            {
                snackOrder.ChosenExtras = new List<SnackOrderExtra>();
            }
            foreach (var snackOrderExtra in snackOrder.ChosenExtras)
            {
                var extra = ctx.Extras.FirstOrDefault(e => e.Id == snackOrderExtra.Extra.Id);
                snackOrderExtras.Add(new SnackOrderExtra { ExtraId = extra.Id, SnackOrderId = snackOrder.SnackOrderId });
            }
            var snack = ctx.Snacks.FirstOrDefault(s => s.Id == snackOrder.Snack.Id);
            order.SnackOrders.Add(new SnackOrder { Snack = snack, OrderId = order.Id, ChosenExtras = snackOrderExtras });
        }

        private void AddDrinkOrderToOrder(Order order, DrinkOrder drinkOrder)
        {
            List<DrinkOrderExtra> drinkOrderExtras = new List<DrinkOrderExtra>();
            if(drinkOrder.ChosenExtras == null)
            {
                drinkOrder.ChosenExtras = new List<DrinkOrderExtra>();
            }
            foreach (var drinkOrderExtra in drinkOrder.ChosenExtras)
            {
                var extra = ctx.Extras.FirstOrDefault(e => e.Id == drinkOrderExtra.Extra.Id);
                drinkOrderExtras.Add(new DrinkOrderExtra { ExtraId = extra.Id, DrinkOrderId = drinkOrder.DrinkOrderId });
            }
            var drink = ctx.Drinks.FirstOrDefault(d => d.Id == drinkOrder.Drink.Id);
            var drinkSize = ctx.DrinkSizes.FirstOrDefault(s => s.Id == 1);
            order.DrinkOrders.Add(new DrinkOrder { Drink = drink, OrderId = order.Id, ChosenExtras = drinkOrderExtras, DrinkSize = drinkSize });
        }

        private Order CreateEmptyOrder(Order order)
        {
            order.SnackOrders = new List<SnackOrder>();
            order.DrinkOrders = new List<DrinkOrder>();
            order.CreatedDateTime = DateTime.Now;            
            order.Status = OrderStatusType.NotCreated;
            ctx.Orders.Add(order);
            return order;
        }

        public bool Delete(int id)
        {
            Order orderToDelete = Get(id);
            foreach (var snackOrder in orderToDelete.SnackOrders)
            {
                ctx.Remove(snackOrder);
            }

            foreach (var drinkOrder in orderToDelete.DrinkOrders)
            {
                ctx.Remove(drinkOrder);
            }

            var toRemove = ctx.Orders.Find(id);
            if (toRemove != null)
            {
                ctx.Orders.Remove(toRemove);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public Order Get(int id)
        {
            return ctx.Orders.Include(s => s.SnackOrders).Include(d => d.DrinkOrders).FirstOrDefault(o => o.Id == id);
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
        
    }
}
