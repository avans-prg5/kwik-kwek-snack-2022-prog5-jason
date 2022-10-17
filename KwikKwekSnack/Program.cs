using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KwikKwekSnack
{
    /*Bestelsysteem
     * Bestelling kiezen
     * vervolgens aan balie betalen en afhalen.
     * stappen:
     * 1. Nieuwe bestelling aanmaken
     * 2. Kiezen; één of meer losse snacks
     * 3. Kiezen drankjes
     * 4. Afronden bestelling
     * 4a. tonen overzicht
     * 4b. tonen kosten
     * 4c. tonen afhaalnummer
     * 
     * Eigenaar: onderhouden informatie beschikbaar eten en drinken
     * 
     * medewerkers en klanten kunnen zien waar hun bestelling in de wachtrij staat, 
     * welke order er nu onder handen is 
     * en wat de meest recente orders zijn die afgehaald kunnen worden.
     * 
     * 28 minuten
     */

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
