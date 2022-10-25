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
        public DbSet<DrinkOrder> DrinkOrders { get; set; }
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
            FillSeedData(modelBuilder);
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
            FillExtraSeedData(modelBuilder);
            FillDrinkSizeSeedData(modelBuilder);
            FillSnackSeedData(modelBuilder);
            FillDrinkSeedData(modelBuilder);
            AddExtraSeedDataToDrinksAndSnacks(modelBuilder);
        }

        private void FillExtraSeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Extra>().HasData( 
                new Extra { Id = 1, Name = "IJsklontjes", Price = 0.10},
                new Extra { Id = 2, Name = "Rietje", Price = 0.15},
                new Extra { Id = 3, Name = "Slagroom", Price = 0.20},
                new Extra { Id = 4, Name = "Sla", Price = 0.25},
                new Extra { Id = 5, Name = "Mayonnaise", Price = 0.25}
            );
        }

        private void FillDrinkSizeSeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DrinkSize>().HasData(
                new DrinkSize {  Id = 1, ShortName = "S", FullName = "Small", PriceMultiplier = 1.00},
                new DrinkSize { Id = 2, ShortName = "M", FullName = "Medium", PriceMultiplier = 1.25 },
                new DrinkSize { Id = 3, ShortName = "L", FullName = "Large", PriceMultiplier = 1.50 }
            );
        }
        private void FillSnackSeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Snack>().HasData(
                new Snack { Id = 1, Name = "Beef Burger", Description = "Rundvlees hamburger", StandardPrice = 2.50, ImageURL = "https://imgs.search.brave.com/DyyLM6KzO1StGQnV_w3sPjLgSZyelGWt7GQcDkUDXqA/rs:fit:612:539:1/g:ce/aHR0cHM6Ly9tZWRp/YS5pc3RvY2twaG90/by5jb20vcGhvdG9z/L2ZyZXNoLWJ1cmdl/ci1pc29sYXRlZC1w/aWN0dXJlLWlkMTEy/NTE0OTE4Mz9rPTYm/bT0xMTI1MTQ5MTgz/JnM9NjEyeDYxMiZ3/PTAmaD1LeFNmVlVr/M0tQM0JnSFZZYm95/TDlhUkxIcC1mUlly/ZlBjRmVhMHc2OE93/PQ" },
                new Snack { Id = 2, Name = "Friet", Description = "Gefrituurde aardappelen", StandardPrice = 1.50 },
                new Snack { Id = 3, Name = "Frikandel", Description = "Gefrituurde vleesrol", StandardPrice = 1.00, ImageURL = "https://imgs.search.brave.com/zjRHgiREOLZtvFh-TiPRqiUPLR_1JGBTiErIRUY89UI/rs:fit:800:568:1/g:ce/aHR0cHM6Ly90aHVt/YnMuZHJlYW1zdGlt/ZS5jb20vYi9mcmlr/YW5kZWwtdW0tcGV0/aXNjby1ob2xhbmQl/QzMlQUFzLXRyYWRp/Y2lvbmFsLW1laW8t/Y2FjaG9ycm8tcXVl/bnRlLXRyaXR1cmFk/by1kYS1jYXJuZS1w/ciVDMyVCM3hpbW8t/YWNpbWEtZG8tMTUy/MTc2MzMwLmpwZw" },
                new Snack { Id = 4, Name = "Pizza Salami", Description = "Pizza met plakjes salami", StandardPrice = 5.00, ImageURL = "http://www.clker.com/cliparts/3/9/1/d/1451508004467611065wallpaper-sliced-pizza.jpg" }
             );
        }

        private void FillDrinkSeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drink>().HasData(
                new Drink { Id = 1, Name = "Cola", Description = "Bruine frisdrank met koolzuur", MinimalPrice = 2.50, ImageURL = "https://imgs.search.brave.com/s1tlzTSdN6odOOFO1fLwEPOUyq4gnuw6DfxckSH0ylM/rs:fit:1000:1000:1/g:ce/aHR0cHM6Ly9henRl/Y21leGljYW5wcm9k/dWN0c2FuZGxpcXVv/ci5jb20uYXUvd3At/Y29udGVudC91cGxv/YWRzLzIwMjAvMDUv/UmVkLUNvbGEtY2Fu/cy1henRlYy1tZXhp/Y2FuLmpwZw" },
                new Drink { Id = 2, Name = "Spa Blauw", Description = "Water zonder koolzuur", MinimalPrice = 1.50 },
                new Drink { Id = 3, Name = "Spa Rood", Description = "Water met koolzuur", MinimalPrice = 1.50},
                new Drink { Id = 4, Name = "Chocomel", Description = "Chocolademelk", MinimalPrice = 3.00}
             );
        }

        private void AddExtraSeedDataToDrinksAndSnacks(ModelBuilder modelBuilder)
        {
            AddExtraSeedDataToDrinks(modelBuilder);
            AddExtraSeedDataToSnacks(modelBuilder);
        }

        private void AddExtraSeedDataToDrinks(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DrinkExtra>().HasData(
                new DrinkExtra { DrinkId = 1, ExtraId = 1 },
                new DrinkExtra { DrinkId = 1, ExtraId = 2 },
                new DrinkExtra { DrinkId = 2, ExtraId = 1 },
                new DrinkExtra { DrinkId = 3, ExtraId = 1 },
                new DrinkExtra { DrinkId = 4, ExtraId = 3 }
            );
        }

        private void AddExtraSeedDataToSnacks(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SnackExtra>().HasData(
                new SnackExtra { SnackId = 1, ExtraId = 4 },
                new SnackExtra { SnackId = 1, ExtraId = 5 },
                new SnackExtra { SnackId = 2, ExtraId = 5 },
                new SnackExtra { SnackId = 3, ExtraId = 5 }
            );
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
