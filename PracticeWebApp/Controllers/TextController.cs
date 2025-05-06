using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracticeWebApp.Services;
using PracticeWebApp.Services.Interfaces;
using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.Extensions.Logging;

namespace PracticeWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {
        
        private readonly ITextService _textService;
        private readonly IAppSettings _appSettings;
        private readonly ILogger<TextController> _logger;
        private static SemaphoreSlim _semaphore;
        private readonly bool _useSemaphore;
        public TextController(ITextService textService, IAppSettings appSettings, ILogger<TextController> logger) 
        {
            _textService = textService;
            _appSettings = appSettings;
            _logger = logger;
            _useSemaphore = _appSettings.ParallelLimit > 0; // Set the flag
            if (_useSemaphore && _semaphore == null) 
            {
                _semaphore = new SemaphoreSlim(_appSettings.ParallelLimit, _appSettings.ParallelLimit);
            }
        }

        [HttpGet("send")]
        public async Task<IActionResult> ReturnProcessedString(string word, string? sortAlgorithm)
        {
            if (_useSemaphore)
            {
                try
                {
                    bool acquired = await _semaphore.WaitAsync(0);
                    if (!acquired)
                    {
                        _logger.LogWarning("Service Unavailable: Maximum number of requests reached.");
                        return StatusCode(503, "Service Unavailable: Maximum number of requests reached."); // Возвращаем HTTP 503
                    }
                    try
                    {
                        (bool isValid, string errorMessage) = _textService.IsWordCorrect(word);
                        if (!isValid)
                        {
                            return BadRequest(new { message = "HTTP ошибка 400 Bad Request: " + errorMessage });
                        }
                        string processedWord = await _textService.ReturnProcessedString(word, sortAlgorithm);
                        return Ok(processedWord);
                    }
                    finally
                    {
                        _semaphore.Release();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An unexpected error occurred.");
                    return StatusCode(500, "Internal Server Error"); // Обработка неожиданных исключений
                }
            }
            else 
            {
                (bool isValid, string errorMessage) = _textService.IsWordCorrect(word);
                if (!isValid)
                {
                    return BadRequest(new { message = "HTTP ошибка 400 Bad Request: " + errorMessage });
                }
                string processedWord = await _textService.ReturnProcessedString(word, sortAlgorithm);
                return Ok(processedWord);
            }

        }


    }  
}
