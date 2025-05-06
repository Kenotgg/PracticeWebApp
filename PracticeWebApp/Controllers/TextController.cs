using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
        public TextController(ITextService textService, IHttpClientFactory clientFactory, ILogger<TextController> logger) 
        {
            _textService = textService;
            
        } 

        [HttpGet("send")]
        public async Task<IActionResult> ReturnProcessedString(string word, string? sortAlgorithm)
        {
            (bool isValid, string errorMessage) = _textService.IsWordCorrect(word);
            if (!isValid) 
            {
                return BadRequest(errorMessage);
            }
            string processedWord = await _textService.ReturnProcessedString(word, sortAlgorithm);
            return Ok(processedWord);
        }

        //[HttpGet("getRandomNumber")]
        //public async Task<IActionResult> GetRandomNumber() 
        //{
            
        //}
    }  
}
