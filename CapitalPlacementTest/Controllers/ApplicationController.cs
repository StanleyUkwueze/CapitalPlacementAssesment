using CapitalPlacementTest.Requests;
using CapitalPlacementTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacementTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController(IApplicationService applicationService) : ControllerBase
    {
        private readonly IApplicationService applicationService = applicationService;

        [HttpPost("apply")]
        public async Task<IActionResult> AddQuestion(ApplicationDto application)
        {
            var result = await applicationService.Apply(application);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}
