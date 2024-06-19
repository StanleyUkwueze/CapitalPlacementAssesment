using CapitalPlacementTest.Requests;
using CapitalPlacementTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacementTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController(IQuestionService questionService) : ControllerBase
    {
        private readonly IQuestionService questionService = questionService;


        [HttpPost("create")]
        public async Task<IActionResult> AddQuestion(CreateQuestionDto questionDto)
        {
            var result = await questionService.CreateQuestionAsync(questionDto);
            if (result.Data is null || !result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditQuestion(EditQuestionDto questionDto, string id)
        {
            var result = await questionService.EditQuestion(questionDto, id);
            if (result.Data is null || !result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("get-by-type")]
        public async Task<IActionResult> GetQuestionByType(string type)
        {
            var result = await questionService.GetQuestionByType(type);
            if (result.Data?.Count < 0 || !result.Success) return NotFound(result);

            return Ok(result);
        }
    }
}
