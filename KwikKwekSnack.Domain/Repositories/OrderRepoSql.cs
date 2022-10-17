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

        public Order Create(Order order)
        {
            ctx.Orders.Add(order);
            ctx.SaveChanges();
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
        public SnackOrder AddSnackOrder(SnackOrder snackOrder, int id)
        {
            ctx.SnackOrders.Add(snackOrder);            
            var order = Get(id);
            if(order.SnackOrders == null)
            {
                order.SnackOrders = new List<SnackOrder>();
            }
            order.SnackOrders.Add(snackOrder);
            ctx.SaveChanges();
            return snackOrder;
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
