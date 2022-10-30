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

        public DrinkSize Get(int id)
        {
            try
            {
                return ctx.DrinkSizes.First(d => d.Id == id);
            }
            catch
            {
                return ctx.DrinkSizes.FirstOrDefault(d => d.Id == 1);
            }          
        }

        public List<DrinkSize> GetAll()
        {
            return ctx.DrinkSizes.ToList();
        }        
    }
}
