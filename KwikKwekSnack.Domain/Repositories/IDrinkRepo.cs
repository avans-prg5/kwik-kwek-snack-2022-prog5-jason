using System.Collections.Generic;

namespace KwikKwekSnack.Domain.Repositories
{
    public interface IDrinkRepo
    {
        List<Drink> GetAll();
        Drink Get(int id);
        bool Delete(int id);
        Drink Update(Drink drink, List<int> extras);
        Drink Create(Drink drink, List<int> extras);
    }
}
