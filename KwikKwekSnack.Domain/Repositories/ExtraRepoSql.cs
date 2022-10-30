using Microsoft.EntityFrameworkCore;
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
            extra.Active = true;
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
        public bool MakeInactive(int id)
        {
            try
            {
                var toRemove = ctx.Extras.FirstOrDefault(d => d.Id == id);
                ctx.Attach(toRemove);
                toRemove.Active = false;
                ctx.Extras.Update(toRemove);
                ctx.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Extra Get(int id)
        {
            return ctx.Extras.FirstOrDefault(d => d.Id == id);
        }

        public List<Extra> GetAll()
        {
            return ctx.Extras.ToList();
        }

        public List<Extra> GetAllActive()
        {
            return ctx.Extras.Where(e => e.Active == true).ToList();
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
