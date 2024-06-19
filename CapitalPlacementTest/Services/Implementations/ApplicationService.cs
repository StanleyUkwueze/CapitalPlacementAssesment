using CapitalPlacementTest.Models;
using CapitalPlacementTest.Requests;
using CapitalPlacementTest.Responses;
using CapitalPlacementTest.Services;
using Microsoft.Azure.Cosmos;

namespace CosmosDb.Services
{
    public class ApplicationService(IConfiguration configuration) : IApplicationService
    {
        private readonly IConfiguration configuration = configuration;

        private readonly string CosmosDBAccountUri = configuration.GetSection("CosmosDbConfiguration:CosmosDBAccountUri").Value!;
        private readonly string CosmosDBAccountPrimaryKey = configuration.GetSection("CosmosDbConfiguration:CosmosDBAccountPrimaryKey").Value!;
        private readonly string CosmosDbName = configuration.GetSection("CosmosDbConfiguration:CosmosDbName").Value!;
        private readonly string CosmosDbContainerName = configuration.GetSection("CosmosDbConfiguration:ApplicationContainer").Value!; 

        public async Task<ApiResponse<string>> Apply(ApplicationDto application)
        {
            var container = GetContainerClient();

            var applicationRecord = new Application
            {
                FirstName = application.FirstName,
                LastName = application.LastName,
                Email = application.Email,
                Nationality = application.Nationality,
                Phone = application.Phone,
                DateMovedToUK = application.DateMovedToUK,
                DateOfBirth = application.DateOfBirth,
                IdNumber = application.IdNumber,
                CurrentResidence = application.CurrentResidence,
                MultipleChoiceAnswer = application.MultipleChoiceAnswer,
                UKEmbassyRejectionStatus =application.UKEmbassyRejectionStatus,
                YearOfGraduation = application.YearOfGraduation,
                AboutMe = application.AboutMe,
                Gender = application.Gender,
            };

            var response = await container.CreateItemAsync(applicationRecord, new PartitionKey(applicationRecord.Email));

            return new ApiResponse<string>
            {
                Message = "Success",
                Success = true,
                Data = $"Hi {application.FirstName}, Congratulations"
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
