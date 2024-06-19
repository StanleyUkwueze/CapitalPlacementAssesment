using AutoMapper;
using CapitalPlacementTest.Models;
using CapitalPlacementTest.Requests;
using CapitalPlacementTest.Responses;
using CapitalPlacementTest.Services;
using Microsoft.Azure.Cosmos;

namespace CosmosDb.Services
{
    public class ApplicationService(IConfiguration configuration, IMapper mapper) : IApplicationService
    {
        private readonly IConfiguration configuration = configuration;
        private readonly IMapper mapper = mapper;
        private readonly string CosmosDBAccountUri = configuration.GetSection("CosmosDbConfiguration:CosmosDBAccountUri").Value!;
        private readonly string CosmosDBAccountPrimaryKey = configuration.GetSection("CosmosDbConfiguration:CosmosDBAccountPrimaryKey").Value!;
        private readonly string CosmosDbName = configuration.GetSection("CosmosDbConfiguration:CosmosDbName").Value!;
        private readonly string CosmosDbContainerName = configuration.GetSection("CosmosDbConfiguration:ApplicationContainer").Value!; 

        public async Task<ApiResponse<string>> Apply(ApplicationDto application)
        {
            var container = GetContainerClient();

           var applicationRecord = mapper.Map<Application>(application);
            var response = await container.CreateItemAsync(applicationRecord, new PartitionKey(applicationRecord.Email));
            if (response.StatusCode is not System.Net.HttpStatusCode.Created) return new ApiResponse<string>
            {
                Message = "Appologies, something went wrong and your application attempt has failed, Kindly try again",
                Success = false,
                Data = "Application attempt failed"
            };

            return new ApiResponse<string>
            {
                Message = $"Hi {application.FirstName}, Congratulations, You have successfully applied for this Program",
                Success = true,
                Data = "Application successful"
            };
        }

        private Container GetContainerClient()
        {
            var client = new CosmosClient(CosmosDBAccountUri, CosmosDBAccountPrimaryKey);
            var database = client.GetDatabase(CosmosDbName);
            var container = database.GetContainer(CosmosDbContainerName);
            return container;
        }
    }
}
