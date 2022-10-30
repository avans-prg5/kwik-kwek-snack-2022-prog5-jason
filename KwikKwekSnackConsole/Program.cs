using KwikKwekSnack.Domain;
using KwikKwekSnack.Domain.Repositories;
using KwikKwekSnackConsole.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KwikKwekSnackConsole
{
    public class Program
    {        
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            
            try
            {
                StartApp(config);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Fout bij het opstarten van de applicatie");
                Console.WriteLine(ex.Message);
            }
                                 
                     
        }
        
        private static KwikKwekSnackContext CreateContext(string connectionString)
        {            
            DbContextOptionsBuilder options = new DbContextOptionsBuilder<KwikKwekSnackContext>();
            options.UseSqlServer(connectionString ?? "");
            KwikKwekSnackContext ctx = new KwikKwekSnackContext(options.Options);
            return ctx;
        }

        private static void StartApp(IConfigurationRoot config)
        {
            string connectionString = config.GetConnectionString("KwikKwekSnackSolution");
            var ctx = CreateContext(connectionString);
            OrderApp app = new OrderApp(new OrderRepoSql(ctx), config);
            app.Run();
        }
        
    }
}

