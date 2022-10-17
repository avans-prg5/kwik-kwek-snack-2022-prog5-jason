using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KwikKwekSnack.Domain.Repositories
{
    public class DrinkOrderRepoSql : IDrinkOrderRepo
    {
        readonly KwikKwekSnackContext ctx;
        public DrinkOrderRepoSql(KwikKwekSnackContext context)
        {
            ctx = context;
        }
        public DrinkOrder Create(DrinkOrder drinkOrder)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DrinkOrder Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<DrinkOrder> GetAll()
        {
            throw new NotImplementedException();
        }

        public DrinkOrder Update(DrinkOrder drinkOrder)
        {
            throw new NotImplementedException();
        }
    }
}
