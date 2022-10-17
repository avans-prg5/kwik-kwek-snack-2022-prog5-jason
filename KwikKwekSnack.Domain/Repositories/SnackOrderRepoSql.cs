using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KwikKwekSnack.Domain.Repositories
{
    public class SnackOrderRepoSql : ISnackOrderRepo
    {
        readonly KwikKwekSnackContext ctx;
        public SnackOrderRepoSql(KwikKwekSnackContext context)
        {
            ctx = context;
        }
        public SnackOrder Create(SnackOrder snackOrder, List<int> extras)
        {
            if (extras == null)
            {
                extras = new List<int>();
            }
            snackOrder.ChosenExtras = new List<SnackOrderExtra>();
            foreach (var extra in ctx.Extras)
            {
                if (extras.Contains(extra.Id))
                {
                    snackOrder.ChosenExtras.Add(new SnackOrderExtra { SnackOrderId = snackOrder.SnackOrderId, ExtraId = extra.Id });
                }
            }
            ctx.SnackOrders.Add(snackOrder);
            ctx.SaveChanges();
            return snackOrder;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public SnackOrder Get(int id)
        {
            return ctx.SnackOrders.Include(s => s.ChosenExtras).ThenInclude(i => i.Extra).FirstOrDefault(d => d.SnackOrderId == id);
        }

        public List<SnackOrder> GetAll()
        {
            throw new NotImplementedException();
        }

        public SnackOrder Update(SnackOrder snackOrder, List<int> extras)
        {
            ctx.Attach(snackOrder);
            ctx.Entry(snackOrder).Collection(p => p.ChosenExtras).Load();
            var chosenExtras = snackOrder.ChosenExtras.Select(i => i.ExtraId);

            if (chosenExtras == null)
            {
                chosenExtras = new List<int>();
            }

            foreach (var extra in ctx.Extras)
            {
                if (extras.Contains(extra.Id))
                {
                    if (!chosenExtras.Contains(extra.Id))
                    {
                        snackOrder.ChosenExtras.Add(new SnackOrderExtra { SnackOrderId = snackOrder.SnackOrderId, ExtraId = extra.Id });
                    }
                }
                else
                {
                    if (chosenExtras.Contains(extra.Id))
                    {
                        var itemToRemove = snackOrder.ChosenExtras.FirstOrDefault(d => d.ExtraId == extra.Id);
                        ctx.Remove(itemToRemove);
                    }
                }
            }

            ctx.SnackOrders.Update(snackOrder);
            ctx.SaveChanges();
            return snackOrder;
        }
    }
}
