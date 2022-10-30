using KwikKwekSnack.Domain;
using KwikKwekSnack.Domain.Repositories;
using KwikKwekSnackConsole.Controllers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KwikKwekSnackConsole
{
    public class Program
    {
        static string connectionString = "Server=.;Database=KwikKwekSnack;Trusted_Connection=True;";
        
        public static void Main(string[] args)
        {
            //SqlConnection connection = new SqlConnection(connectionString);
            //CreateConnectionToDb(connection);
            //CreateServices();
            DbContextOptionsBuilder options = new DbContextOptionsBuilder<KwikKwekSnackContext>();
            options.UseSqlServer(connectionString);
            KwikKwekSnackContext ctx = new KwikKwekSnackContext(options.Options);            
            OrderApp app = new OrderApp(new OrderRepoSql(ctx)); 
            app.Run();
            //connection.Close();
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true);
                
        }

        private static bool CreateConnectionToDb(SqlConnection connection)
        {
            Console.WriteLine("Connectie met server aan het maken...");           
            try
            {
                connection.Open();
                Console.WriteLine("Verbonden met server");
                return true;                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }            
        }

        private static bool CreateServices()
        {
            try
            {
                IServiceCollection services = new ServiceCollection();
                services.AddDbContext<KwikKwekSnackContext>(options => options.UseSqlServer(connectionString));
                services.AddScoped<IOrderRepo, OrderRepoSql>();                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
    }
}

