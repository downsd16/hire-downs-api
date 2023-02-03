using Azure;
using System;
using System.Linq;
using Newtonsoft.Json;
using Azure.Data.Tables;
using Company.Function.Model;
using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Mvc;
using Company.Function.Services;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace MyFunctionApp
{
    public class GetProjects
    {
        private readonly IDataService _dataService;

        public GetProjects(
            IDataService dataService) 
        {
            _dataService = dataService;
        }

        [FunctionName("GetProjects")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            Microsoft.AspNetCore.Http.HttpRequest req)
        {
            Pageable<TableEntity> projectTableResults = _dataService.GetProjects();
            List<Project> projectResult = new List<Project>();

            foreach (TableEntity qProjectEntity in projectTableResults)
            {
                projectResult.Add(new Project(
                    Int32.Parse(qProjectEntity.GetString("RowKey")), 
                    qProjectEntity.GetString("Name"),
                    qProjectEntity.GetString("ImageURL"),
                    qProjectEntity.GetString("Description"),
                    qProjectEntity.GetString("Repository"),
                    qProjectEntity.GetString("Skills").Split(','), 
                    qProjectEntity.GetString("Tags").Split(',')
                ));
            }

            return new OkObjectResult(JsonConvert.SerializeObject(
                projectResult.OrderBy(ent => ent.RowKey).ToList()
            ));
        }
    }
}
