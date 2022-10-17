using System.Collections.Generic;

namespace KwikKwekSnack.Domain.Repositories
{
    public interface IDrinkOrderRepo
    {
        List<DrinkOrder> GetAll();
        DrinkOrder Get(int id);
        bool Delete(int id);
        DrinkOrder Update(DrinkOrder drinkOrder);
        DrinkOrder Create(DrinkOrder drinkOrder);
    }
}
