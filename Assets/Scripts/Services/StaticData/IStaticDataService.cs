using Services.UI.Factory;
using StaticData.Hero;
using StaticData.UI;
using UnityEngine;

namespace Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
    WindowInstantiateData ForWindow(WindowId id);
    HeroSpawnStaticData ForHero();
  }
}