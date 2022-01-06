using Enemies.Spawn;

namespace Services.Bonuses
{
  public interface IBonusSpawner : IService
  {
    void AddPoint(SpawnPoint spawnPoint);
  }
}