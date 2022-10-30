using System.Collections.Generic;

namespace KwikKwekSnack.Domain.Repositories
{
    public interface ISnackRepo
    {
        List<Snack> GetAll();
        List<Snack> GetAllActive();
        Snack Get(int id);
        bool Delete(int id);
        bool MakeInactive(int id);
        Snack Update(Snack snack, List<int> extras);
        Snack Create(Snack snack, List<int> extras);
        public List<Extra> GetExtras(int id);
    }
}
