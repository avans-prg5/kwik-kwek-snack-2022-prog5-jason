using System.Collections.Generic;

namespace KwikKwekSnack.Domain.Repositories
{
    public interface IExtraRepo
    {
        List<Extra> GetAll();
        Extra Get(int id);
        bool Delete(int id);
        Extra Update(Extra extra);
        Extra Create(Extra extra);
    }
}
