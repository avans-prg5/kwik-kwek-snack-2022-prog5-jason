using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KwikKwekSnack.Domain.Repositories
{
    public class ExtraRepoSql : IExtraRepo
    {
        readonly KwikKwekSnackContext ctx;
        public ExtraRepoSql(KwikKwekSnackContext context)
        {
            ctx = context;
        }
        public Extra Create(Extra extra)
        {
            ctx.Extras.Add(extra);
            ctx.SaveChanges();
            return extra;
        }

        public bool Delete(int id)
        {
            var toRemove = ctx.Extras.Find(id);
            if (toRemove != null)
            {
                ctx.Extras.Remove(toRemove);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public Extra Get(int id)
        {
            return ctx.Extras.FirstOrDefault(d => d.Id == id);
        }

        public List<Extra> GetAll()
        {
            return ctx.Extras.ToList();
        }

        public Extra Update(Extra extra)
        {
            ctx.Attach(extra);
            ctx.Extras.Update(extra);
            ctx.SaveChanges();
            return extra;
        }
    }
}
