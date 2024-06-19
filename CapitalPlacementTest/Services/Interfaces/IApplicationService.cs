using CapitalPlacementTest.Requests;
using CapitalPlacementTest.Responses;

namespace CapitalPlacementTest.Services
{
    public interface IApplicationService
    {
        Task<ApiResponse<string>> Apply(ApplicationDto application);
    }
}
