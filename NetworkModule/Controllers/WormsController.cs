using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace NetworkModule.Controllers
{
    [ApiController]
    public class WormsController : ControllerBase
    {
        // POST: api/Worms
        [Route("api/{wormName}/getAction")]
        [HttpPost]
        public string Post([FromRoute] string wormName, [FromBody] JsonElement body)
        {
            string json = JsonSerializer.Serialize(body);
            return wormName + json;
        }
        
    }
}