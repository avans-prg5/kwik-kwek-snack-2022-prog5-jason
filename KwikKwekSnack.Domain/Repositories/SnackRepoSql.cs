﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KwikKwekSnack.Domain.Repositories
{
    public class SnackRepoSql : ISnackRepo
    {
        readonly KwikKwekSnackContext ctx;
        public SnackRepoSql(KwikKwekSnackContext context)
        {
            ctx = context;
        }
        public Snack Create(Snack snack, List<int> extras)
        {
            if (extras == null)
            {
                extras = new List<int>();
            }
            snack.AvailableExtras = new List<SnackExtra>();
            foreach (var extra in ctx.Extras)
            {
                if (extras.Contains(extra.Id))
                {
                    snack.AvailableExtras.Add(new SnackExtra { SnackId = snack.Id, ExtraId = extra.Id });
                }
            }

            ctx.Snacks.Add(snack);
            ctx.SaveChanges();
            return snack;
        }

        public bool Delete(int id)
        {
            Snack snackToDelete = Get(id);
            foreach(var extra in snackToDelete.AvailableExtras)
            {                
                ctx.Remove(extra);
            }

            var toRemove = ctx.Snacks.Find(id);
            if (toRemove != null)
            {
                ctx.Snacks.Remove(toRemove);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public Snack Get(int id)
        {
            return ctx.Snacks.Include(s => s.AvailableExtras).ThenInclude(i => i.Extra).FirstOrDefault(d => d.Id == id);
        }

        public List<Snack> GetAll()
        {
            return ctx.Snacks.ToList();
        }

        public Snack Update(Snack snack, List<int> extras)
        {
            ctx.Attach(snack);
            ctx.Entry(snack).Collection(p => p.AvailableExtras).Load();
            var availableExtras = snack.AvailableExtras.Select(i => i.ExtraId);

            if (availableExtras == null)
            {
                availableExtras = new List<int>();
            }

            foreach (var extra in ctx.Extras)
            {
                if (extras.Contains(extra.Id))
                {
                    if (!availableExtras.Contains(extra.Id))
                    {
                        snack.AvailableExtras.Add(new SnackExtra { SnackId = snack.Id, ExtraId = extra.Id });
                    }
                }
                else
                {
                    if (availableExtras.Contains(extra.Id))
                    {
                        var itemToRemove = snack.AvailableExtras.FirstOrDefault(d => d.ExtraId == extra.Id);
                        ctx.Remove(itemToRemove);
                    }
                }
            }

            ctx.Snacks.Update(snack);
            ctx.SaveChanges();
            return snack;
        }

        public List<Extra> GetExtras(int id)
        {
            var snack = ctx.Snacks.FirstOrDefault(s => s.Id == id);
            ctx.Attach(snack);
            ctx.Entry(snack).Collection(p => p.AvailableExtras).Load();
            var availableExtras = snack.AvailableExtras.Select(i => i.ExtraId);
            var extras = new List<Extra>();            
            foreach(var extraId in availableExtras)
            {
                extras.Add(ctx.Extras.FirstOrDefault(e => e.Id == extraId));
            }
            return extras;
        }
    }
}
