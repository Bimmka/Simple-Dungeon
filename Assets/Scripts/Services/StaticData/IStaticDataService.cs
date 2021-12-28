using Enemies;
using Enemies.Spawn;
using Services.UI.Factory;
using StaticData.Enemies;
using StaticData.Hero;
using StaticData.Level;
using StaticData.UI;

namespace Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
    WindowInstantiateData ForWindow(WindowId id);
    HeroSpawnStaticData ForHero();
    EnemyStaticData ForMonster(EnemyTypeId typeId);
    LevelStaticData ForLevel(string sceneKey);
  }
}