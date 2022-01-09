using MongoDB.Bson;

namespace Services.Database
{
  public class LeaderboardPlayer
  {
    public ObjectId Id;
    public string Nickname;
    public int Score;
    
    public LeaderboardPlayer(string nickname, int score)
    {
      Nickname = nickname;
      Score = score;
    }
  }
}