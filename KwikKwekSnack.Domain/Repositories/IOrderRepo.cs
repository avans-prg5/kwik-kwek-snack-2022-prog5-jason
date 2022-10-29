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
        Order Create(Order order, List<SnackOrder> snackOrders, List<DrinkOrder> drinkOrders, OrderDeliveryType deliveryType);             
        List<SnackOrder> GetSnackOrders(int id);
        List<DrinkOrder> GetDrinkOrders(int id);

        SnackOrder CreateSnackOrder(SnackOrder snackOrder, List<int> extras);
        void CleanUpUnused();
    }
}
