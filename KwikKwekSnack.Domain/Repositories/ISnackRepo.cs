using System.Collections.Generic;

namespace KwikKwekSnack.Domain.Repositories
{
    public interface ISnackRepo
    {
        List<Snack> GetAll();
        Snack Get(int id);
        bool Delete(int id);
        Snack Update(Snack snack, List<int> extras);
        Snack Create(Snack snack, List<int> extras);
        public List<Extra> GetExtras(int id);
    }
}
