using TetrisAPI.Data;
using TetrisAPI.Models;

namespace TetrisAPI.Services
{
    public interface IStatisticsService
    {
        /// <summary>
        /// Get tetris info by an id
        /// </summary>
        /// <param name="tetrisId"></param>
        /// <returns></returns>
        Game GetGame(int tetrisId);

        /// <summary>
        /// Get all tetris games for a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<Game> GetGameForUser(int userId);
    }

    public class StatisticsService : IStatisticsService
    {
        private readonly DBContext context;

        public StatisticsService(DBContext context)
        {
            this.context = context;       
        }

        public Game GetGame(int tetrisId)
        {
            return context.GameInfo
                .Where(t => t.Id.Equals(tetrisId)).First();
        }

        public IEnumerable<Game> GetGameForUser(int userId)
        {
            return context.GameInfo
                .Where(t => t.UserId.Equals(userId));
        }
    }
}
