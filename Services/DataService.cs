using Azure.Data.Tables;
using Azure;

namespace Company.Function.Services
{
    public class DataService : IDataService
    {
        private readonly TableClient _tableClient;

        public DataService(
            TableClient tableClient)
        {
            _tableClient = tableClient;
        }
        public Pageable<TableEntity> GetProjects()
        {
            return  _tableClient.Query<TableEntity>(
                ent => ent.PartitionKey == System.Environment.GetEnvironmentVariable("STORAGE_PROJECT_PARTITION")
            );
        }
        public Pageable<TableEntity> GetEducations()
        {
            return  _tableClient.Query<TableEntity>(
                ent => ent.PartitionKey == System.Environment.GetEnvironmentVariable("STORAGE_EDUCATION_PARTITION")
            );
        }
        public Pageable<TableEntity> GetExperiences()
        {
            return  _tableClient.Query<TableEntity>(
                ent => ent.PartitionKey == System.Environment.GetEnvironmentVariable("STORAGE_EXPERIENCE_PARTITION")
            );
        }
        public TableClient GetTableClient()
        {
            return  _tableClient;
        }
    }
}