using System.Collections.Generic;

namespace KwikKwekSnack.Domain.Repositories
{
    public interface IUserRepo
    {
        List<User> GetAll();
        User Get(int id);
        bool Delete(int id);
        User Update(User user);
        User Create(User user);
    }
}
