using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeWebApp.Services;
using PracticeWebApp.Services.Interfaces;
using System.Text;

namespace PracticeWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {
        ITextService _textService;
        public TextController(ITextService textService) 
        {
            _textService = textService;
        }

        [HttpGet("send")]
        public async Task<IActionResult> ReturnProcessedString(string word)
        {
            (bool isValid, string errorMessage) = _textService.IsWordCorrect(word);
            if (!isValid) 
            {
                return BadRequest(errorMessage);
            }
            var processedWord = await _textService.ReturnProcessedString(word);
            return Ok(processedWord);
        }

       
    }
}
