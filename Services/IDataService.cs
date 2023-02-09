using Azure;
using Azure.Data.Tables;

namespace Company.Function.Services
{
    public interface IDataService
    {
        /// <summary>
        /// Get all projects from the Projects collection
        /// </summary>
        /// <returns>
        /// List of type Project
        /// </returns>
        Pageable<TableEntity> GetProjects();

        /// <summary>
        /// Get all projects from the Projects collection
        /// </summary>
        /// <returns>
        /// List of type Education
        /// </returns>
        Pageable<TableEntity> GetEducations();

        /// <summary>
        /// Get all projects from the Projects collection
        /// </summary>
        /// <returns>
        /// List of type Experience
        /// </returns>
        Pageable<TableEntity> GetExperiences();

        /// <summary>
        /// Enables content uploads
        /// </summary>
        /// <returns>
        /// TableEntity 
        /// </returns>
        TableClient GetTableClient();
    }
}