using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Azure.Data.Tables;
using System;
using Company.Function.Services;
using Company.Function.GetContent;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Company.Function.GetContent
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            
            builder.Services.AddSingleton((s) => new TableClient(
                new Uri(config["STORAGE_URI"]),
                config["TABLE_NAME"],
                new TableSharedKeyCredential(
                    config["STORAGE_ACCOUNT_NAME"],
                    config["STORAGE_ACCOUNT_KEY"]
            )));

            builder.Services.AddSingleton<IConfiguration>(config);
            builder.Services.AddTransient<IDataService, DataService>();
        }
    }
}
