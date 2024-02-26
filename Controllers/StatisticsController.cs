using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public Game Get(int gameId) {
            return statisticsService.GetGame(gameId);
        }
    }
}
