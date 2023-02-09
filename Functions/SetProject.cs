using Azure;
using System.Linq;
using Newtonsoft.Json;
using Azure.Data.Tables;
using Company.Function.Model;
using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Mvc;
using Company.Function.Services;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Threading.Tasks;
using System.IO;
using System;

namespace MyFunctionApp
{
    public class AddProjects
    {
        private readonly IDataService _dataService;

        public AddProjects(
            IDataService dataService) 
        {
            _dataService = dataService;
        }

        [FunctionName("projects")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            Microsoft.AspNetCore.Http.HttpRequest req)
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();
            Project newProjectData = JsonConvert.DeserializeObject<Project>(content);
            TableClient tableClient = _dataService.GetTableClient();

            TableEntity newProjectEntity = new TableEntity("project", Guid.NewGuid().ToString("N"))
            {
                {"Name",newProjectData.Name},
                {"ImageURL",newProjectData.ImageHREF},
                {"Description",newProjectData.Description},
                {"Repository",newProjectData.Repository},
                {"Skills",newProjectData.Skills},
                {"Tags",newProjectData.Tags}
            };

            await tableClient.AddEntityAsync(newProjectEntity);

            /*
                TO DO:
                1.  Read AddEntityAsyn Task<Response> when completed 
                2.  Create return response(boolean) 
            */

            return new OkObjectResult(JsonConvert.SerializeObject(
                result.OrderBy(ent => ent.RowKey).ToList()
            ));
        }
    }
}
