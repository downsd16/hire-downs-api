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
    public class GetExperiences
    {
        private readonly IDataService _dataService;

        public GetExperiences(
            IDataService dataService) 
        {
            _dataService = dataService;
        }

        [FunctionName("experiences")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            Microsoft.AspNetCore.Http.HttpRequest req)
        {
            Pageable<TableEntity> tableResults = _dataService.GetExperiences();
            List<Experience> result = new List<Experience>();

            foreach (TableEntity qEntity in tableResults)
            {
                result.Add(new Experience(
                    Int32.Parse(qEntity.GetString("RowKey")), 
                    qEntity.GetString("Name"),
                    qEntity.GetString("ImageURL"),
                    qEntity.GetString("Institution"),
                    qEntity.GetString("Description"),
                    qEntity.GetString("StartDate"),
                    qEntity.GetString("EndDate")
                ));
            }

            return new OkObjectResult(JsonConvert.SerializeObject(
                result.OrderBy(ent => ent.RowKey).ToList()
            ));
        }
    }
}
