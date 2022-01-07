using System.Collections.Generic;

namespace Services.Database
{
  public interface IDatabaseService : IService
  {
    void AddToLeaderboard(string nickname, int score);
    List<LeaderboardPlayer> TopPlayers();
  }
}