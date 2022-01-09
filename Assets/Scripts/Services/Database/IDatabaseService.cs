using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Database
{
  public interface IDatabaseService : IService
  {
    void AddToLeaderboard(string nickname, int score);
    Task<IEnumerable<LeaderboardPlayer>> UpdateTopPlayers();
    IEnumerable<LeaderboardPlayer> Leaderboard { get; }
    bool IsNeedToUpdateLeaderboard();
  }
}