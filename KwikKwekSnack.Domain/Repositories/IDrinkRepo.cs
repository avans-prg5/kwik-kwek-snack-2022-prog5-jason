using System.Collections.Generic;

namespace KwikKwekSnack.Domain.Repositories
{
    public interface IDrinkRepo
    {
        List<Drink> GetAll();
        List<Drink> GetAllActive();
        Drink Get(int id);
        bool Delete(int id);
        bool MakeInactive(int id);
        Drink Update(Drink drink, List<int> extras);
        Drink Create(Drink drink, List<int> extras);
        List<Extra> GetExtras(int id);
    }
}
