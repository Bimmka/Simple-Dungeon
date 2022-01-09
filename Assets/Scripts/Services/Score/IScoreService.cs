using System.Threading.Tasks;

namespace Services.Score
{
  public interface IScoreService : ICleanupService
  {
    Task<bool> IsPLayerInTop();
    void SavePlayerInLeaderboard(string nickname);
  }
}