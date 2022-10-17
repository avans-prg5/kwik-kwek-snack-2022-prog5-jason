using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace KwikKwekSnack.Domain
{
    public class KwikKwekSnackContext : DbContext
    {

        public KwikKwekSnackContext(DbContextOptions<KwikKwekSnackContext> options) : base(options)
        {

        }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<DrinkExtra> DrinkExtras { get; set; }
        public DbSet<Drink> DrinkOrders { get; set; }
        public DbSet<DrinkOrderExtra> DrinkOrderExtras { get; set; }        
        public DbSet<DrinkSize> DrinkSizes { get; set; }
        public DbSet<Extra> Extras { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Snack> Snacks { get; set; }
        public DbSet<SnackExtra> SnackExtras { get; set; }
        public DbSet<SnackOrderExtra> SnackOrderExtras { get; set; }
        public DbSet<SnackOrder> SnackOrders { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=KwikKwekSnack;Trusted_Connection=True;");
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CreateManyToManyRelationships(modelBuilder);
            //FillSeedData(modelBuilder);
        }        

        private void CreateManyToManyRelationships(ModelBuilder modelBuilder)
        {
            CreateDrinkExtraRelationship(modelBuilder);
            CreateSnackExtraRelationship(modelBuilder);
            CreateDrinkOrderExtraRelationship(modelBuilder);
            CreateSnackOrderExtraRelationship(modelBuilder);            
        }

        private void FillSeedData(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }

        private void CreateDrinkExtraRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DrinkExtra>()
                .HasKey(t => new { t.ExtraId, t.DrinkId });

            modelBuilder.Entity<DrinkExtra>()
                .HasOne(pt => pt.Drink)
                .WithMany(p => p.AvailableExtras)
                .HasForeignKey(pt => pt.DrinkId);

            modelBuilder.Entity<DrinkExtra>()
                .HasOne(pt => pt.Extra)
                .WithMany(p => p.ExtraOfDrink)
                .HasForeignKey(pt => pt.ExtraId);
        }
        private void CreateSnackExtraRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SnackExtra>()
                .HasKey(t => new { t.ExtraId, t.SnackId });

            modelBuilder.Entity<SnackExtra>()
                .HasOne(pt => pt.Snack)
                .WithMany(p => p.AvailableExtras)
                .HasForeignKey(pt => pt.SnackId);

            modelBuilder.Entity<SnackExtra>()
                .HasOne(pt => pt.Extra)
                .WithMany(p => p.ExtraOfSnack)
                .HasForeignKey(pt => pt.ExtraId);
        }
        private void CreateDrinkOrderExtraRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DrinkOrderExtra>()
                .HasKey(t => new { t.ExtraId, t.DrinkOrderId });

            modelBuilder.Entity<DrinkOrderExtra>()
                .HasOne(pt => pt.DrinkOrder)
                .WithMany(p => p.ChosenExtras)
                .HasForeignKey(pt => pt.DrinkOrderId);

            modelBuilder.Entity<DrinkOrderExtra>()
                .HasOne(pt => pt.Extra)
                .WithMany(p => p.ExtraOfDrinkOrder)
                .HasForeignKey(pt => pt.ExtraId);
        }
        private void CreateSnackOrderExtraRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SnackOrderExtra>()
                .HasKey(t => new { t.ExtraId, t.SnackOrderId });

            modelBuilder.Entity<SnackOrderExtra>()
                .HasOne(pt => pt.SnackOrder)
                .WithMany(p => p.ChosenExtras)
                .HasForeignKey(pt => pt.SnackOrderId);

            modelBuilder.Entity<SnackOrderExtra>()
                .HasOne(pt => pt.Extra)
                .WithMany(p => p.ExtraOfSnackOrder)
                .HasForeignKey(pt => pt.ExtraId);
        }      
    }
}
