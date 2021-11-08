using System;
using System.Threading.Tasks;
using ConsoleApp1.CoreGame.Enums;
using ConsoleApp1.Network.Entity;
using ConsoleApp1.WormsLogic;
using Microsoft.AspNetCore.Mvc;

namespace NetworkModule.Controllers
{
    [ApiController]
    public class WormsController : ControllerBase
    {
        // POST: api/Worms
        [Route("api/{wormName}/getAction")]
        [HttpPost]
        public async Task<ActionResult<InfoFromServer>> Post([FromRoute] string wormName, [FromBody] InfoForServer infoForServer)
        {
            if (infoForServer == null)
            {
                return BadRequest();
            }

            var wormIntent = GetIntent(wormName, infoForServer);
            return Ok(wormIntent);
        }

        private InfoFromServer GetIntent(string wormName, InfoForServer infoForServer)
        {
            var worm = infoForServer.Worms.Find(it => it.Name == wormName);
            IWormLogic wormLogic = new OptionalLogic();
            
            Console.WriteLine($"From Client: {wormName}, {infoForServer != null}");
            var wormIntent = wormLogic.Decide(worm, infoForServer);

            string direction = "";
            bool split;
            
            if (wormIntent.Item1 == Actions.Budding)
            {
                split = true;
            }
            else
            {
                split = false;
            }
            
            switch (wormIntent.Item2)
            {
                case Directions.Top:
                {
                    direction = "Up";
                    break;
                }
                case  Directions.Bottom:
                {
                    direction = "Down";
                    break;
                }
                case  Directions.Left:
                {
                    direction = "Left";
                    break;
                }
                case  Directions.Right:
                {
                    direction = "Right";
                    break;
                }
                case Directions.None:
                {
                    direction = "None";
                    split = false;
                    break;
                }
            }

            ActionDTO actionDto = new ActionDTO(direction, split);
            
            Console.WriteLine(actionDto.Direction);
            Console.WriteLine(actionDto.Split);

            return new InfoFromServer(actionDto);
        }
    }
}