using System.Collections.Generic;

namespace Services.Database
{
  public class DatabaseService : IDatabaseService
  {
    public void AddToLeaderboard(string nickname, int score)
    {
      
    }

    public List<LeaderboardPlayer> TopPlayers() => 
      new List<LeaderboardPlayer>();
  }
}