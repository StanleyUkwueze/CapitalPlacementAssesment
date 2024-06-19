

using CapitalPlacementTest.Requests;
using CapitalPlacementTest.Responses;

namespace CapitalPlacementTest.Services
{
    public interface IQuestionService
    {
        Task<ApiResponse<QuestionResponse>> CreateQuestionAsync(CreateQuestionDto questionDto);
        Task<ApiResponse<QuestionResponse>> EditQuestion(EditQuestionDto editQuestionDto, string id);
        Task<ApiResponse<List<QuestionResponse>>> GetQuestionByType(string type);
    }
}
