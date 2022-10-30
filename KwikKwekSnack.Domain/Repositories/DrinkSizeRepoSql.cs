using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KwikKwekSnack.Domain.Repositories
{
    public class DrinkSizeRepoSql : IDrinkSizeRepo
    {
        readonly KwikKwekSnackContext ctx;
        public DrinkSizeRepoSql(KwikKwekSnackContext context)
        {
            ctx = context;
        }
        public DrinkSize Create(DrinkSize drinkSize)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DrinkSize Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<DrinkSize> GetAll()
        {
            return ctx.DrinkSizes.ToList();
        }

        public DrinkSize Update(DrinkSize drinkSize)
        {
            throw new NotImplementedException();
        }
    }
}
