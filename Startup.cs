using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Company.Function.GetContent;
using Company.Function.Services;
using Azure.Data.Tables;
using System;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Company.Function.GetContent
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton((s) => new TableClient(
                new Uri(System.Environment.GetEnvironmentVariable("STORAGE_URI")),
                System.Environment.GetEnvironmentVariable("TABLE_NAME"),
                new TableSharedKeyCredential(
                    System.Environment.GetEnvironmentVariable("STORAGE_ACCOUNT_NAME"),
                    System.Environment.GetEnvironmentVariable("STORAGE_ACCOUNT_KEY")
            )));

            builder.Services.AddTransient<IDataService, DataService>();
        }
    }
}
