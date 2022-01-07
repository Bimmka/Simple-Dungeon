namespace Services.Score
{
  public interface IScoreService : ICleanupService
  {
    bool IsPLayerInTop();
    void SavePlayerInLeaderboard(string nickname);
  }
}