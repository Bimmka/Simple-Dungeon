using Enemies.Spawn;
using StaticData.Level;

namespace Services.Bonuses
{
  public interface IBonusSpawner : IService
  {
    void AddPoint(SpawnPoint spawnPoint);
    void SpawnBonus(WaveBonus bonus);
  }
}