namespace CapitaPlacementUnitTest
{
    using AutoMapper;
    using CapitalPlacementTest.Requests;
    using CapitalPlacementTest.Responses;
    using CosmosDb.Services;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using Xunit;

    public class QuestionServiceTests
    {
        private  readonly IConfiguration _configuration;
        public QuestionServiceTests()
        {
            _configuration = TestConfigurationProvider.GetConfiguration();
        }
        [Fact]
        public async Task CreateQuestionAsync_ValidQuestionType_ReturnsSuccessResponse()
        {
            
        // Arrange
        var questionDto = new CreateQuestionDto { id = Guid.NewGuid().ToString(), Type = "MultipleChoice", Description = "A sample question", Choice = new List<string> { "Option1","Option2" } };


            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<QuestionResponse>(It.IsAny<object>()))
                      .Returns(new QuestionResponse());

            var questionService = new QuestionService(_configuration, mockMapper.Object);

            // Act
            var response = await questionService.CreateQuestionAsync(questionDto);

            // Assert
            Assert.True(response.Success);
            Assert.Equal("Question successfully created", response.Message);
            Assert.NotNull(response.Data);
        }

        [Fact]
        public async Task CreateQuestionAsync_InvalidQuestionType_ReturnsErrorResponse()
        {
            // Arrange
            var questionDto = new CreateQuestionDto { Type = "InvalidType", Choice = new List<string> { "Option1", "Option2" } };

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<QuestionResponse>(It.IsAny<object>()))
                      .Returns(new QuestionResponse());

            var questionService = new QuestionService(_configuration, mockMapper.Object);

            // Act
            var response = await questionService.CreateQuestionAsync(questionDto);

            // Assert
            Assert.False(response.Success);
            Assert.Contains("Kindly supply a valid question type", response.Message);
            Assert.Null(response.Data);
        }

        [Fact]
        public async Task CreateQuestionAsync_InvalidChoiceForQuestionType_ReturnsErrorResponse()
        {
            // Arrange
            var questionDto = new CreateQuestionDto { Type = "YesNo", Choice = new List<string> { "Option1", "Option2" } };

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<QuestionResponse>(It.IsAny<object>()))
                      .Returns(new QuestionResponse());

            var questionService = new QuestionService(_configuration, mockMapper.Object);

            // Act
            var response = await questionService.CreateQuestionAsync(questionDto);

            // Assert
            Assert.False(response.Success);
            Assert.Equal("Only MultipleChoice and DropDown question types can have choices", response.Message);
            Assert.Null(response.Data);
        }
    }

}