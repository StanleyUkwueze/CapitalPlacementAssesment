using AutoMapper;
using Azure;
using CapitalPlacementTest.Common;
using CapitalPlacementTest.Models;
using CapitalPlacementTest.Requests;
using CapitalPlacementTest.Responses;
using CapitalPlacementTest.Services;
using Microsoft.Azure.Cosmos;

namespace CosmosDb.Services
{
    public class QuestionService(IConfiguration configuration, IMapper mapper) : IQuestionService
    {
        private readonly IConfiguration configuration = configuration;
        private readonly IMapper mapper = mapper;

        private readonly string CosmosDBAccountUri = configuration.GetSection("CosmosDbConfiguration:CosmosDBAccountUri").Value!;
        private readonly string CosmosDBAccountPrimaryKey = configuration.GetSection("CosmosDbConfiguration:CosmosDBAccountPrimaryKey").Value!;
        private readonly string CosmosDbName = configuration.GetSection("CosmosDbConfiguration:CosmosDbName").Value!;
        private readonly string CosmosDbContainerName = configuration.GetSection("CosmosDbConfiguration:QuestionsContainer").Value!;
        private const string query = $"SELECT * FROM c WHERE c.Type = @type";

        public async Task<ApiResponse<QuestionResponse>> CreateQuestionAsync(CreateQuestionDto questionDto)
        {
            if (string.IsNullOrEmpty(QuestionType.ValidateQuestionType(questionDto.Type)))
            {
                return new ApiResponse<QuestionResponse>
                {
                    Message = $"Kindly supply a valid question type: {string.Join("," , QuestionType.questionTypes.ToArray())}",
                    Success = false
                };
            }

            if (!QuestionType.ValidateQuestionType(questionDto.Choice!, questionDto.Type)) return new ApiResponse<QuestionResponse>
            {
                Message = "Only MultipleChoice and DropDown question types can have choices",
                Success = false
            };

            var container = GetContainerClient();
            var result = new QuestionResponse();

            questionDto.Type = QuestionType.ValidateQuestionType(questionDto.Type);
            var response = await container.CreateItemAsync(questionDto, new PartitionKey(questionDto.Type));
            result = mapper.Map<QuestionResponse>(response.Resource);

            return new ApiResponse<QuestionResponse>
            {
                Message = "Question successully created",
                Success = true,
                Data = result
            };
        }

        public async Task<ApiResponse<QuestionResponse>> EditQuestion(EditQuestionDto editQuestionDto, string id)
        {
           var container = GetContainerClient();
           var response = await container.ReadItemAsync<ApplicationQuestion>(id, new PartitionKey(editQuestionDto.Type));
            var result = new QuestionResponse();

            if (response.Resource is null)
            {
                return new ApiResponse<QuestionResponse>
                {
                    Message = $"No Question record with the provided id: {id} found",
                    Success = false
                };
            }
           
            var existingItem = response.Resource;
            existingItem = mapper.Map<ApplicationQuestion>(editQuestionDto);
       
            var updateResponse = await container.ReplaceItemAsync(existingItem, id, new PartitionKey(editQuestionDto.Type));
            var IsUpdated = updateResponse.StatusCode == System.Net.HttpStatusCode.OK;

            result = mapper.Map<QuestionResponse>(updateResponse.Resource!);
            return new ApiResponse<QuestionResponse>
            {
                Message = IsUpdated ? "Question updated successfully" : "Oops! something went wrong and the update failed",
                Success = IsUpdated ? true : false,
                Data = IsUpdated ? result : null
            };
        }

        public async Task<ApiResponse<List<QuestionResponse>>> GetQuestionByType(string type)
        {
            var container = GetContainerClient();
            var queryDefinition = new QueryDefinition(query).WithParameter("@type", type);

            var queryIterator = container.GetItemQueryIterator<ApplicationQuestion>(queryDefinition);
            if (!queryIterator.HasMoreResults)
            {
                return new ApiResponse<List<QuestionResponse>>
                {
                    Message = $"No record of the provided type: {type} found",
                    Success = false
                };
            }

            var results = new List<ApplicationQuestion>();
            while (queryIterator.HasMoreResults)
            {
                var response = await queryIterator.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            var hasRecords = results.Count > 0;
            var recordsToReturn = mapper.Map<List<QuestionResponse>>(results);

            return new ApiResponse<List<QuestionResponse>>
            {
                Success = hasRecords ? true : false,
                Message = hasRecords ? "Question fetched succesfully" : "No record found",
                Data = recordsToReturn
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
