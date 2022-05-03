using Microsoft.Extensions.Configuration;
using System.IO;

namespace Infrastructure
{
    public static class ConfigurationHandler
    {
        private const string DEV_CS_PREFIX = "Dev";
        private const string PROD_CS_PREFIX = "Prod";

        public static IConfiguration Get()
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Directory where the json files are located
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
            return configuration;
        }

        public static string GetConnectionString()
        {
            var configuration = Get();
            string cs = configuration.GetConnectionString(DEV_CS_PREFIX);
#if !DEBUG
            cs = configuration.GetConnectionString(PROD_CS_PREFIX);
#endif
            return cs;
        }
    }
}
