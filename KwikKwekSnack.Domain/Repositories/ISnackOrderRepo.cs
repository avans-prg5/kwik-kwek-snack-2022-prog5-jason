using System.Collections.Generic;

namespace KwikKwekSnack.Domain.Repositories
{
    public interface ISnackOrderRepo
    {
        List<SnackOrder> GetAll();
        SnackOrder Get(int id);
        bool Delete(int id);
        SnackOrder Update(SnackOrder snackOrder, List<int> extras);
        SnackOrder Create(SnackOrder snackOrder, List<int> extras);
    }
}
