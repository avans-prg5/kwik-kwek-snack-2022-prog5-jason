using System.Collections.Generic;

namespace KwikKwekSnack.Domain.Repositories
{
    public interface IDrinkSizeRepo
    {
        List<DrinkSize> GetAll();
        DrinkSize Get(int id);        
    }
}
