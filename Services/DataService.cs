using Microsoft.Extensions.Configuration;
using Azure.Data.Tables;
using Azure;

namespace Company.Function.Services
{
    public class DataService : IDataService
    {
        private readonly TableClient _tableClient;
        private readonly IConfiguration _configuration;

        public DataService(
            TableClient tableClient,
            IConfiguration configuration)
        {
            _tableClient = tableClient;
            _configuration = configuration;
        }
        public Pageable<TableEntity> GetProjects()
        {
            return  _tableClient.Query<TableEntity>(
                ent => ent.PartitionKey == _configuration["STORAGE_PROJECT_PARTITION"]
            );
        }
        public Pageable<TableEntity> GetEducations()
        {
            return  _tableClient.Query<TableEntity>(
                ent => ent.PartitionKey == _configuration["STORAGE_EDUCATION_PARTITION"]
            );
        }
        public Pageable<TableEntity> GetExperiences()
        {
            return  _tableClient.Query<TableEntity>(
                ent => ent.PartitionKey == _configuration["STORAGE_EXPERIENCE_PARTITION"]
            );
        }
    }
}