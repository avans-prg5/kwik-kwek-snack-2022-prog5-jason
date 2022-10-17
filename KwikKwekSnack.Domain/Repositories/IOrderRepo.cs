using System;
using System.Collections.Generic;
using System.Linq;
namespace KwikKwekSnack.Domain.Repositories
{
    public interface IOrderRepo
    {
        List<Order> GetAll();
        Order Get(int id);
        bool Delete(int id);
        Order Update(Order order);
        Order Create(Order order);
        SnackOrder AddSnackOrder(SnackOrder snackOrder, int id);
        List<SnackOrder> GetSnackOrders(int id);
        List<DrinkOrder> GetDrinkOrders(int id);
        void CleanUpUnused();
    }
}
