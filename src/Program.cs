using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using oauth2testbed.Database;
using System;

namespace oauth2testbed
{
    public class Program
    {
        /// <summary>
        /// Init all the things..
        /// </summary>
        public static void Main(string[] args)
        {
            // Create the database and table.
            try
            {
                DatabaseContext.CreateTables();
                Console.WriteLine($"StoragePath: {DatabaseContext.StoragePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("CRITICAL ERROR");
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                Console.WriteLine("ABORTING/EXITING THE APPLICATION!");

                return;
            }

            // Init the host.
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(l =>
                {
                    l.ClearProviders();
                    l.AddConsole();
                })
                .ConfigureWebHostDefaults(b =>
                {
                    b.UseStartup<Startup>();
                })
                .Build()
                .Run();
        }
    }
}