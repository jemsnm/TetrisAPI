using TetrisAPI.Data;
using TetrisAPI.Models;

namespace TetrisAPI.Services
{
    public interface IStatisticsService
    {
        /// <summary>
        /// Get tetris info by an id
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        Game? GetGame(int gameId);

        /// <summary>
        /// Get all tetris games for a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<Game> GetGameForUser(string userId);
    }

    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDBContext context;

        public StatisticsService(ApplicationDBContext context)
        {
            this.context = context;       
        }

        public Game? GetGame(int gameId) =>
            context.GameInfo.Find(gameId);

        public IEnumerable<Game> GetGameForUser(string userId) =>
            context.GameInfo.Where(t => t.User.Id.Equals(userId));
    }
}
