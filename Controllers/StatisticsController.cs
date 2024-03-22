using Microsoft.AspNetCore.Mvc;
using TetrisAPI.Libraries;
using TetrisAPI.Models;
using TetrisAPI.Services;

namespace TetrisAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        [HttpGet("{gameid}")]
        public IActionResult Get(int gameid) {
            var game = statisticsService.GetGame(gameid);
            return game != null ? Ok(game) : Util.GenerateError($"Could not find game with id: {gameid}");
        }
    }
}
