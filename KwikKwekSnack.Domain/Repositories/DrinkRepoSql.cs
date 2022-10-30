using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KwikKwekSnack.Domain.Repositories
{
    public class DrinkRepoSql : IDrinkRepo
    {
        readonly KwikKwekSnackContext ctx;
        public DrinkRepoSql(KwikKwekSnackContext context)
        {
            ctx = context;
        }

        public Drink Create(Drink drink, List<int> extras)
        {
            drink.Active = true;
            if(extras == null)
            {
                extras = new List<int>();
                ctx.Drinks.Add(drink);
                ctx.SaveChanges();
                return drink;
            }
            drink.AvailableExtras = new List<DrinkExtra>();
            foreach (var extra in ctx.Extras)
            {
                if (extras.Contains(extra.Id))
                {
                    drink.AvailableExtras.Add(new DrinkExtra { DrinkId = drink.Id, ExtraId = extra.Id });                    
                }
            }

            ctx.Drinks.Add(drink);
            ctx.SaveChanges();
            return drink;
        }

        public bool Delete(int id)
        {
            var toRemove = ctx.Drinks.Include(d => d.AvailableExtras).FirstOrDefault(d => d.Id == id);
            
            if(toRemove != null)
            {
                ctx.Drinks.Remove(toRemove);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public bool MakeInactive(int id)
        {
            try
            {
                var toRemove = ctx.Drinks.Include(d => d.AvailableExtras).FirstOrDefault(d => d.Id == id);
                ctx.Attach(toRemove);
                toRemove.Active = false;
                ctx.Drinks.Update(toRemove);
                ctx.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }           

        public Drink Get(int id)
        {
            return ctx.Drinks.Include(d => d.AvailableExtras).ThenInclude(i => i.Extra).FirstOrDefault(d => d.Id == id);            
        }

        public List<Drink> GetAll()
        {
            return ctx.Drinks.Include(d => d.AvailableExtras).ThenInclude(i => i.Extra).ToList();
        }
        public List<Drink> GetAllActive()
        {
            return ctx.Drinks.Where(d => d.Active == true).Include(d => d.AvailableExtras).ThenInclude(i => i.Extra).ToList();
        }
        public Drink Update(Drink drink, List<int> extras)
        {            
            ctx.Attach(drink);
            ctx.Entry(drink).Collection(p => p.AvailableExtras).Load();
            var availableExtras = drink.AvailableExtras.Select(i => i.ExtraId);
            if(availableExtras == null)
            {
                availableExtras = new List<int>();
            }
            foreach (var extra in ctx.Extras)
            {
                if(extras.Contains(extra.Id))
                {
                    if(!availableExtras.Contains(extra.Id))
                    {
                        drink.AvailableExtras.Add(new DrinkExtra { DrinkId = drink.Id, ExtraId = extra.Id });
                    }
                }
                else
                {
                    if(availableExtras.Contains(extra.Id))
                    {
                        var itemToRemove = drink.AvailableExtras.FirstOrDefault(d => d.ExtraId == extra.Id);
                        ctx.Remove(itemToRemove);
                    }
                }
            }

            ctx.Drinks.Update(drink);
            ctx.SaveChanges();
            return drink;
        }

        public List<Extra> GetExtras(int id)
        {
            var drink = ctx.Drinks.FirstOrDefault(s => s.Id == id);
            ctx.Attach(drink);
            ctx.Entry(drink).Collection(p => p.AvailableExtras).Load();
            var availableExtras = drink.AvailableExtras.Select(i => i.ExtraId);
            var extras = new List<Extra>();
            foreach (var extraId in availableExtras)
            {
                var extra = ctx.Extras.Where(e => e.Active == true).FirstOrDefault(e => e.Id == extraId);
                if (extra != null)
                {
                    extras.Add(extra);
                }
            }
            return extras;
        }
    }
}
