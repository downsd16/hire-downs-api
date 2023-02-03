using Azure;
using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Azure.Data.Tables;
using Company.Function.Model;
using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace MyFunctionApp
{
    public static class GetProjects
    {
        [FunctionName("GetProjects")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var tableClient = new TableClient(
                new Uri(config["STORAGE_URI"]),
                config["TABLE_NAME"],
                new TableSharedKeyCredential(
                    config["STORAGE_ACCOUNT_NAME"],
                    config["STORAGE_ACCOUNT_KEY"]
                ));

            //Make query for 'projects' partition
            Pageable<TableEntity> projectTableResults = tableClient
                .Query<TableEntity>(
                    ent => ent.PartitionKey == config["STORAGE_PROJECT_PARTITION"]
                );

            //Create Variables for Projects
            List<Project> projectResult = new List<Project>();
            int rowKey = -1;

            //Iterate over Table and assign parameters for Projects
            foreach (TableEntity qProjectEntity in projectTableResults)
            {
                //Converting string RowKey to int
                try{
                    rowKey = Int32.Parse(qProjectEntity.GetString("RowKey"));
                } catch (Exception e) {
                    Console.WriteLine(e);
                    return new BadRequestObjectResult("Error Converting RowKey (string) to RowKey (int)");
                }

                //Create new Project objects
                projectResult.Add(new Project(
                    rowKey, 
                    qProjectEntity.GetString("Name"),
                    qProjectEntity.GetString("ImageURL"),
                    qProjectEntity.GetString("Description"),
                    qProjectEntity.GetString("Repository"),
                    qProjectEntity.GetString("Skills").Split(','), 
                    qProjectEntity.GetString("Tags").Split(',')
                ));
            }

            // Serialize the results to a JSON string
            string jsonProj = JsonConvert.SerializeObject(projectResult.OrderBy(ent => ent.RowKey).ToList());

            // Return the JSON string as the response
            return new OkObjectResult(jsonProj);
        }
    }
}
