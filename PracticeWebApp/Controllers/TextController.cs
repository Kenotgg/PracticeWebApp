using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace PracticeWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {
        [HttpGet("send")]
        public async Task<IActionResult> ReturnProcessedString(string word)
        {
            StringBuilder result = new StringBuilder();
            int lenght = word.Length;
            if (lenght % 2 == 0)
            {
                for (int i = lenght / 2 - 1; i >= 0; i--)
                {
                    result.Append(word[i]);
                }
                for (int i = lenght - 1; i >= lenght / 2; i--)
                {
                    result.Append(word[i]);
                }
            }
            else
            {
                for (int i = lenght - 1; i >= 0; i--)
                {
                    result.Append(word[i]);
                }
                result.Append(word);
            }
            return Ok(result.ToString());
        }
    }
}
